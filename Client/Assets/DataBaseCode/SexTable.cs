namespace GameDataTable
{
    public class SexTable : ConfigTableBase
    {
        public int DataID {get;set;}
public string SexName {get;set;}

    }

    public class SexTableManager : TableManager<SexTable>
    {

    }
}
