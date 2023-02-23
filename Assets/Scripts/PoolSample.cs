using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using UnityEngine.Pool;



public class PoolSample : MonoBehaviour
{

    public Vector3 baseHole;
    public enum PoolType
    {
        Stack,
        LinkedList
    }
    private SpaceShip[] spaceShips;
    private void Awake()
    {
        spaceShips = Resources.LoadAll<SpaceShip>("SpaceShipType");
        maxPoolSize = spaceShips.Length;
    }
    
    public PoolType poolType;

    // Collection checks will throw errors if we try to release an item that is already in the pool.
    public bool collectionChecks = false;
    [HideInInspector]
    public int maxPoolSize;

    ObjectPool<GameObject> m_Pool;

    public ObjectPool<GameObject> Pool
    {
        get
        {
            m_Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, spaceShips.Length , maxPoolSize);
            return m_Pool;
        }
    }
    [SerializeField]
    GameObject spaceShip;

    int currentSpaceShipIndex = 0;
    GameObject CreatePooledItem()
    {
        GameObject go;
        Vector2 currentPool = baseHole;

        go = Instantiate(spaceShip, currentPool, Quaternion.identity);
        go.GetComponent<EnemyStatus>().setSpaceShipType(spaceShips[currentSpaceShipIndex++]);


        if (go.GetComponent<EnemyStatus>() != null)
            go.GetComponent<EnemyStatus>().DestroyEvent.AddListener(DieEvent);

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
    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(GameObject system)
    {
        system.gameObject.SetActive(true);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(GameObject system)
    {
        Destroy(system);
    }


}