using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif

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

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            Connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            // Debug.Log("Final PATH: " + dbPath);
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
        const string DatabaseName = "Config.db";
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

