using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Vector3 previousTarget;
    float speed = 2;
    private void Start()
    {
        previousTarget = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x != previousTarget.x && transform.position.y != previousTarget.y)
        {
            transform.position = Vector3.MoveTowards(transform.localPosition, previousTarget, speed * Time.deltaTime);
        }
        else
        {
            previousTarget = RandomLocation.randomTarget();
        }
    }
    public void setPreviousTarget(Vector3 pt)
    {
        previousTarget = pt;
    }
    public Vector3 getPreviousTarget()
    {
        return previousTarget;
    }
    [SerializeField]
    private bool ableToEat;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("1:" + gameObject.GetInstanceID() + ",2:" + collision.gameObject.GetInstanceID());
        if (ableToEat)
            GameMaster.EatSelection(gameObject, collision.gameObject);
    }

}
