using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Rada : MonoBehaviour
{
    [SerializeField]
    private float lookRadius = 3;
    UnityEvent<Vector3> gunControl;
    // Start is called before the first frame update
    private void Awake()
    {
        if (gunControl == null)
        {
            gunControl = new UnityEvent<Vector3>();
        }
    }
    
    public void addGunListener(UnityAction<Vector3> listener)
    {
        gunControl.AddListener(listener);
    }
    // Update is called once per frame
    void Update()
    {
        DetectEnemy();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
    [SerializeField]
    private LayerMask enemy;
    private void DetectEnemy()
    {
        //Debug.Log("Detecting NearestEnemy");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, lookRadius, enemy);
        if (enemies.Length == 0)
            return;
        float minDistance = Vector2.Distance(enemies.ElementAt(0).transform.position, transform.position) ;
        Transform nearestEnemy = enemies.ElementAt(0).transform;
        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            if (distance < minDistance)
            {
                nearestEnemy = enemy.transform;
                minDistance = distance;
            }
        }
        if (nearestEnemy != null)
        {
            try { 
            gunControl.Invoke(nearestEnemy.transform.position);
            }
            catch
            {
                Debug.Log("Object Killed");
            }
        }
    }
}
