using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

class Levelling(RestClient client, ILogger<Levelling> logger) : IMessageCreateGatewayHandler
{
    public ValueTask HandleAsync(Message message)
    {
        if(message.Author.IsBot) {return default;}

        SqliteConnection connection = new SqliteConnection("Data Source=/home/creature/Projects/coding/Dahlia/levels.db");
        
        connection.Open();

        long level = 0;
        long xp = 0;

        SqliteDataReader reader = new SqliteCommand($"SELECT * FROM users WHERE id = {message.Author.Id}", connection).ExecuteReader();
        while (reader.Read())
        {
            level = reader.GetInt64(1);
            xp = reader.GetInt64(2);
        }

        string response = $"{message.Author.Id}, {level}, {xp}";

        logger.LogInformation(response);
        _ = Utilities.SendMessage(client, message.ChannelId, response);

        connection.Close();

        return default;
    }
}