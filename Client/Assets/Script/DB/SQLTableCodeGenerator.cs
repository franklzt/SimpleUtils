using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;
using System.IO;

namespace GameDataTable
{
    class TableView
    {
        public string type { get; set; }
        public string name { get; set; }
        public string tbl_name { get; set; }
        public int rootpage { get; set; }
        public string sql { get; set; }
    }


    public class SQLToSharp
    {
        Dictionary<string, string> sqlLookTable;
        public SQLToSharp()
        {
            sqlLookTable = new Dictionary<string, string>();
            sqlLookTable.Add("INTEGER", "int");
            sqlLookTable.Add("STRING", "string");
            sqlLookTable.Add("BOOLEAN", "bool");
            sqlLookTable.Add("String[]", "string[]");
        }

        public string ReplaceSQLType(string sqlType)
        {
            if (sqlLookTable.ContainsKey(sqlType))
            {
                return sqlLookTable[sqlType];
            }

            return "Error";
        }
    }


    public class BaseClassMember
    {
        public bool IsBaseClassMember(string baseClassMemberName)
        {
            return baseClassMemberName == "DataID";
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
            SQLToSharp sqlReplace = new SQLToSharp();
            WriteCodeToFile codeWrite = new WriteCodeToFile();
            BaseClassMember baseClassMember = new BaseClassMember();
            foreach (var item in tableViews)
            {
                // string replaceTypeName = templemateStr.Replace("#SCRIPTNAME#", item.name);
                string needMatch = item.sql;
                string removePattern = string.Format("CREATE TABLE {0} (", item.tbl_name);
                string removeStr = needMatch.Replace(removePattern, "");
                string[] splits = removeStr.Split(',');
                string code = "";
                for (int i = 0; i < splits.Length; i++)
                {
                    splits[i] = splits[i].Trim();
                    string[] codeSplited = splits[i].Split(' ', '\t');

                    string typeName = codeSplited[0];

                    if (baseClassMember.IsBaseClassMember(typeName))
                    {
                        continue;
                    }
                    string type = sqlReplace.ReplaceSQLType(codeSplited[1]);
                    string getset = @"{get;set;}";
                    string singleCode = string.Format(@"        public {0} {1} {2}", type, typeName, getset);
                    code += singleCode + "\n";
                }

                string replaceClassName = templemateStr.Replace("#SCRIPTNAME#", item.name);
                string finalCode = replaceClassName.Replace("#CodeList#", code);

                string finalSourcePath = string.Format("{0}/DataBaseCode/{1}.cs", Application.dataPath, item.tbl_name);

                codeWrite.WriteToFile(finalSourcePath, finalCode);
            }
        }
    }
}