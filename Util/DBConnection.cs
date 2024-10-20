using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace CrimeAnalysisAndReportingSystemSol.Util
{    
    public static class DBConnection
      {
            private static IConfigurationRoot _configuration;
            static string s = null;
            static DBConnection()
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("F:\\HEXAWARE C# CODES\\CrimeAnalysisAndReportingSystemSol\\Util\\AppSettings.json",
                   optional: true, reloadOnChange: true);
                _configuration = builder.Build();
            }
            public static string ReturnCn(string key)
            {

                s = _configuration.GetConnectionString("CrimeAnalysisAndReportingSystemCn");

                return s;
            }
        }
}