using SQLite4Unity3d;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GameDataTable
{
    internal class TableView
    {
        public string type { get; set; }
        public string name { get; set; }
        public string tbl_name { get; set; }
        public int rootpage { get; set; }
        public string sql { get; set; }
    }


    public interface ISQLToSharp
    {
        string ReplaceSQLType(string sqlType);
    }

    public class SQLToSharp : ISQLToSharp
    {
        private Dictionary<string, string> sqlLookTable;
        public SQLToSharp()
        {
            sqlLookTable = new Dictionary<string, string>
            {
                { "INTEGER", "int" },
                { "STRING", "string" },
                { "BOOLEAN", "bool" },
                { "String[]", "string[]" }
            };
        }

        public string ReplaceSQLType(string sqlType)
        {
            if (sqlLookTable.ContainsKey(sqlType))
            {
                return sqlLookTable[sqlType];
            }

            return "string";
        }
    }


    public class BaseClassMember
    {
        public bool IsBaseClassMember(string baseClassMemberName)
        {
            return baseClassMemberName == "DataID";
        }
    }


    public struct CodeFormat
    {
        public string CodeTypeName;
        public string CodeType;
    }


    public interface ICodeFormatManager
    {
        string GetCodeResult();
        void Add(CodeFormat code);
    }

    public class CodeFormatManager: ICodeFormatManager
    {
        List<CodeFormat> codeFormats = new List<CodeFormat>();
        BaseClassMember baseClassMember;
        public CodeFormatManager()
        {
           baseClassMember = new BaseClassMember();
        }

        public void Add(CodeFormat code)
        {
            if (!codeFormats.Contains(code))
            {
                codeFormats.Add(code);
            }
        }

        public string GetCodeResult()
        {
            string code = "";
            string getset = @" { get; set; }";
            for (int i = 0; i < codeFormats.Count; i++)
            {
                if(baseClassMember.IsBaseClassMember(codeFormats[i].CodeTypeName))
                {
                    continue;
                }
                code += string.Format("     public  {0} {1} {2} \n       ", codeFormats[i].CodeType, codeFormats[i].CodeTypeName, getset);
            }
            return code;
        }


    }


    public interface IRegularExpressionHelper
    {
        string GetExpresionResult(string input);
    }


    public class RegularExpressionHelper: IRegularExpressionHelper
    {
        public string GetExpresionResult(string input)
        {
            ISQLToSharp sqlReplace = new SQLToSharp();

            string finalResult = "";
            string pattern = @"\((.*)";
            Match match = Regex.Match(input, pattern);
            pattern = @"\(|\sPRIMARY|KEY|ASC|AUTOINCREMENT|NOT|NULL|UNIQUE|DEFAULT\s\(\d+\)|,|\)|\bDEFAULT\s\w+|\d+\)\s|NULL|\'|DEFAULT";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(match.Value, "");
            pattern = @"\w+";
            regex = new Regex(pattern);
            MatchCollection matchCollection = regex.Matches(result);
            List<CodeFormat> codeList = new List<CodeFormat>();
            ICodeFormatManager formatManager = new CodeFormatManager();
            CodeFormat codeFormat = new CodeFormat();
            for (int i = 0; i < matchCollection.Count; i++)
            {
                string matchValue = matchCollection[i].Value;

                if((i % 2) == 0)
                {
                    codeFormat.CodeTypeName = string.Format("{0}", matchCollection[i].Value);
                }
                else
                {
                    codeFormat.CodeType = string.Format("{0}", sqlReplace.ReplaceSQLType(matchCollection[i].Value));
                    formatManager.Add(codeFormat);
                }
            }


            finalResult = formatManager.GetCodeResult();
            return finalResult;
        }
    }



    public class SQLTableCodeGenerator
    {
        public static void GenerateCode()
        {
            string sql = "SELECT * FROM sqlite_master where type='table' AND tbl_name != 'sqlite_sequence'";
            SQLiteConnection connection = DataBaseHelperExtend.GetDefaultDataBaseConnnect();
            List<TableView> tableViews = connection.Query<TableView>(sql);

            string templematePath = string.Format("{0}/Helper/Templemate/TableTemplate.txt", Application.dataPath);
            string templemateStr = File.ReadAllText(templematePath);

            WriteCodeToFile codeWrite = new WriteCodeToFile();

            IRegularExpressionHelper regularExpressionHelper = new RegularExpressionHelper();

            foreach (TableView item in tableViews)
            {
                string code = regularExpressionHelper.GetExpresionResult(item.sql);
                string replaceClassName = templemateStr.Replace("#SCRIPTNAME#", item.name);
                string finalCode = replaceClassName.Replace("#CodeList#", code);
                string finalSourcePath = string.Format("{0}/DataBaseCode/{1}.cs", Application.dataPath, item.tbl_name);
                codeWrite.WriteToFile(finalSourcePath, finalCode);
            }
        }
    }
}