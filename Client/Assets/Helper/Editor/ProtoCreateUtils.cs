using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class ProtoCreateUtils
{
    [MenuItem("Assets/UpdateProto")]
    private static void UpdateProto()
    {
        string basePath = GetBasePath();
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

    private static string GetBasePath()
    {
        string basePath = Application.dataPath;
        string replacePath = "Client/Assets";
        basePath = basePath.Replace(replacePath, "");
        return basePath;
    }

    private static void ProcessProto(string exePath, string arg)
    {
        Process foo = new Process();
        foo.StartInfo.FileName = exePath;
        foo.StartInfo.Arguments = arg;
        foo.Start();
    }

    [MenuItem("Assets/GenerateCode")]
    private static void GenerateCode()
    {
        GenerateMessageCode();
        CopyMessageCodeToServer();
        AssetDatabase.Refresh();
    }

    private static void GenerateMessageCode()
    {
        string protoSourcePath = string.Format("{0}/protoSource", Application.dataPath);
        string[] files = Directory.GetFiles(protoSourcePath, "*.proto");
        string messageFilePath = string.Format("{0}/Helper/Templemate/Message.txt", Application.dataPath);
        string handleFilePath = string.Format("{0}/Helper/Templemate/MessageHandler.txt", Application.dataPath);

        new MessageCodeHelper().SetupCode(files, protoSourcePath, messageFilePath);
        new HandleCodeHelper().SetupCode(files, protoSourcePath, handleFilePath);
    }

    private static void CopyMessageCodeToServer()
    {
        string serverPattern = "SimpleServer/SimpleServer/messageCode";
        string serverPath = string.Format("{0}{1}", GetBasePath(), serverPattern);
        string clientPattern = "Client/Assets/messageCode";
        string clientPath = string.Format("{0}{1}", GetBasePath(), clientPattern);

        if (!Directory.Exists(serverPath))
        {
            Directory.CreateDirectory(serverPath);
        }
        string[] clientMessageFiles = Directory.GetFiles(clientPath, "*.cs");
        for (int i = 0; i < clientMessageFiles.Length; i++)
        {
            string serverFileName = clientMessageFiles[i].Replace(clientPattern, serverPattern);
            File.Copy(clientMessageFiles[i], serverFileName,true);
        }
    }

}

public class CodeBase
{
    public void SetupCode(string[] files, string protoSourcePath, string fileNamePattern, string messageFilePath, bool isReplace = true)
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
                GenerateCodeClientPath generateCodeClientPath = new GenerateCodeClientPath(generateCodeFileName);
                new GenerateCodeToFile(generateCodeClientPath, messageScript, isReplace);
            }
        }
    }
}

public class MessageCodeHelper
{
    public void SetupCode(string[] files, string protoSourcePath, string messageFilePath, bool replaceFile = true)
    {
        CodeBase codeBase = new CodeBase();
        codeBase.SetupCode(files, protoSourcePath, "message", messageFilePath, replaceFile);
    }
}

public class HandleCodeHelper
{
    public void SetupCode(string[] files, string protoSourcePath, string messageFilePath)
    {
        CodeBase codeBase = new CodeBase();
        codeBase.SetupCode(files, protoSourcePath, "Handler", messageFilePath, false);
    }
}

public class GenerateCodeBasePath
{
    protected readonly string _fileName;
    public GenerateCodeBasePath(string fileName)
    {
        _fileName = fileName;
    }

    public virtual string GetFilePath()
    {
        return "";
    }

    public string FileName()
    {
        return _fileName;
    }
}

public class GenerateCodeClientPath : GenerateCodeBasePath
{
    public GenerateCodeClientPath(string fileName) : base(fileName)
    {

    }

    public override string GetFilePath()
    {
        return string.Format("{0}/messageCode/{1}.cs", Application.dataPath, _fileName);
    }
}

public class GenerateCodeServerPath : GenerateCodeBasePath
{
    public GenerateCodeServerPath(string fileName) : base(fileName)
    {

    }

    public override string GetFilePath()
    {
        return string.Format("{0}/messageCode/{1}.cs", Application.dataPath, _fileName);
    }
}

public class GenerateCodeToFile
{
    private readonly string generateCodePath = "";

    private void WriteToFile(string fileName, string script)
    {
        FileStream fileStream = File.Create(generateCodePath);
        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(script);
        fileStream.Write(byteArray, 0, byteArray.Length);
        fileStream.Close();
    }

    public GenerateCodeToFile(GenerateCodeBasePath basePath, string script, bool replacefile = true)
    {
        generateCodePath = basePath.GetFilePath();
        bool needReaceCode = true;

        if (!File.Exists(generateCodePath))
        {
            WriteToFile(basePath.FileName(), script);
            needReaceCode = false;
        }

        if (replacefile && needReaceCode)
        {
            File.Delete(generateCodePath);
            WriteToFile(basePath.FileName(), script);
        }
    }
}