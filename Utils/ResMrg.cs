using UnityEngine;
using System.Collections;
using UnityEngineInternal;

public class ResMrg  {

    public static Object GetRes(string path,string filename) 
    {
        return Resources.Load(path + filename);
    }
}
