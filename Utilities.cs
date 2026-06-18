using Microsoft.Data.Sqlite;
using NetCord.Rest;

static class Utilities
{
    #region discord
    public static async Task SendMessage(RestClient client, ulong channel, string message)
    {
        await client.SendMessageAsync(channel, message);
    }
    #endregion

    #region sqlite
    public static void CreateTableIfDoesntExist(string table, string columnsIfDoesntExist)
    {
        SqliteConnection connection = new SqliteConnection("Data Source=/home/creature/Projects/coding/Dahlia/levels.db");
        connection.Open();

        new SqliteCommand($"CREATE TABLE IF NOT EXISTS {table} ({columnsIfDoesntExist})", connection).ExecuteNonQuery();

        connection.Close();
    }

    public static void CreateColumnIfDoesntExist(string table, KeyValuePair<string,object> keyValuePair, string valuesIfDoesntExist)
    {
        SqliteConnection connection = new("Data Source=/home/creature/Projects/coding/Dahlia/levels.db");
        connection.Open();

        SqliteDataReader columnExistsReader = new SqliteCommand($"SELECT EXISTS(SELECT * FROM {table} WHERE {keyValuePair.Key}={keyValuePair.Value});", connection).ExecuteReader();
        while (columnExistsReader.Read())
        {
            bool exists = columnExistsReader.GetBoolean(0);
            if(!exists)
            {
                new SqliteCommand($"INSERT INTO {table} VALUES ({valuesIfDoesntExist})", connection).ExecuteNonQuery();
            }
        }

        connection.Close();
    }
    #endregion
}

