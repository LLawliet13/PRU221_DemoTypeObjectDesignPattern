using Assets.Scenes.New_Folder;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// Spawner.cs
public class Spawner : MonoBehaviour
{
    public Enemy enemyPrototype;
    public int spawnCount;
    public Transform previous;
    public GameObject sword1;
    private void Start()
    {

        Sword sword = Instantiate(sword1).AddComponent<Sword>();
        Debug.Log(sword.atk);
        Instantiate(sword.clone());
    }
}
