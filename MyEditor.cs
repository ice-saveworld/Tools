using System.IO;
using Excel;
using UnityEditor;
using UnityEngine;
using System.Data;
public class MyEditor : Editor {
    [MenuItem("Tools/MyEditor/RoleAttribute")]
    static void RoleAttribute()
    {
        //Debug.Log(Application.dataPath + "/RoleAttributes.xlsx");
        FileStream stream = File.Open(Application.dataPath + "/RoleAttributes.xlsx", FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //do
        //{
        //    Debug.Log(excelReader.Name);
        //    while (excelReader.Read())
        //    {
        //        for (int i = 0; i < excelReader.FieldCount; i++)
        //        {
        //            string value = excelReader.IsDBNull(i) ? "" : excelReader.GetString(i);
        //            Debug.Log(value);
        //        }
        //    }
        //} while (excelReader.NextResult());

        DataSet result = excelReader.AsDataSet();
        int columns = result.Tables[0].Columns.Count;
        int rows = result.Tables[0].Rows.Count;
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                string value = result.Tables[0].Rows[i][j].ToString();
                if (value == null || value == "") break;
                Debug.Log(value);
            }
        }
    }
}
