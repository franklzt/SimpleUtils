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
    public class AbilityTable : ConfigTableBase
    {
             public  string AbilityName  { get; set; } 
            public  int AbilityAmount  { get; set; } 
       
    }

    public class AbilityTableManager : TableManager<AbilityTable>
    {

    }

    public class AbilityTableInstance :TableManagerInstance<AbilityTableManager,AbilityTableManager>
    {
        public static List<AbilityTable> GetActorList()
        {
            return SingleInstance.GetActorList();
        }

        public static AbilityTable GetData(int dataID)
        {
            return SingleInstance.GetData(dataID);
        }

        public static bool UpdateData(AbilityTable newValue)
        {
            return SingleInstance.UpdateData(newValue);
        }

        public static int Count{ get { return SingleInstance.Count; }}

        public static AbilityTable GetLastItme()
        {
            return SingleInstance.GetLastItme();
        }
    }
}
