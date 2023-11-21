using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get { return instance; }
    }
    [SerializeField]
    public bool canGrow = true;
    [SerializeField]
    public GameObject pooledObject;

    [SerializeField]
    private int objectCount;

    [SerializeField]
    public List<GameObject> gameObjects;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameObjects = new List<GameObject>();

        for (int i = 0; i < objectCount; i++)
        {
            GameObject Obj = Instantiate(pooledObject);
            Obj.SetActive(false);
            Obj.transform.SetParent(transform);
            gameObjects.Add(Obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach(GameObject @object in gameObjects)
        {
            if (!@object.activeInHierarchy)
            {
                return @object;
            }
        }

        if(canGrow == true)
        {
            Debug.LogError("Object Pool " + pooledObject.name + " is not big enough - concider increasing the initial size of the pool - a new object has been instantiated and added to the object pool.");
            GameObject Obj = Instantiate(pooledObject);
            Obj.SetActive(false);
            Obj.transform.SetParent(transform);
            gameObjects.Add(Obj);
            return Obj;
        }
        return null;
    }
}
