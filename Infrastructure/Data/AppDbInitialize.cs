using System;
using System.Collections.Generic;
using Domain.Data;
using Domain.Entity;
using Domain.Enums;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data
{
    public class AppDbInitialize
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var providerScope = scope.ServiceProvider;

            var dbContext = providerScope.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            var logger = providerScope.GetRequiredService<ILogger<AppDbContext>>();
            var adminConfiguration = providerScope.GetService<IOptions<AdminConfig>>();

            using var txn = dbContext.Database.BeginTransaction();
            Seed(dbContext, adminConfiguration?.Value, logger);

            txn.Commit();
        }

        private static void Seed(
            AppDbContext context, 
            AdminConfig adminConfig,
            ILogger<AppDbContext> logger)
        {
            try
            {
                /* init project data */
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(adminConfig.Password);
                User mainAdmin = new User(
                        adminConfig.Mail, 
                        adminConfig.Nick, 
                        passwordHash, 
                        UserRoles.Admin
                    );
                context.Users.Add(mainAdmin);


                List<Lesson> lessons = new List<Lesson>()
                {
                    new Lesson("Знакомство с библиотекой"),
                    new Lesson("Основные понятия"),
                    new Lesson("Что такое код"),
                    new Lesson("Примеры кода"),
                    new Lesson("Компанент"),
                    new Lesson("Подходы к разработке")
                };
                
                
                context.SaveChanges();
            }
            catch (Exception err)
            {
                logger.LogCritical(err.Message);
                throw;
            }
        }
    }
}