using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    private ObjectPool<T> _pool = new ObjectPool<T>();
    private List<T> _activeObjects = new List<T>();

    public T Spawn(T prefab, Vector2 spawnPosition, Transform parent, out bool isNewObject )
    {
        T currentObject = _pool.GetObject(prefab, parent, out isNewObject);
        _activeObjects.Add(currentObject);
        currentObject.transform.position = spawnPosition;

        return currentObject;
    }

    public void ReturnObjectInPool(T returnedObject)
    {
        _activeObjects.Remove(returnedObject);
        _pool.PutObject(returnedObject);
    }

    public void CleanActiveObject()
    {
        if (_activeObjects.Count > 0)
        {
            for (int i = _activeObjects.Count - 1; i >= 0; i--)
            {
                T currentObject = _activeObjects[i];

                currentObject.gameObject.SetActive(false);
                ReturnObjectInPool(currentObject);
            }
        }
    }
}
