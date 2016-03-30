using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EStore.BL.Exceptions;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;
using Microsoft.AspNet.Identity;

// ReSharper disable ReplaceWithSingleCallToSingleOrDefault

namespace EStore.BL.Services
{
    public class AdminUserService : ServiceBase
    {
        public AdminUserService(EStoreEntities entities) : base(entities)
        {
        }

        public void Save(AdminUserItem item)
        {
            var errors = item.GetValidationErrors();
            if (item.Id == 0 && item.Password.IsEmptyOrWhiteSpace())
                errors.Add(new DbValidationError("Password", "Необходимо указать пароль для нового пользователя"));
            errors.ThrowIfHasErrors();

            var user = item.Id == 0
                ? Db.CreateAndAdd<tblUser>()
                : Db.Set<tblUser>().Single(x => x.Id == item.Id);

            user.Email = item.Email;
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;
            if (!item.Password.IsEmptyOrWhiteSpace())
                user.Password = HashPassword(item.Password);
            user.UserName = item.UserName;
            user.IsAdmin = true;

            Db.SaveChanges();

            item.Id = user.Id;
        }

        public AdminUserItem Edit(long id)
        {
            var userItem = new AdminUserItem();

            if (id != 0)
            {
                userItem = Db.Set<tblUser>()
                    .Select(x => new AdminUserItem
                    {
                        Id = x.Id,
                        Email = x.Email,
                        UserName = x.UserName,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    })
                    .Single(x => x.Id == id);
            }

            return userItem;
        }

        public List<AdminUserItem> All()
        {
            return Db.AllAdminUsers();
        }

        public AdminUserItem Login(string email, string password)
        {
            if (email.IsEmptyOrWhiteSpace() || password.IsEmptyOrWhiteSpace())
                throw new ValidationException("Необходимо указать имя пользователя и пароль");

            var hasedPassword = HashPassword(password);

            var foundUser = Db.Set<tblUser>()
                .Where(x => x.Email == email)
                .Where(x => x.Password == hasedPassword)
                .Select(x => new AdminUserItem
                {
                    Id = x.Id,
                    Email = x.Email,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                })
                .SingleOrDefault();

            if (foundUser == null)
                throw new ValidationException("Имя пользователя или пароль неверны");

            return foundUser;
        }

        private string HashPassword(string password)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var data = Encoding.UTF8.GetBytes(password);
                var md5data = md5.ComputeHash(data);
                return Convert.ToBase64String(md5data);
            }
        }
    }
}