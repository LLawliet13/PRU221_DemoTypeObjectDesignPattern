using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnUsingObjectPool : MonoBehaviour
{

    private int numberOfGameObject;
    float previousTime = 0;
    float timeToNextRespawn = 1f;
    ObjectPool<GameObject> pool;
    private void Start()
    {
        PoolSample poolEx = gameObject.GetComponent<PoolSample>();
        poolEx.poolType = PoolSample.PoolType.Stack;
        poolEx.baseHole = transform.position;
        numberOfGameObject = poolEx.maxPoolSize;
        pool = poolEx.Pool;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > previousTime)
        {
            if (pool.CountAll < numberOfGameObject||pool.CountInactive>0)
            {
                pool.Get();
                previousTime = Time.time + timeToNextRespawn;
            }
            //else
            //{
            //    GameObject a = pool.Get();
            //    pool.Release(a);
            //    previousTime = Time.time + timeToNextRespawn;
            //}
            
        }
    }

}
