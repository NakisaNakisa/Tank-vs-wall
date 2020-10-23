using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create pool of game objects with a component T
/// </summary>
public class ObjectPool<T> where T : MonoBehaviour
{
    GameObject objectTemplate = null;
    List<GameObject> pool = new List<GameObject>();
    GameObject parent = null;
    public List<T> GetComponentList { get; } = new List<T>();

    public ObjectPool(GameObject _objectTemplate, int _poolSize, string _poolName = "ObjectPool")
    {
        objectTemplate = _objectTemplate;
        parent = new GameObject();
        parent.name = _poolName;
        for (int i = 0; i < _poolSize; ++i)
        {
            ExtendPool();
        }
    }

    public List<T> GetActiveList()
    {
        List<T> _activeList = new List<T>();
        foreach (var _component in GetComponentList)
        {
            if (_component.gameObject.activeSelf)
                _activeList.Add(_component);
        }
        return _activeList;
    }

    public T GetComponent()
    {
        foreach (T _component in GetComponentList)
        {
            if (!_component.gameObject.activeSelf)
                return _component;
        }
        return ExtendPool().GetComponent<T>();
    }

    public GameObject GetGameObject()
    {
        foreach (GameObject _gameObject in pool)
        {
            if (!_gameObject.activeSelf)
                return _gameObject;
        }
        return ExtendPool();
    }

    public GameObject ExtendPool()
    {
        GameObject _go = Object.Instantiate(objectTemplate);
        _go.transform.SetParent(parent.transform);
        _go.SetActive(false);
        GetComponentList.Add(_go.AddComponent<T>());
        pool.Add(_go);
        return _go;
    }

    public void DeactivatePool()
    {
        foreach (GameObject _gameObject in pool)
        {
            _gameObject.SetActive(false);
        }
    }
}
