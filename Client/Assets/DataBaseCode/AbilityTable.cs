namespace GameDataTable
{
    public class AbilityTable : ConfigTableBase
    {
        public int DataID {get;set;}
public string AbilityName {get;set;}
public int AbilityAmount {get;set;}

    }

    public class AbilityTableManager : TableManager<AbilityTable>
    {

    }
}
