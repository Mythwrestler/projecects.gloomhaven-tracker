using System.IO;
using Npgsql;
using System;

namespace GloomhavenTracker.Service.SeedData
{


    public static class SeedData
    {
        
        private class SeedDataContent
        {
            public Guid ContentId {get; set;} = new Guid();
            public string Description {get; set;} = string.Empty;
            public object ContentJson {get; set;} = new Object();
        }

        public static void LoadDefaultContent(string connectionString)
        {
            NpgsqlConnection? _connection = new NpgsqlConnection(connectionString);
            try
            {
                _connection.Open();
                ClearDatabase(_connection);
                LoadContent(_connection);
                LoadTestData(_connection);
                _connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed To Load Default Content", ex);
            }
            finally
            {
                _connection.Close();
            }

        }

        private static void ClearDatabase(NpgsqlConnection connection)
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            var sqlFileString = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "20.DML.ClearDatabase.sql");
            var sqlString = File.OpenText(sqlFileString).ReadToEnd();
            var command = new NpgsqlCommand(sqlString, connection);
            command.ExecuteNonQuery();
        }

        private static void LoadContent(NpgsqlConnection connection)
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            var jsonFileString = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "30.DML.AddContent.JawsOfTheLion.sql");
            var sqlString = File.OpenText(jsonFileString).ReadToEnd();
            var command = new NpgsqlCommand(sqlString, connection);
            command.ExecuteNonQuery();
        }

        private static void LoadTestData(NpgsqlConnection connection)
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            var jsonFileString = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "40.DML.AddTestData.sql");
            var sqlString = File.OpenText(jsonFileString).ReadToEnd();
            var command = new NpgsqlCommand(sqlString, connection);
            command.ExecuteNonQuery();
        }
    }
}