using UnityEngine;

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
}
