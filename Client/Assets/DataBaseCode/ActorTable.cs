namespace GameDataTable
{
    public class ActorTable : ConfigTableBase
    {
        public int DataID {get;set;}
public int ActorJobTypeTable_DataID {get;set;}
public int SexTable_DataID {get;set;}
public int AbilityTable_DataID {get;set;}

    }

    public class ActorTableManager : TableManager<ActorTable>
    {

    }
}
