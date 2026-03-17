using System.Collections.Generic;
using System.Diagnostics;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected int _startPoolSize;
    [SerializeField] protected T _prefab;

    private Queue<T> _pool;

    protected virtual void Awake()
    {
        _pool = new Queue<T>();

        CreateObjectPool();
    }

    private void CreateObjectPool()
    {
        for (int i = 0; i < _startPoolSize; i++)
        {
            T obj = Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public T GetObject(Vector3 positionToPlace)
    {
        if (_pool.Count > 0)
        {
            T obj = _pool.Dequeue();
            obj.transform.position = positionToPlace;
            obj.gameObject.SetActive(true);
            return obj;
        }

        T obj2 = Instantiate(_prefab, positionToPlace, Quaternion.identity);
        return obj2;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}