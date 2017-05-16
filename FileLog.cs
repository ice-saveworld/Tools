using UnityEngine;

using System.Collections;

using System.IO;

public class FileLog : MonoBehaviour

{

    static int line = 0;

    public static void printf(string info)

    {

        line++;

        string path = Application.dataPath + "/ LogFile.txt";

        StreamWriter sw;

        //Debug.Log (path);

        if (line == 1)

        {

            sw = new StreamWriter(path, false);

            string fileTitle = "日志文件创建的时间  " +System.DateTime.Now.ToString();

            sw.WriteLine(fileTitle);

        }

        else

        {

            sw = new StreamWriter(path, true);

        }

        string lineInfo = line + "\t";

        sw.Write(lineInfo);

        sw.WriteLine(info);

        sw.Flush();

        sw.Close();

    }

    // Update is called once per frame

    void Update()

    {

        // test

        if (Input.GetKeyDown(KeyCode.Space))

        {

            FileLog.print("测试信息");

        }

    }

}