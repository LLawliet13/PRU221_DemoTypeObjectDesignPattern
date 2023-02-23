using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStatus : MonoBehaviour
{
    SpriteRenderer sr;
    Color baseColor;
    [SerializeField]
    private SpaceShip spaceShip;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        if (DestroyEvent == null)
            DestroyEvent = new UnityEvent<GameObject>();
        baseColor = sr.color;

        maxHp = curHp = spaceShip.hp;
        sr.sprite = Resources.Load<Sprite>(spaceShip.avatarPath);
    }
    [SerializeField]
    private int maxHp, curHp ;
    public void takeDamage(int hp)
    {
        curHp -= hp;
        if (curHp <= 0)
        {
            //Destroy(gameObject);
            DestroyEvent.Invoke(gameObject);
        }
        
    }
    public void setSpaceShipType(SpaceShip spaceShip)
    {
        this.spaceShip = spaceShip;
    }
    public UnityEvent<GameObject> DestroyEvent;
    public int getCurHp()
    {
        return curHp;
    }
    public void resetHP()
    {
        curHp = maxHp;
    }
    private void Update()
    {
        sr.color = new Color(baseColor.r, baseColor.g, baseColor.b, (float)curHp / maxHp);
    }

}
