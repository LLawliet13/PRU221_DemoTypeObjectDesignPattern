using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static Dictionary<int, GameObject> pairs;
    // Start is called before the first frame update
    void Start()
    {
        pairs = new Dictionary<int, GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        if (pairs.Count > 0)
        {
            for (int i = 0; i < pairs.Count; i += 2)
            {
                try
                {
                    Eat.EatAction(pairs.Values.ElementAt(i), pairs.Values.ElementAt(i + 1));
                }
                catch
                {
                    //catch th cham 3
                    //Debug.Log("alreadyEated");
                }
            }
            pairs = new Dictionary<int, GameObject>();
        }

    }
    public static void EatSelection(GameObject a, GameObject b)
    {
        try
        {
            pairs.Add(a.GetInstanceID(), a);
            pairs.Add(b.GetInstanceID(), b);
        }
        catch
        {
            //Debug.Log("alreadyExist");
        }
    }

}
