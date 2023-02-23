using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    GameObject red, black;
    float phantram_red = 0.5f;
    float previousTime = 0;
    float timeToNextRespawn = 1f;
    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > previousTime)
        {
            Vector2 randomTarget = RandomLocation.randomBoundTarget();
            float f = Random.Range(0f, 1f);
            if (phantram_red >= f )
            {
                Instantiate(red, randomTarget, Quaternion.identity);
            }
            else
            {
                Instantiate(black, randomTarget, Quaternion.identity);
            }
            previousTime = Time.time + timeToNextRespawn;
        }
    }
   
}
