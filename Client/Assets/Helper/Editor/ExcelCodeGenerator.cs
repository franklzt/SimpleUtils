using ExcelDataReader;
using System.Data;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class ExcelCodeGenerator
{
    [MenuItem("Assets/ExcelCodeGenerator")]
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

        Debug.Log(filesNames[0]);

        handlers = new ExcelHandleBase[filesNames.Length];
        handlers[0] = new ExcelHandleBase(new ComplexHandleTemple());
        handlers[1] = new ExcelHandleBase(new NormalHandleTemple());
        handlers[2] = new ExcelHandleBase(new SimpleHandleTemple());

        for (int i = 0; i < filesNames.Length; i++)
        {
            handlers[i].StartReadData(filesNames[i]);
        }
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
                ReadHeaderRow = (rowReader) => {
                    // F.ex skip the first row and use the 2nd row as column headers:
                    rowReader.Read();
                },

                // Gets or sets a callback to determine whether to include the 
                // current row in the DataTable.
                FilterRow = (rowReader) => {
                    return true;
                },

                // Gets or sets a callback to determine whether to include the specific
                // column in the DataTable. Called once per column after reading the 
                // headers.
                FilterColumn = (rowReader, columnIndex) => {
                    return true;
                }
            }
        }));
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
        Debug.Log(result.GetXml());
    }
}

public class NormalHandleTemple : HandleTempleInterface
{
    public void Handler(DataSet result)
    {

    }
}

public class ComplexHandleTemple : HandleTempleInterface
{
    public void Handler(DataSet result)
    {

    }
}

