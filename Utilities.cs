using NetCord.Rest;

static class Utilities
{
    #region discord
    public static async Task SendMessage(RestClient client, ulong channel, string message)
    {
        await client.SendMessageAsync(channel, message);
    }
    #endregion
    //get values
    //SELECT * FROM users WHERE id = ???;

    //register user
    //INSERT INTO users VALUES (?, ?, ?)

    //update values
    //UPDATE users SET ??? = ??? WHERE id = ???
}

