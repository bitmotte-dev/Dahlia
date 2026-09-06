using NetCord.Rest;
using SQLite;

namespace Dahlia.Leveling;

public class LevelsDatabaseHandler {

    private SQLiteConnection _db;
    
    public LevelsDatabaseHandler() {
        
        _db = new SQLiteConnection("Databases/levels.db");
        _db.CreateTable<Levels>();	
    }

    public void AddUser(ulong user)
    {
        string query = "";
        
        query = $"SELECT * FROM levels;";
        List<Levels> result = _db.Query<Levels>(query);

        bool alreadyExists = false;
        foreach (Levels row in result)
        {
            if(row.Id == user)
            {
                alreadyExists = true;
                break;
            }
        }

        if(!alreadyExists)
        {
            Console.WriteLine("User doesn't exist, registering . . .");
            query = $"INSERT INTO levels VALUES ('{user}', '0', '0');";
            _db.Query<Levels>(query);
        }
    }

    public Levels GetLevelByUser(ulong id)
    {
        string query = "";
        
        query = $"SELECT * FROM levels WHERE user_id = {id};";
        Levels result = _db.FindWithQuery<Levels>(query);

        return result;
    }

    public void UpdateUserLevel(ulong id, int level, int exp)
    {
        string query = "";
        
        query = $"UPDATE levels SET level={level} WHERE user_id = {id};";
        _db.Query<Levels>(query);

        query = $"UPDATE levels SET exp={exp} WHERE user_id = {id};";
        _db.Query<Levels>(query);
    }
}	