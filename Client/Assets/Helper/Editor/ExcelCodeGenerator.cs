using ExcelDataReader;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class ExcelCodeGenerator
{
    [MenuItem("Assets/Helper/ExcelCodeGenerator")]
    private static void CodeGeneratorFromExcelFiles()
    {
        string fileDir = "ExcelFiles";

        ExcelHandleBase[] handlers;

        string excelFileBase = string.Format("{0}/{1}/", Application.dataPath, fileDir);

        if (!Directory.Exists(excelFileBase))
        {
            return;
        }

        string[] filesNames = Directory.GetFiles(excelFileBase, "*.xlsx");


        handlers = new ExcelHandleBase[filesNames.Length];
        handlers[0] = new ExcelHandleBase(new ComplexHandleTemple());
        handlers[1] = new ExcelHandleBase(new NormalHandleTemple());
        handlers[2] = new ExcelHandleBase(new SimpleHandleTemple());

        for (int i = 0; i < filesNames.Length; i++)
        {
            handlers[i].StartReadData(filesNames[i]);
        }
    }

    [MenuItem("Assets/Helper/SQLitCodeGenerator")]
    private static void CodeGeneratorFromSqlit()
    {
        GameDataTable.SQLTableCodeGenerator.GenerateCode();
        AssetDatabase.Refresh();
    }
}


public class ExcelHandleBase
{
    private IExcelDataReader reader;
    private readonly FileStream excelStream;
    private HandleTempleInterface handleTempleInterface;
    public ExcelHandleBase(HandleTempleInterface readDataHandler)
    {
        handleTempleInterface = readDataHandler;
    }

    public void StartReadData(string path)
    {
        FileStream excelStream = File.Open(path, FileMode.Open, FileAccess.Read);

        reader = ExcelReaderFactory.CreateReader(excelStream, new ExcelReaderConfiguration { FallbackEncoding = Encoding.GetEncoding(1252) });
        handleTempleInterface.Handler(reader.AsDataSet(new ExcelDataSetConfiguration()
        {

            // Gets or sets a value indicating whether to set the DataColumn.DataType 
            // property in a second pass.
            UseColumnDataType = true,

            // Gets or sets a callback to obtain configuration options for a DataTable. 
            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
            {

                // Gets or sets a value indicating the prefix of generated column names.
                EmptyColumnNamePrefix = "Column",

                // Gets or sets a value indicating whether to use a row from the 
                // data as column names.
                UseHeaderRow = false,

                // Gets or sets a callback to determine which row is the header row. 
                // Only called when UseHeaderRow = true.
                ReadHeaderRow = (rowReader) =>
                {
                    // F.ex skip the first row and use the 2nd row as column headers:
                    rowReader.Read();
                },

                // Gets or sets a callback to determine whether to include the 
                // current row in the DataTable.
                FilterRow = (rowReader) =>
                {
                    return true;
                },

                // Gets or sets a callback to determine whether to include the specific
                // column in the DataTable. Called once per column after reading the 
                // headers.
                FilterColumn = (rowReader, columnIndex) =>
                {
                    return true;
                }
            }
        }));

        reader.Dispose();
        excelStream.Close();
    }
}


public interface HandleTempleInterface
{
    void Handler(DataSet result);
}

public class SimpleHandleTemple : HandleTempleInterface
{
    public void Handler(DataSet result)
    {


    }
}

public class NormalHandleTemple : HandleTempleInterface
{
    public void Handler(DataSet result)
    {

        //DataTable dataTable = dataTableCollection[0];
        //DataColumnCollection dataColumnCollection = dataTable.Columns;

        DataTableCollection dataTableCollection = result.Tables;

        List<List<CodeStrcut>> codeCollections = new List<List<CodeStrcut>>();

        for (int tableCount = 0; tableCount < result.Tables.Count; tableCount++)
        {
            DataTable dataTable = dataTableCollection[tableCount];

            DataRowCollection dataRowCollection = dataTable.Rows;
            DataColumnCollection dataColumnCollection = dataTable.Columns;

            List<CodeStrcut> tableCodeStruct = new List<CodeStrcut>();


            for (int coloumCount = 1; coloumCount < dataColumnCollection.Count; coloumCount++)
            {
                CodeStrcut codeStrcut = new CodeStrcut();
                for (int rowCount = 2; rowCount < 4; rowCount++)
                {
                    string code = string.Format("{0}", dataRowCollection[rowCount][dataColumnCollection[coloumCount]].ToString());
                    codeStrcut.AssignValue(rowCount - 2, code);
                }
                tableCodeStruct.Add(codeStrcut);
            }
            codeCollections.Add(tableCodeStruct);
        }

        for (int i = 0; i < codeCollections.Count; i++)
        {
            for (int j = 0; j < codeCollections[i].Count; j++)
            {
                CodeStrcut codeStrcut = codeCollections[i][j];
                Debug.Log(codeStrcut.GetSharpCode());
            }
        }
    }
}

public class ComplexHandleTemple : HandleTempleInterface
{
    public void Handler(DataSet result)
    {

    }
}

public struct CodeStrcut
{
    private string VaribleName { get; set; }
    private string VaribleType { get; set; }
    private string GenerateJson { get; set; }
    private string GenerateXML { get; set; }

    public void AssignValue(int index, string value)
    {
        switch (index)
        {
            case 0:
                VaribleName = value;
                break;
            case 1:
                VaribleType = value;
                break;
            case 2:
                GenerateJson = value;
                break;
            case 3:
                GenerateXML = value;
                break;
            default:
                break;
        }
    }

    public string GetSharpCode()
    {
        return string.Format("public {0} {1};", VaribleType, VaribleName);
    }

}


