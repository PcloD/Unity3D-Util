using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#region CsvData
public struct CsvData
{
    private static string FileName;
    private static Dictionary<string, Dictionary<string, string>> TabData = new Dictionary<string, Dictionary<string, string>>();


    public string GetData(string key, string head)
    {
        return TabData[head][key];
    }

    public void PushData(string key, Dictionary<string, string> data)
    {
        //Debug.Log("key"+key);
        TabData.Add(key, data);
    }

    public void SetFileName(string filename)
    {
        FileName = filename;
    }

    public void Init()
    {
        TabData.Clear();
        FileName = "";
    }

    public int GetLength()
    {
        return TabData.Keys.Count;
    }
}

#endregion

public class ReadCsvUtils
{
    private static Dictionary<string, CsvData> m_AllDatas = new Dictionary<string, CsvData>();

    private static ReadCsvUtils _instance = null;
    public static ReadCsvUtils GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ReadCsvUtils();
            m_AllDatas.Clear();
        }
        return _instance;
    }

    public bool LoadCsv(string fileName, string path)
    {
        TextAsset csvfile = Resources.Load(path, typeof(TextAsset)) as TextAsset;
        CsvData data = new CsvData();
        data.Init();
        data.SetFileName(fileName);

        string strbuff = "";
        //按行分割字符串
        string[] list_file = SplitString(csvfile.text.Trim(), '\r');
        string[] keys = SplitString(list_file[0].Trim(), ',');

        for (int i = 1; i < list_file.Length; i++)
        {
            Dictionary<string, string> dist_file = new Dictionary<string, string>();
            dist_file.Clear();
            string[] datas = SplitString(list_file[i], ',');
            for (int j = 0; j < datas.Length; j++)
            {
                dist_file.Add(keys[j], datas[j].Trim());
            }
            data.PushData(i.ToString(), dist_file);
        }
        m_AllDatas.Add(fileName, data);

        return true;
    }

    public string GetData(string filename, string key, string head)
    {
        CsvData data = new CsvData();
        string value = "";
        if (m_AllDatas.TryGetValue(filename, out data))
        {
            value = data.GetData(key, head);
        }
        else
        {
            value = "error";
        }
        return value;
    }

    public string GetData(string filename, string key, int head)
    {
        return GetData(filename, key, head.ToString());
    }

    /*
     * 获取文本总长度
     */
    public int GetLength(string filename)
    {
        CsvData data = new CsvData();
        if (m_AllDatas.TryGetValue(filename, out data))
        {
            return data.GetLength();
        }
        else
        {
            return 0;
        }
        
    }

    public string[] SplitString(string strContext, char strSpilt)
    {

        string[] ss = strContext.Split(strSpilt);
        return ss;
    }
}
