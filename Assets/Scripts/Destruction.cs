using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    [SerializeField]
    private GameObject red;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 curSize = gameObject.transform.localScale;
        if (curSize.x >= 6)
        {
            Vector2 center = sr.bounds.center;
            Vector2 p1 = new Vector2(center.x, center.y + sr.bounds.size.y/2);
            Vector2 p2 = new Vector2(center.x, center.y - sr.bounds.size.y);
            GameObject o1 = Instantiate(red, p1, Quaternion.identity);
            GameObject o2 = Instantiate(red, p2, Quaternion.identity);
            o1.transform.localScale = new Vector2(curSize.x/2, curSize.y/2);
            o2.transform.localScale = new Vector2(curSize.x / 2, curSize.y / 2);
            Destroy(gameObject);
        }
    }
}
