using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader> {

    public T Load<T>(string path) where T : Object
    {
        T res = Resources.Load<T>(path);
        return res;
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        T[] res = Resources.LoadAll<T>(path);
        return res;
    }
}
