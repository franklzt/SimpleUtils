using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GameDataTable
{
    public class DownLoadDB : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(DownLoadDataBase());
        }

        public IEnumerator DownLoadDataBase()
        {
            string dbPath = string.Format("{0}/{1}", Application.persistentDataPath, DataBaseHelperExtend.DatabaseName);

            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }
            string path = string.Format("{0}/{1}", Application.streamingAssetsPath, DataBaseHelperExtend.DatabaseName);
            WWW db = new WWW(path);
            yield return db;

            WriteCodeToFile codeToFile = new WriteCodeToFile();
            codeToFile.WriteFile(dbPath, db.bytes, 1024);
        }
    }
}

   
