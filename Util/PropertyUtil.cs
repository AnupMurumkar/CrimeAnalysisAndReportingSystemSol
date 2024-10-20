//using System;
//using System.Collections.Generic;
//using System.IO;

//namespace CrimeAnalysisAndReportingSystemSol.Util
//{
//    public class PropertyUtil
//    {
//        // Method to read a properties file and return the connection string
//        public static string GetPropertyString(string filePath)
//        {
//            try
//            {
//                Dictionary<string, string> properties = new Dictionary<string, string>();

//                // Reading each line from the file
//                foreach (var line in File.ReadAllLines(filePath))
//                {
//                    if (line.Contains("="))
//                    {
//                        var keyValue = line.Split('=');
//                        properties.Add(keyValue[0].Trim(), keyValue[1].Trim());
//                    }
//                }

//                // Create connection string using properties from file
//                string connectionString = $"Server={properties["hostname"]};" +
//                                          $"Database={properties["dbname"]};" +
//                                          $"User Id={properties["username"]};" +
//                                          $"Password={properties["password"]};" +
//                                          $"Port={properties["port"]};";

//                connectionString = "Data Source=LAPTOP-8JT18MT0;Initial Catalog=CrimeReportingSystem;Integrated Security=True;Trust Server Certificate=True";

//                return connectionString;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Error reading property file: " + ex.Message);
//            }
//        }
//    }
//}
