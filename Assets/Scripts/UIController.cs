using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Unity.VisualScripting;
using System.IO;
using System;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    [SerializeField]
    GameObject Red, Black;

    public  static  string path = "Assets/Resources/test.txt";
    // Update is called once per frame
    void Update()
    {

    }
    public void SaveGame()
    {
        List<EnemyDB> enemyDBs = new List<EnemyDB>();
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag("Black").ToList();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Red").ToList());
        foreach (GameObject enemy in enemies)
        {
            EnemyDB edb = new EnemyDB();
            int curHp = enemy.GetComponent<EnemyStatus>().getCurHp();
            edb.CurHp = curHp;
            edb.Location = enemy.transform.position;
            edb.Target = enemy.GetComponent<Mover>().getPreviousTarget();
            if (enemy.name.Contains("Black"))
            {
                edb.TypeOfEnemy = "Black";
            }
            else
            {
                edb.TypeOfEnemy = "Red";
            }
            enemyDBs.Add(edb);
        }
        string data = JsonConvert.SerializeObject(enemyDBs, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        Debug.Log("Length:" + enemyDBs.Count);
        FileInfo fi = new FileInfo(path);
        StreamWriter writer = new StreamWriter(fi.Open(FileMode.Truncate));
        writer.WriteLine(data);
        writer.Close();
    }
   
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag("Black").ToList();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Red").ToList());
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        StreamReader reader = new StreamReader(path);
        string data = reader.ReadToEnd();
        
        List<EnemyDB> enemyDBs = JsonConvert.DeserializeObject<List<EnemyDB>>(data);
        if (enemyDBs == null) return;
        foreach (EnemyDB enemy in enemyDBs)
        {
            GameObject go;
            if(enemy.TypeOfEnemy == "Black")
                go = Instantiate(Black,enemy.Location, Quaternion.identity);
            else
                go = Instantiate(Red, enemy.Location, Quaternion.identity);
            go.GetComponent<Mover>().setPreviousTarget(enemy.Target);
            go.GetComponent<EnemyStatus>().takeDamage(10 - enemy.CurHp);
        }
    }
    
}
