using Dahlia.Utilities;
using NetCord.Rest;
using SQLite;

namespace Dahlia.Leveling;

static class Leveller
{
    private static LevelsDatabaseHandler handler = new();

    public static void Init()
    {
        
    }

    public static void ManageUser(RestClient client, ulong userid)
    {
        handler.AddUser(userid);
        Levels userLevels = handler.GetLevelByUser(userid);

        userLevels.Exp += 1;
        int maxExp = 30 + (userLevels.Level * 3);
        
        if(userLevels.Exp > maxExp)
        {
            userLevels.Level++;
            userLevels.Exp = 0;

            SendMessage.Send(client, 1504214992034070693, $"<@{userid}> Has reached level {userLevels.Level} !!");
        }

        handler.UpdateUserLevel(userLevels.Id, userLevels.Level, userLevels.Exp);
    }
}