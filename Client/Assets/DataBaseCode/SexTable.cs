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
    public class SexTable : ConfigTableBase
    {
             public  string SexName  { get; set; } 
       
    }

    public class SexTableManager : TableManager<SexTable>
    {

    }

    public class SexTableInstance :TableManagerInstance<SexTableManager,SexTableManager>
    {
        public static List<SexTable> GetActorList()
        {
            return SingleInstance.GetActorList();
        }

        public static SexTable GetData(int dataID)
        {
            return SingleInstance.GetData(dataID);
        }

        public static bool UpdateData(SexTable newValue)
        {
            return SingleInstance.UpdateData(newValue);
        }

        public static int Count{ get { return SingleInstance.Count; }}

        public static SexTable GetLastItme()
        {
            return SingleInstance.GetLastItme();
        }
    }
}
