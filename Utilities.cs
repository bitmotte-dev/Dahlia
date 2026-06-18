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
    public static void CreateTableIfDoesntExist(string tableName, string columns)
    {
        SqliteConnection connection = new SqliteConnection("Data Source=/home/creature/Projects/coding/Dahlia/levels.db");
        connection.Open();

        new SqliteCommand($"CREATE TABLE IF NOT EXISTS {tableName} ({columns})", connection).ExecuteNonQuery();

        connection.Close();
    }
    #endregion
}

