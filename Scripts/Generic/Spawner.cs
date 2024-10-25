﻿using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> where T : MonoBehaviour
{
    private ObjectPool<T> _pool;
    private List<T> _activeObjects = new List<T>();

    public Spawner(T prefab)
    {
        _pool = new ObjectPool<T>(prefab);
    }

    public T Spawn(Vector2 spawnPosition, Transform parent, out bool isNewObject)
    {
        T currentObject = _pool.GetObject(parent, out isNewObject);
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
