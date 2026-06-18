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

        SqliteConnection connection = new SqliteConnection("Data Source=/home/creature/Projects/coding/Dahlia/levels.db");
        connection.Open();

        long level = 0;
        long xp = 0;

        Utilities.CreateTableIfDoesntExist("users", "id INTEGER PRIMARY KEY UNIQUE, level INTEGER NOT NULL, xp INTEGER NOT NULL");

        #region check if user exists
        SqliteDataReader userExistsReader = new SqliteCommand($"SELECT EXISTS(SELECT * FROM users WHERE id={message.Author.Id});", connection).ExecuteReader();
        while (userExistsReader.Read())
        {
            bool exists = userExistsReader.GetBoolean(0);
            if(!exists)
            {
                new SqliteCommand($"INSERT INTO users VALUES ({message.Author.Id}, 0, 0)", connection).ExecuteNonQuery();
            }
        }
        #endregion

        #region read
        SqliteDataReader infoReader = new SqliteCommand($"SELECT * FROM users WHERE id = {message.Author.Id}", connection).ExecuteReader();
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

            string levelUpMessage = $"<@{message.Author.Id}> Has reached level {level} !!";
            _ = Utilities.SendMessage(client, message.ChannelId, levelUpMessage);
        }
        
        #region write
        new SqliteCommand($"UPDATE users SET xp = {xp} WHERE id = {message.Author.Id}", connection).ExecuteNonQuery();
        new SqliteCommand($"UPDATE users SET level = {level} WHERE id = {message.Author.Id}", connection).ExecuteNonQuery();
        #endregion

        string debugMessage = $"usr{message.Author.Id}, lvl{level}, xp{xp}";
        _ = Utilities.SendMessage(client, message.ChannelId, debugMessage);
        
        connection.Close();
        return default;
    }
}