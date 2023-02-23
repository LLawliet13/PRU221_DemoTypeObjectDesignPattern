using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomLocation : MonoBehaviour
{
    static LinkedList<Vector2> basePositions ;
    [SerializeField]
    GameObject baseHole;
    //[SerializeField]
    //List<int> numberGOonHole;
    private void Start()
    {
        Bounds bound = OrthographicBounds(Camera.main);
        //basePositions = new LinkedList<Vector2>();
        //basePositions.AddLast(bound.min);
        //basePositions.AddLast(new Vector2(bound.min.x,bound.max.y));
        //basePositions.AddLast(bound.max);
        //basePositions.AddLast(new Vector2(bound.max.x, bound.min.y));

        //basePositions.AddLast(bound.center - new Vector3(bound.extents.x,0));
        //basePositions.AddLast(bound.center + new Vector3(bound.extents.x,0));
        //basePositions.AddLast(bound.center - new Vector3(0,bound.extents.y ));
        //basePositions.AddLast(bound.center + new Vector3(0,bound.extents.y ));


        //for(int i= 0; i < basePositions.Count; i++)
        //{
        //    Instantiate(baseHole, basePositions.ElementAt(i), Quaternion.identity);
            
        //}
    }
    public static Vector3 randomTarget()
    {
        Bounds bound = OrthographicBounds(Camera.main);
        Vector3 spawnDestination = new Vector2(Random.Range(bound.min.x, bound.max.x), Random.Range(bound.min.y, bound.max.y));
        return spawnDestination;
    }

    public static Vector3 randomBoundTarget()
    {
        Bounds bound = OrthographicBounds(Camera.main);
        Vector3 spawnDestination = basePositions.ElementAt(Random.Range(0, 7));
        return spawnDestination;
    }
    private static Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}
