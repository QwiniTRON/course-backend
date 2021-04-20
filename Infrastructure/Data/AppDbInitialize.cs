using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Entity;
using Domain.Enums;
using Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
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

            var userManager = providerScope.GetRequiredService<UserManager<User>>();
            var rolesManager = providerScope.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var logger = providerScope.GetRequiredService<ILogger<AppDbContext>>();
            var adminConfiguration = providerScope.GetService<IOptions<AdminConfig>>();

            using var txn = dbContext.Database.BeginTransaction();
            Seed(dbContext, adminConfiguration?.Value, logger, userManager, rolesManager);

            txn.Commit();
        }

        private static void Seed(
                AppDbContext context, 
                AdminConfig adminConfig,
                ILogger<AppDbContext> logger,
                UserManager<User> userManager,
                RoleManager<IdentityRole<int>> rolesManager
            )
        {
            try
            {
                /* init app */
                var roles = Enum.GetNames(typeof(UserRoles));

                var dbRoles = context.Roles.ToList();

                if (roles.Length > dbRoles.Count)
                {
                    var rolesToAdd = roles
                        .Where(x => dbRoles.All(dbr => dbr.Name != x));
                    
                    foreach (var role in rolesToAdd)
                    {
                        rolesManager.CreateAsync(new IdentityRole<int>(role)).Wait();
                    }
                }
                
                context.SaveChanges();
                
                /* init project data */
                var mainAdmin = new User(adminConfig.Mail);

                var createResult = userManager.CreateAsync(user:mainAdmin).GetAwaiter().GetResult();
                
                context.SaveChanges();
                
                var addPasswordResult = userManager.AddPasswordAsync(mainAdmin, adminConfig.Password).GetAwaiter().GetResult();
                var addRoleResult = userManager.AddToRoleAsync(mainAdmin, UserRoles.Admin.ToString()).GetAwaiter().GetResult();

                context.SaveChanges();

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