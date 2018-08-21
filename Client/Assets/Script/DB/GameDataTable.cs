using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

namespace GameDataTable
{
    public class ConfigTableBase
    {
        [PrimaryKey, AutoIncrement]
        public int DataID { get; set; }
    }

    public class TableManager<T> where T : ConfigTableBase, new()
    {
        List<T> tableList = new List<T>();
        public TableManager()
        {
            var connection = DataBaseHelperExtend.GetDefaultDataBaseConnnect();
            var collection = connection.Table<T>();
            foreach (var item in collection)
            {
                tableList.Add(item);
            }
        }

        public List<T> GetActorList()
        {
            return tableList;
        }
    }

}


