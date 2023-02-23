using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using UnityEngine.Pool;



public class BulletPoolSample : MonoBehaviour
{

    public Vector3 baseHole;
    public enum PoolType
    {
        Stack,
        LinkedList
    }
 
    
    public PoolType poolType;
    // Collection checks will throw errors if we try to release an item that is already in the pool.
    public bool collectionChecks = false;
    public int maxPoolSize;

    ObjectPool<GameObject> m_Pool;

    public ObjectPool<GameObject> Pool
    {
        get
        {
            m_Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
            return m_Pool;
        }
    }
    [SerializeField]
    GameObject bullet;

    GameObject CreatePooledItem()
    {
        GameObject go;
        Vector2 currentPool = baseHole;

        go = Instantiate(bullet, currentPool, Quaternion.identity);
        if (go.GetComponent<Bullet>() != null)
            go.GetComponent<Bullet>().DestroyEvent.AddListener(DieEvent);
        go.SetActive(false);
        return go;
    }
    private void DieEvent(GameObject go)
    {
        m_Pool.Release(go);
    }

    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(GameObject system)
    {
        system.gameObject.transform.position = baseHole;
        if (system.gameObject.GetComponent<EnemyStatus>() != null)
            system.gameObject.GetComponent<EnemyStatus>().resetHP();
        system.gameObject.SetActive(false);
    }
    int createdGO = 0;
    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(GameObject system)
    {
        //Debug.Log(m_Pool.CountAll + "all");
        //Debug.Log(m_Pool.CountInactive + "inactive");

        system.gameObject.SetActive(true);
        system.transform.position = baseHole;
        if (createdGO <= maxPoolSize)
            createdGO++;
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(GameObject system)
    {
        Destroy(system);
    }


}