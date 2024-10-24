using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    private Queue<T> _pool = new Queue<T>();

    public T GetObject(T prefab, Transform parent, out bool isNewObject)
    {
        if (_pool.Count > 0)
        {
            isNewObject = false;

            return _pool.Dequeue();
        }

        isNewObject = true;

        return CrateObject(prefab, parent);
    }

    public void PutObject(T obj)
    {
        _pool.Enqueue(obj);
    }

    private T CrateObject(T prefab, Transform parent)
    {
        T currentObject = Instantiate(prefab);
        currentObject.transform.parent = parent;
        currentObject.gameObject.SetActive(false);

        return currentObject;
    }
}
