using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    static GameObject red, black;
    public static void EatAction(GameObject a, GameObject b)
    {
        red = black = null;
        if (a.tag == "Red") red = a; else black = a;
        if (b.tag == "Red") red = b; else black = b;
        if (black == null)
        {
            float diff = a.transform.localScale.x - b.transform.localScale.x;
            if (diff == 0)
            {
                if (0.5 > Random.Range(0f, 1f))
                {
                    Eat1st(a, b);
                }
                else
                {
                    Eat1st(b, a);
                }
                return;
            }

            if (diff < 0)
            {
                Eat1st(a, b);
            }
            if (diff > 0)
            {
                Eat1st(b, a);
            }
        }
        else if (red != null)
        {
            Eat1st(black, red);
        }

    }
    // an object thu 1
    static void Eat1st(GameObject a, GameObject b)
    {
        red = b;
        float beingEatScale = a.transform.localScale.x;
        Destroy(a);
        Vector3 size = red.transform.localScale;
        red.transform.localScale = new Vector2(size.x + beingEatScale, size.y + beingEatScale);
    }
    
}
