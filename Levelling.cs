using Microsoft.Extensions.Logging;

using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

class Levelling(RestClient client, ILogger<Levelling> logger) : IMessageCreateGatewayHandler
{
    public ValueTask HandleAsync(Message message)
    {
        if(message.Author.IsBot) {return default;}

        string response = $"User {message.Author.Username} has sent \"{message.Content}\"";

        logger.LogInformation(response);
        _ = Utilities.SendMessage(client, message.ChannelId, response);
        
        return default;
    }
}