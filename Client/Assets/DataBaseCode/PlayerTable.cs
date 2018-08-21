namespace GameDataTable
{
    public class PlayerTable : ConfigTableBase
    {
        public int DataID {get;set;}
public string PlayerName {get;set;}
public string Password {get;set;}
public int ActorTable_DataID {get;set;}

    }

    public class PlayerTableManager : TableManager<PlayerTable>
    {

    }
}
