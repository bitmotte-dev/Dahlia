using Microsoft.Extensions.Hosting;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;

static class Bot
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddDiscordGateway(options =>
        {
            options.Intents = GatewayIntents.GuildMessages | GatewayIntents.MessageContent;
        });
        builder.Services.AddGatewayHandler(typeof(Levelling));
        
        var host = builder.Build();
        
        await host.RunAsync();
    }
} 