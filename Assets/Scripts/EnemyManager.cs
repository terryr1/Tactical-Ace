using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnitySampleAssets.CrossPlatformInput;
using UnitySampleAssets.Utility;

public class EnemyManager : MonoBehaviour {

    
	public GameObject ship;
	public float spawnTime = 3f;
    int numEnemies=0;
	public Transform[] spawnPoints;
    public Vector3 randomSpawnPoints;
    //public int timer=0;
    float x;
    float y;
    float z;
    Enemy enemy;
    GameObject[] Enemies;
    GameObject[] enemyColor;
    Color[] colorChoices =  new Color[8];
    bool yes;
    int num = 0;
    Color color;

    // Update is called once per frame
    
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
       
        colorChoices[0] = Color.black;
        colorChoices[1] = Color.cyan;
        colorChoices[2] = Color.gray;
        colorChoices[3] = Color.green;
        colorChoices[4] = Color.grey;
        colorChoices[5] = Color.magenta;
        colorChoices[6] = Color.white;
        colorChoices[7] = Color.blue;
    }
	
	// Update is called once per frame
    void Update()
    {
        
        if (ScoreManager.score % 1000 != 0)
            yes = true;
        spawnTime = spawnTime-.01f;
        
        if (GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            numEnemies = Enemies.Length;
        }
        
        if(ScoreManager.score % 1000 == 0 && ScoreManager.score > 0 && yes == true)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<Enemy>();
            enemyColor = GameObject.FindGameObjectsWithTag("EnemyColor");
            enemy.enemyStats.Health += 50;
            
            if (ScoreManager.score % 5000 == 0 && (enemy.enemyStats.shooter ==false ||enemy.enemyStats.shooterOne == false || enemy.enemyStats.shooterTwo == false))
            {
                num = Random.Range(0, 3);
                Debug.Log(num);
                if (num == 0)
                    enemy.enemyStats.shooter = true;
                if (num == 1)
                    enemy.enemyStats.shooterOne = true;
                if (num == 2)
                    enemy.enemyStats.shooterTwo = true;
                enemy.enemyStats.Health = 100;
            }
            color = colorChoices[Random.Range(0, (colorChoices.Length))];
            for (int i = 0; i < enemyColor.Length; i++)
                enemyColor[i].GetComponent<Renderer>().material.color = color;
            yes = false;
        }
    }

    
	void Spawn () {
        x = Random.Range(-40, 40);
        z = Random.Range(-16, 16);
        y = Random.Range(1, 2); ;
        randomSpawnPoints = new Vector3(x, y, z);
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        if(numEnemies<6)
		    Instantiate (ship, randomSpawnPoints, spawnPoints[spawnPointIndex].rotation);

	}
    
}
