//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
namespace GameDataTable
{
    public class ActorJobTypeTable : ConfigTableBase
    {
             public  string JobTypeName  { get; set; } 
            public  string ActorBaseName  { get; set; } 
       
    }

    public class ActorJobTypeTableManager : TableManager<ActorJobTypeTable>
    {

    }

    public class ActorJobTypeTableInstance :TableManagerInstance<ActorJobTypeTableManager,ActorJobTypeTableManager>
    {
        public static List<ActorJobTypeTable> GetActorList()
        {
            return SingleInstance.GetActorList();
        }

        public static ActorJobTypeTable GetData(int dataID)
        {
            return SingleInstance.GetData(dataID);
        }

        public static bool UpdateData(ActorJobTypeTable newValue)
        {
            return SingleInstance.UpdateData(newValue);
        }

        public static int Count{ get { return SingleInstance.Count; }}

        public static ActorJobTypeTable GetLastItme()
        {
            return SingleInstance.GetLastItme();
        }
    }
}
