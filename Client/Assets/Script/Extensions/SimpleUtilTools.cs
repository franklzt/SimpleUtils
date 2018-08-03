using System.IO;

public static class SimpleUtilTools
{
    public static void WriteFileToDisk(this object obj, string path, string data)
    {
        FileStream fileStream = File.Create(path);
        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
        fileStream.Write(byteArray, 0, byteArray.Length);
        fileStream.Close();
    }
}
