using SQLite4Unity3d;
using UnityEngine;
using System.Collections;
using System.IO;

namespace GameDataTable
{
    public abstract class DataBaseConnnectInterface
    {
        public abstract SQLiteConnection GetConnection(string DatabaseName);
    }

    public class DataBaseHelper : DataBaseConnnectInterface
    {
        private SQLiteConnection Connection;
        public override SQLiteConnection GetConnection(string DatabaseName)
        {
            if (Connection != null)
            {
                return Connection;
            }

            string dbPath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
            Connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            return Connection;
        }
    }

    public class SinglgInstance<T, U> where T : U, new()
    {
        static readonly U UValue = new T();
        public static U GetSingleInstance { get { return UValue; } }
    }

    public class SinglgInstance<T> where T : new()
    {
        public static T SingleInstance { get; } = new T();
    }

    public class TableManagerInstance<T, M> : SinglgInstance<T> where T : new() where M : new()
    {
        public M ManagerInstance { get; } = new M();
    }

    public static class DataBaseHelperExtend
    {
        public const string DatabaseName = "Config.db";
        public static SQLiteConnection GetDefaultDataBaseConnnect()
        {
            return GetDataBaseConnnectInterface(DatabaseName);
        }

        public static SQLiteConnection GetDataBaseConnnectInterface(string DatabaseName)
        {
            return SinglgInstance<DataBaseHelper, DataBaseConnnectInterface>.GetSingleInstance.GetConnection(DatabaseName);
        }
    }
}

