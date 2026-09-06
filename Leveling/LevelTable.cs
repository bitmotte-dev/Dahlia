using SQLite;

namespace Dahlia.Leveling;

[Table("levels")]	 
public class Levels		
{		
    [PrimaryKey, AutoIncrement]
    [Column("user_id")]		
    public ulong Id { get; set; }	

    [Column("level")]		
    public int Level { get; set; }

    [Column("exp")]		
    public int Exp { get; set; }		
}	