using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance = null;
    private bool _initialized;

    public static T Instance
    {
        get
        {
            if (_instance) return _instance;

            if ((_instance = (T)FindObjectOfType(typeof(T))) != null)
            {
                return _instance;
            }

            _instance = new GameObject(typeof(T).Name).AddComponent<T>();

            return _instance;
            
        }
    }

    protected virtual void Init()
    {
    }
    
    private void Awake()
    {
        if (!_initialized)
        {
            Init();
            DontDestroyOnLoad(gameObject);

            _initialized = true;
        }
    }
}