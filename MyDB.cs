using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using static System.Environment;

 
 public class MyDB
 {
    // Проверить если базы нет - создать
    public async static void InitializeDatabase()
    {
        using (SqliteConnection db = new SqliteConnection($"Filename=messages.db"))
        {
            db.Open();

            String tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                "first_name NVARCHAR(2048) NULL, " +
                "text NVARCHAR(2048) NULL)";

            SqliteCommand createTable = new SqliteCommand(tableCommand, db);

            createTable.ExecuteReader();
            db.Close();
        }
    }


    // Записать запись в таблицу
    public static void AddData(string first_name, string text)
    {
        using (SqliteConnection db = new SqliteConnection($"Filename=messages.db"))
        {
            db.Open();

            SqliteCommand insertCommand = new SqliteCommand();
            insertCommand.Connection = db;

            // Use parameterized query to prevent SQL injection attacks
            insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @first_name, @text);";
            insertCommand.Parameters.AddWithValue("@first_name", first_name);
            insertCommand.Parameters.AddWithValue("@text", text);

            insertCommand.ExecuteReader();

            db.Close();
        }

    }

 }