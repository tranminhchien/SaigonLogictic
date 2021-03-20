using Common.Utils;
using Logictics.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.DAL.Seed
{
    public static class ModelBuilderExtensions
    {
       

        public static void Seed(this ModelBuilder modelBuilder)
        {
            IEncryptionUtil _encryptionUtil = new EncryptionUtil();
            var hash = _encryptionUtil.EncodeSHA1("123456");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            var clientId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DF");
            modelBuilder.Entity<UserAdmin>().HasData(new UserAdmin
            {
                Id = adminId.ToString(),
                UserName = "admin",
                Role = "ADMIN",
                PassWord = hash,
                Status = "ACTIVE"
            });
            modelBuilder.Entity<UserAdmin>().HasData(new UserAdmin
            {
                Id = clientId.ToString(),
                UserName = "ChienClient",
                Role = "CLIENT",
                PassWord = hash,
                Status = "ACTIVE",
                CreateDate = 1
            });
        }
    }
}
