using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private LayerMask enemy;
    [SerializeField]
    private int ATK;
    private float timeToDestroy;
    private void Start()
    {
        if (ATK == 0) ATK = 5;

    }
    public UnityEvent<GameObject> DestroyEvent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
        EnemyStatus es = collision.gameObject.GetComponent<EnemyStatus>();
        if(es != null)
        {
            es.takeDamage(ATK);
            DestroyEvent.Invoke(gameObject);
        }
    }
    public void setTimeDestroy(float time)
    {
        timeToDestroy = time;
    }
    private void Update()
    {
        if(timeToDestroy< Time.time)
        {
            DestroyEvent.Invoke(gameObject);
        }
    }
}
