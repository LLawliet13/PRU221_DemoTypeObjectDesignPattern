using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;
    private LinkedList<Transform> bulletList;
    // Start is called before the first frame update
    void Start()
    {
        rotationOffset = transform.rotation.z;
        Rada rada = GameObject.FindGameObjectWithTag("Rada").GetComponent<Rada>();
        Debug.Log("Rada Found");
        rada.addGunListener(followEnemy);
        bulletList = new LinkedList<Transform>();
        Reload();

    }
    [SerializeField]
    private float rotationOffset;
    private float timeToShoot = 0;
    [SerializeField]
    private float delayShootTime = 0.5f;
    //[SerializeField]
    //private float ReloadTime = 2.3f;
    [SerializeField]
    private float speedRotation = 70;
    [SerializeField]
    private float allowShootAngle = 1;
    [SerializeField]
    private int bulletNumber = 10;

    Quaternion previousDiff = Quaternion.Euler(0, 0, 0);
    public void followEnemy(Vector3 enemy_location)
    {
        //Vector3 diff = transform.position - enemy_location;
        //diff = diff.normalized;
        //float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rotZ + rotationOffset);
        //Shoot(enemy_location);
        //update
        Vector3 diff = enemy_location - transform.position;
        float curAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - rotationOffset;
        Quaternion q = Quaternion.AngleAxis(curAngle, Vector3.forward);
        float singleStep = speedRotation * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, q, singleStep);
        if (Mathf.Abs(transform.rotation.z - previousDiff.z) * Mathf.Rad2Deg < allowShootAngle)
            Shoot(enemy_location);
        previousDiff = transform.rotation;
    }
    ObjectPool<GameObject> pool;
    public void Reload()
    {

        BulletPoolSample poolEx = gameObject.GetComponent<BulletPoolSample>();
        poolEx.poolType = BulletPoolSample.PoolType.Stack;
        poolEx.baseHole = transform.position;
        poolEx.maxPoolSize = bulletNumber;
        pool = poolEx.Pool;

    }

    void Shoot(Vector2 enemy_location)
    {
        if (timeToShoot <= Time.time)
        {
            if (pool.CountAll < bulletNumber || pool.CountInactive > 0)
            {
                Transform b = pool.Get().transform;
                b.GetComponent<Rigidbody2D>().AddForce((enemy_location - (Vector2)transform.position).normalized * 30, ForceMode2D.Impulse);
                timeToShoot = Time.time + delayShootTime;
                b.GetComponent<Bullet>().setTimeDestroy(Time.time+2);
                //StartCoroutine(DestroyBullet(2, b));
            }

        }
    }
    //private IEnumerator DestroyBullet(float waitTime, Transform bullet)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    pool.Release(bullet.gameObject);
    //}
    // Update is called once per frame
    void Update()
    {

    }
}
