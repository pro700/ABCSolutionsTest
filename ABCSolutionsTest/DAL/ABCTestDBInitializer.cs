using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ABCSolutionsTest.Models;



namespace ABCSolutionsTest.DAL
{
    public class ABCTestDBInitializer
    {
        public static void Initialize(ABCTestDBConext context)
        {
            context.Database.EnsureCreated();

            // Посмотреть есть ли какие-то юзеры
            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{Name="Administrator",Login="adm",Password="1",EMail="",IsAdmin=true},
                new User{Name="Guest",Login="guest",Password="1",EMail="",IsAdmin=false}
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

        }
    }
}   
