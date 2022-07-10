using System;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;

    private static bool _initialized;
    private static bool _isQuitting;

    private static readonly object _lock = new object();

    public static T Instance
    {
        get
        {
            if (_isQuitting) return null;

            lock (_lock)
            {
                if (_instance != null)
                    return _instance;

                var objects = FindObjectsOfType<T>();
                if (objects.Length > 1)
                {
                    throw new Exception($"Multiple instances of the singleton, type: ({typeof(T).Name})");
                }

                _instance = objects.Length > 0 ? objects[0] : null;

                if (_instance != null)
                    return _instance;

                var gameObject = new GameObject($"{typeof(T).Name}[Singleton]");
                _instance = gameObject.AddComponent<T>();
                _instance.Initialize();
                _initialized = true;

                DontDestroyOnLoad(gameObject);

                Debug.Log($"Instance of {typeof(T)} created, gameObject: {gameObject.name}");

                return _instance;
            }
        }
    }

    protected abstract void Initialize();

    private void Awake()
    {
        Instance.ManualInit();
    }

    public void ManualInit() {}

    private void OnDestroy()
    {
        _instance = null;
        _initialized = false;
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
        _instance = null;
    }
}