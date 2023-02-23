using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeObject : MonoBehaviour,ICloneable
{
    // Start is called before the first frame update
    public GameObject prefab;

    public object Clone()
    {
        PrototypeObject NEW = new PrototypeObject();
        return NEW;
    }
    private void OnEnable()
    {
        
    }
    void Start()
    {
        Debug.Log("Instantiated");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
