using Logictics.DAL.EFContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logictics.Web.Models
{
    public class Sitekeys
    {
        private static IConfigurationSection _configuration;
        public static void Configure(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public static string WebSiteDomain => _configuration["WebSiteDomain"];
        public static string Token => _configuration["SecretKey"];
        
       
    }
}
