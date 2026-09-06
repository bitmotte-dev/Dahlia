using NetCord.Gateway;
using NetCord.Rest;

namespace Dahlia.Utilities;

static class SendMessage
{
    public static void Send(RestClient client, ulong channel, string contents)
    {
        client.SendMessageAsync(channel, contents);
    }
}
