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
    public class PlayerTable : ConfigTableBase
    {
             public  string PlayerName  { get; set; } 
            public  string Password  { get; set; } 
            public  int ActorTable_DataID  { get; set; } 
       
    }

    public class PlayerTableManager : TableManager<PlayerTable>
    {

    }

    public class PlayerTableInstance :TableManagerInstance<PlayerTableManager,PlayerTableManager>
    {
        public static List<PlayerTable> GetActorList()
        {
            return SingleInstance.GetActorList();
        }

        public static PlayerTable GetData(int dataID)
        {
            return SingleInstance.GetData(dataID);
        }

        public static bool UpdateData(PlayerTable newValue)
        {
            return SingleInstance.UpdateData(newValue);
        }

        public static int Count{ get { return SingleInstance.Count; }}

        public static PlayerTable GetLastItme()
        {
            return SingleInstance.GetLastItme();
        }
    }
}
