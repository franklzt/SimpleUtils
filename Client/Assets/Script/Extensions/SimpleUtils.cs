using UnityEngine;
using System.IO;

public static class SimpleUtils
{
    #region ConfigFileHelper
    public static void WriteIconNameToFile(this object obj)
    {
        Object[] allIcons = Resources.LoadAll("inventory_icons");
        string configFilePath = string.Format("{0}/Resources/data.txt", Application.dataPath);
        string data = "";
        for (int i = 0; i < allIcons.Length; i++)
        {
            if (allIcons[i] is Sprite)
            {
                data += (allIcons[i].name + ",");
            }
        }
        obj.WriteFileToDisk(configFilePath, data);
    }

    public static string[] ReadData(this object obj)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("data");
        string data = textAsset.text;
        string[] splits = data.Split(',');
        return splits;
    }


    #endregion


    public static string GetBasePath()
    {
        string basePath = Application.dataPath;
        string replacePath = "Client/Assets";
        basePath = basePath.Replace(replacePath, "");
        return basePath;
    }

    public static void ProcessProto(string exePath, string arg)
    {
        System.Diagnostics.Process foo = new System.Diagnostics.Process();
        foo.StartInfo.FileName = exePath;
        foo.StartInfo.Arguments = arg;
        foo.Start();
    }
}

public class WriteCodeToFile
{
    public void WriteToFile(string fileName, string script)
    {
        FileStream fileStream = File.Create(fileName);
        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(script);
        fileStream.Write(byteArray, 0, byteArray.Length);
        fileStream.Close();
    }
}
