using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

class Leveling(RestClient client, ILogger<Leveling> logger) : IMessageCreateGatewayHandler
{
    public ValueTask HandleAsync(Message message)
    {
        if(message.Author.IsBot) {return default;}

        logger.LogInformation(message.Content);

        SqliteConnection connection = new SqliteConnection("Data Source=/home/creature/Projects/coding/Dahlia/levels.db");
        connection.Open();

        long level = 0;
        long xp = 0;

        const string table = "users";
        Utilities.CreateTableIfDoesntExist(table, columnsIfDoesntExist: "id INTEGER PRIMARY KEY UNIQUE, level INTEGER NOT NULL, xp INTEGER NOT NULL");
        Utilities.CreateColumnIfDoesntExist(table, keyValuePair: new("id",message.Author.Id), valuesIfDoesntExist: $"{message.Author.Id}, 0, 0");

        #region read
        SqliteDataReader infoReader = new SqliteCommand($"SELECT * FROM {table} WHERE id = {message.Author.Id}", connection).ExecuteReader();
        while (infoReader.Read())
        {
            level = infoReader.GetInt64(1);
            xp = infoReader.GetInt64(2);
        }
        #endregion

        Random rng = new Random();
        xp += rng.Next(1,7); //1-6
        if(xp > 80 + (level * 10))
        {
            xp = 0;
            level += 1;

            const long activity_channel = 1504214992034070693;
            string levelUpMessage = $"<@{message.Author.Id}> Has reached level {level} !!";
            _ = Utilities.SendMessage(client, activity_channel, levelUpMessage);
            logger.LogInformation(levelUpMessage);
        }
        
        #region write
        new SqliteCommand($"UPDATE {table} SET xp = {xp} WHERE id = {message.Author.Id}", connection).ExecuteNonQuery();
        new SqliteCommand($"UPDATE {table} SET level = {level} WHERE id = {message.Author.Id}", connection).ExecuteNonQuery();
        #endregion

        string debugMessage = $"usr{message.Author.Id}, lvl{level}, xp{xp}";
        logger.LogInformation(debugMessage);
        
        connection.Close();
        return default;
    }
}