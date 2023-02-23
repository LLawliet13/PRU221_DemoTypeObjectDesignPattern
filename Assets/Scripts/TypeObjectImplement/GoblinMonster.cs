using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMonster : MonoBehaviour
{
    [SerializeField]
    private int ATK;
    [SerializeField]
    private int HP;

    public GoblinType type;
    public void Start()
    {
        ATK = type.atk;
        HP = type.hp;   
    }


    // Update is called once per frame
    void Update()
    {
        
    }


}
