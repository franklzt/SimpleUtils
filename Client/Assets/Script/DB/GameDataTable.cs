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

    public class TableManager<T>  where T : ConfigTableBase, new()
    {
        List<T> tableList = new List<T>();

        public int Count{ get { return tableList.Count; }}

        public T GetLastItme()
        {
            if(Count >0)
            {
                return tableList[Count - 1];
            }
            return null;
        }

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

        public T GetData(int dataID)
        {
            for (int i = 0; i < tableList.Count; i++)
            {
                if(dataID == tableList[i].DataID)
                {
                    return tableList[i];
                }
            }
            return null;
        }

        public bool UpdateData(T newValue)
        {
            T result = GetData(newValue.DataID);
            if(result != null)
            {
                result = newValue;
                return true;
            }
            return false;
        }

    }

}


