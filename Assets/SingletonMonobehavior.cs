using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script singleton.
/// </summary>
public abstract class MonoSingleton<TScript> : MonoBehaviour where TScript : MonoBehaviour
{
    protected bool _isReady = false;
    private static TScript _Instance = null;

    public static TScript Instant
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = MonoObject.Find<TScript>();
                if (_Instance == null)
                {
                    _Instance = MonoObject.Create<TScript>();
                }
            }
            return _Instance;
        }
    }

    public static bool Verify()
    {
        return (_Instance != null);
    }

    public bool IsReady
    {
        get
        {
            return _isReady;
        }
    }
    public virtual void Initialize()
    {
    }
    protected void Ready()
    {
        _isReady = true;
    }

    protected void Begin()
    {
        _isReady = false;
    }
    protected void Destroy()
    {
        _Instance = null;
    }
    protected void HideInHierarchy()
    {
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }
}

/// <summary>
/// Script object.
/// </summary>
public static class MonoObject
{
    private static Dictionary<System.Type, MonoBehaviour> _scripts = new Dictionary<System.Type, MonoBehaviour>();

    public static TScript Find<TScript>() where TScript : MonoBehaviour
    {
        TScript script = null;
        System.Type type = typeof(TScript);
        if (_scripts.ContainsKey(type))
        {
            script = _scripts[type] as TScript;
        }
        if (script == null)
        {
            script = Object.FindObjectOfType(typeof(TScript)) as TScript;
        }
        return script;
    }

    public static TScript[] FindArray<TScript>() where TScript : MonoBehaviour
    {
        TScript[] scriptArray = Object.FindObjectsOfType(typeof(TScript)) as TScript[];
        return scriptArray;
    }

    public static TScript Create<TScript>() where TScript : MonoBehaviour
    {
        System.Type type = typeof(TScript);
        if (!_scripts.ContainsKey(type))
        {
            GameObject temp = new GameObject(typeof(TScript).ToString());
            Object.DontDestroyOnLoad(temp);

            TScript script = temp.AddComponent<TScript>();
            if (script != null)
            {
                _scripts.Add(typeof(TScript), script);
            }
            return script;
        }
        return null;
    }

    public static bool Delete<TScript>() where TScript : MonoBehaviour
    {
        TScript script = Find<TScript>();
        if (script != null)
        {
            Object.Destroy(script.gameObject);
            {
                System.Type type = typeof(TScript);
                if (_scripts.ContainsKey(type))
                {
                    _scripts.Remove(typeof(TScript));
                }
            }
            return true;
        }
        return false;
    }

    public static void ClearAll()
    {
        List<MonoBehaviour> list = new List<MonoBehaviour>();
        foreach (KeyValuePair<System.Type, MonoBehaviour> pair in _scripts)
        {
            list.Add(pair.Value);
        }
        while (list.Count > 0)
        {
            Object.Destroy(list[0].gameObject);
            list.RemoveAt(0);
        }
        _scripts.Clear();
    }
}