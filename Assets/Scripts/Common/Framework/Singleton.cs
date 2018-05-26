using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    //_instance = Instantiate(Resources.Load(typeof(T).Name) as GameObject).GetComponent<T>();

                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "(singleton) " + typeof(T).ToString();
                }
            }

            return _instance;
        }
    }

    protected bool destroyOnLoad = false;

    //destroy this object if an instance already exists
    public virtual void Start()
    {
        //DebugLogger.Log("Singleton<" + typeof(T).Name + ">[" + gameObject.GetInstanceID() + "]::Start()");
        if (RemoveThisInstance())
        {
            return;
        }

        if (!destroyOnLoad)
        {
            //DebugLogger.Log("Setting singleton to not destroy on load.");
            DontDestroyOnLoad(gameObject);
        }
    }

    private bool RemoveThisInstance()
    {
        if (this != Instance)
        {
            gameObject.SetActive(false);
            DestroyImmediate(gameObject);
            return true;
        }

        return false;
    }
}
