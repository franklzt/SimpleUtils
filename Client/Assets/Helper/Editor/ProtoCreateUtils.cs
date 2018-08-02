using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using Debug = UnityEngine.Debug;
using System.Text.RegularExpressions;
using System;


public static class ProtoCreateUtils
{

    [MenuItem("Assets/UpdateProto")]
    static void CreateMyLua()
    {
        string basePath = Application.dataPath;
        string replacePath = "Client/Assets";
        basePath = basePath.Replace(replacePath, "");
        string externalExe = string.Format("{0}generator/protogen", basePath);

        string sourcePath = string.Format("{0}Client/Assets/protoSource/", basePath);

        string clientPath = string.Format("{0}Client/Assets/protoCode/", basePath);
        string serverPath = string.Format("{0}SimpleServer/SimpleServer/proto", basePath);


        string[] allFiles = Directory.GetFiles(sourcePath, "*.proto");
        string[] fileNames = new string[allFiles.Length];

        for (int i = 0; i < allFiles.Length; i++)
        {
            fileNames[i] = allFiles[i].Replace(sourcePath, "");
            fileNames[i] = fileNames[i].Replace(".proto", "");

            string clientArg = string.Format("-i:{0}{2}.proto -o:{1}{2}.cs", sourcePath, clientPath, fileNames[i]);
            string serverArg = string.Format("-i:{0}{2}.proto -o:{1}/{2}.cs", sourcePath, serverPath, fileNames[i]);
            UnityEngine.Debug.Log(string.Format("{0} {1}", externalExe, clientArg));
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

    static void ProcessProto(string exePath, string arg)
    {
        Process foo = new Process();
        foo.StartInfo.FileName = exePath;
        foo.StartInfo.Arguments = arg;
        foo.Start();
    }

    [MenuItem("Assets/GenerateCode")]
    static void GenerateCode()
    {
        string protoSourcePath = string.Format("{0}/protoSource", Application.dataPath);
        string[] files = Directory.GetFiles(protoSourcePath, "*.proto");
        string messageFilePath = string.Format("{0}/Helper/Templemate/Message.txt", Application.dataPath);
        string handleFilePath = string.Format("{0}/Helper/Templemate/MessageHandler.txt", Application.dataPath);

        new MessageCodeHelper().SetupCode(files, protoSourcePath, messageFilePath);
        new HandleCodeHelper().SetupCode(files, protoSourcePath, handleFilePath);

        AssetDatabase.Refresh();
    }

    class CodeBase
    {
        public void SetupCode(string[] files, string protoSourcePath, string fileNamePattern,string messageFilePath,bool isReplace)
        {
            string nameSpacePattern = @"{$FileName}";
            string classNamePattern = @"$ClassName";
            string messageContent = File.ReadAllText(messageFilePath);

            for (int i = 0; i < files.Length; i++)
            {
                string namespaceReplace = files[i];
                string replace = string.Format(@"{0}\", protoSourcePath);
                namespaceReplace = namespaceReplace.Replace(replace, "").Replace(".proto", "");

                string proto = File.ReadAllText(files[i]);

                string pattern = @"(?<=message).*?(?=\{)";

                Regex regex = new Regex(pattern);

                MatchCollection matchCollection = regex.Matches(proto);
                foreach (Match classPatternReplace in matchCollection)
                {
                    string messageScript = messageContent;
                    string trimClassPattern = classPatternReplace.Value.Trim();
                    messageScript = messageScript.Replace(nameSpacePattern, namespaceReplace);
                    messageScript = messageScript.Replace(classNamePattern, trimClassPattern);

                    string generateCodeFileName = string.Format("{0}{1}", trimClassPattern, fileNamePattern);
                    new GenerateCodeToFile(generateCodeFileName, messageScript, isReplace);
                }
            }
        }
    }

    class MessageCodeHelper
    {
        public void SetupCode(string[] files, string protoSourcePath, string messageFilePath,bool replaceFile = true)
        {
            CodeBase codeBase = new CodeBase();
            codeBase.SetupCode(files, protoSourcePath, "message",messageFilePath,replaceFile);
        }
    }

    class HandleCodeHelper
    {
        public void SetupCode(string[] files, string protoSourcePath, string messageFilePath)
        {
            CodeBase codeBase = new CodeBase();
            codeBase.SetupCode(files, protoSourcePath, "Handler", messageFilePath, false);
        }
    }

    class GenerateCodeToFile
    {
        string generateCodePath = "";
        void WriteToFile(string fileName, string script)
        {           
            FileStream fileStream = File.Create(generateCodePath);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(script);
            fileStream.Write(byteArray, 0, byteArray.Length);
            fileStream.Close();
        }

        public GenerateCodeToFile(string fileName, string script, bool replacefile = true)
        {
            generateCodePath = string.Format("{0}/messageCode/{1}.cs", Application.dataPath, fileName);

            bool needReaceCode = true;

            if (!File.Exists(generateCodePath))
            {
                WriteToFile(fileName, script);
                needReaceCode = false;
            }

            if(replacefile && needReaceCode)
            {
                File.Delete(generateCodePath);
                WriteToFile(fileName, script);
            }
        }
    }

}
