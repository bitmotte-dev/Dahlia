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
    public static void CreateTableIfDoesntExist()
    {
        
    }
    #endregion
}

