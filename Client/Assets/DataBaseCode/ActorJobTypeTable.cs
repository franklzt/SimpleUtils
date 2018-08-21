namespace GameDataTable
{
    public class ActorJobTypeTable : ConfigTableBase
    {
        public int DataID { get; set; }
        public string JobTypeName { get; set; }
        public string ActorBaseName { get; set; }

    }

    public class ActorJobTypeTableManager : TableManager<ActorJobTypeTable>
    {

    }
}
