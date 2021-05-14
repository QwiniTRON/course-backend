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
                /* admin */
                var mainAdmin = new User(adminConfig.Mail)
                {
                    Nick = adminConfig.Nick,
                    Photo = adminConfig.Photo
                };

                var createResult = userManager.CreateAsync(user:mainAdmin).GetAwaiter().GetResult();
                
                context.SaveChanges();
                
                var addPasswordResult = userManager.AddPasswordAsync(mainAdmin, adminConfig.Password).GetAwaiter().GetResult();
                var addRoleResult = userManager.AddToRoleAsync(mainAdmin, UserRoles.Admin.ToString()).GetAwaiter().GetResult();

                context.SaveChanges();

                /* subjects */
                var reactSubject = new Subject("React.js");
                context.Add(reactSubject);
                context.SaveChanges();
                
                /* lessons */
                List<Lesson> lessons = new List<Lesson>()
                {
                    new Lesson("Знакомство с библиотекой", false, reactSubject, 1, "Ознакомительный урок 1"),
                    new Lesson("Основные понятия", false, reactSubject, 2, "Ознакомительный урок 2"),
                    new Lesson("Что такое код", false, reactSubject, 3, "Ознакомительный урок 3 "),
                    new Lesson("Примеры кода", true, reactSubject, 4, "Ознакомительный урок 4"),
                    new Lesson("Компанент", false, reactSubject, 5, "Ознакомительный урок 5"),
                    new Lesson("Подходы к разработке", false, reactSubject, 6, "Ознакомительный урок 6")
                };
                
                context.Lessons.AddRange(lessons);
                
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