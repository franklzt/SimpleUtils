using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;

public static class ProtoCreateUtils
{

    [MenuItem("Assets/UpdateProto")]
    static void CreateMyLua()
    {
        string basePath = Application.dataPath;
        string replacePath = "Client/Assets";
        basePath = basePath.Replace(replacePath, "");
        string externalExe = string.Format("{0}generator/CodeGenerator.exe", basePath);

        string sourcePath = string.Format("{0}Client/Assets/protoSource/", basePath); 

        string clientPath = string.Format("{0}Client/Assets/protoCode/", basePath); 
        string serverPath = string.Format("{0}SimpleServer/SimpleServer/proto", basePath);


        string[] allFiles = Directory.GetFiles(sourcePath, "*.proto");
        string[] fileNames = new string[allFiles.Length];

        for (int i = 0; i < allFiles.Length; i++)
        {



            fileNames[i] = allFiles[i].Replace(sourcePath, "");
            fileNames[i] = fileNames[i].Replace(".proto", "");

            string clientArg = string.Format("{0}{2}.proto --output {1}{2}.cs", sourcePath, clientPath, fileNames[i]);
            string serverArg = string.Format("{0}{2}.proto --output {1}/{2}.cs", sourcePath, serverPath, fileNames[i]);
            UnityEngine.Debug.Log(serverArg);
            ProcessProto(externalExe, clientArg);
            ProcessProto(externalExe, serverArg);

        }
    }

    static void SecureDeleteDir(string dirPath)
    {
        if (Directory.Exists(dirPath))
        {
            Directory.Delete(dirPath, true);
        }
        //DirectorySecurity directorySecurity = new DirectorySecurity(dirPath, AccessControlSections.All);
        Directory.CreateDirectory(dirPath);
    }

    static void ProcessProto(string exePath,string arg)
    {
        Process foo = new Process();
        foo.StartInfo.FileName = exePath;
        foo.StartInfo.Arguments = arg;
        foo.Start();
    }
}
