using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool = new Queue<T>();
    private T _prefab;

    public ObjectPool(T prefab) 
    {
        _prefab = prefab;
    }

    public T GetObject( Transform parent, out bool isNewObject)
    {
        if (_pool.Count > 0)
        {
            isNewObject = false;

            return _pool.Dequeue();
        }

        isNewObject = true;

        return CrateObject( parent);
    }

    public void PutObject(T obj)
    {
        _pool.Enqueue(obj);
    }

    private T CrateObject( Transform parent)
    {
        T currentObject = Object.Instantiate(_prefab);
        currentObject.transform.parent = parent;
        currentObject.gameObject.SetActive(false);

        return currentObject;
    }
}
