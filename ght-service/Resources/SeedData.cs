using System.IO;
using Npgsql;
using System.Text.Json;
using System;
using System.Collections.Generic;

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
                LoadDatabaseStructure(_connection);
                LoadContent(_connection);
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

        private static void LoadDatabaseStructure(NpgsqlConnection connection)
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            var sqlFileString = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database.ddl.sql");
            var sqlString = File.OpenText(sqlFileString).ReadToEnd();
            var command = new NpgsqlCommand(sqlString, connection);
            command.ExecuteNonQuery();
        }

        private static void LoadContent(NpgsqlConnection connection)
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            var jsonFileString = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "GameContent.JawsOfTheLion.dml.sql");
            var sqlString = File.OpenText(jsonFileString).ReadToEnd();
            var command = new NpgsqlCommand(sqlString, connection);
            command.ExecuteNonQuery();
        }
    }
}

/*


{
	"standard":{
		"0":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"1":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"2":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"3":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"4":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"5":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"6":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"7":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]}
	},
	"elite":{
		"0":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"1":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"2":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"3":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"4":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"5":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"6":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]},
		"7":{"health":0,"movement":0,"attack":0,"defenseEffects":[],"attackEffects":[],"immunity":[]}
	}
}




{
	"standard":{
		"0":{"health":0,"healthMod":"","movement":0,"moveMod":"","attack":0,"attackMod":"","defenseEffects":[],"attackEffects":[],"immunity":[]},
		"1":{"health":0,"healthMod":"","movement":0,"moveMod":"","attack":0,"attackMod":"","defenseEffects":[],"attackEffects":[],"immunity":[]},
		"2":{"health":0,"healthMod":"","movement":0,"moveMod":"","attack":0,"attackMod":"","defenseEffects":[],"attackEffects":[],"immunity":[]},
		"3":{"health":0,"healthMod":"","movement":0,"moveMod":"","attack":0,"attackMod":"","defenseEffects":[],"attackEffects":[],"immunity":[]},
		"4":{"health":0,"healthMod":"","movement":0,"moveMod":"","attack":0,"attackMod":"","defenseEffects":[],"attackEffects":[],"immunity":[]},
		"5":{"health":0,"healthMod":"","movement":0,"moveMod":"","attack":0,"attackMod":"","defenseEffects":[],"attackEffects":[],"immunity":[]},
		"6":{"health":0,"healthMod":"","movement":0,"moveMod":"","attack":0,"attackMod":"","defenseEffects":[],"attackEffects":[],"immunity":[]},
		"7":{"health":0,"healthMod":"","movement":0,"moveMod":"","attack":0,"attackMod":"","defenseEffects":[],"attackEffects":[],"immunity":[]}
	},
	"elite":{}
}



*/