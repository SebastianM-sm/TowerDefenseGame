using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;



/*This is Michael. I put all my contact information in the Trello. 
I will be checking them regualarly, so if you need anything, anything at all just message.*/




public class EnemyHandler : MonoBehaviour
{

    public static EnemyHandler Instance;

    public GameObject enemy;

    public List<Enemy> enemies = new List<Enemy>();

    public Transform turret;

    public float enemyDamage = 1;
    float baseEnemyDamage = 1;
    public float enemyMaxHealth = 1;
    float baseEnemyMaxHealth = 1;
    [SerializeField] int currentWave = 0;

    public int score = 0;

    public TextMeshProUGUI uiText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
        //StartCoroutine(SpawnEnemy(RandomSpawnVector(), 30, 1));
        currentWave = 0;
    }

    // private Vector3 RandomSpawnVector()
    //   {
    //      Vector3 spawnVector = new Vector3(Random.Range(-10.3f, -9.2f), Random.Range(-5.3f, 5.3f));
    //       return spawnVector;
    //  }

    // edit by op, original code is commented out above.

    private Vector3 RandomSpawnVector()
    {
        Vector3 spawnVector1 = new Vector3(-6.4f,0f);
        Vector3 spawnVector2 = new Vector3(3f,0f);
        Vector3 spawnVector3 = new Vector3(-1.66f,4.6f);
        Vector3 spawnVector4 = new Vector3(-1.66f,-4.6f);

        var spawnList = new List<Vector3> { spawnVector1, spawnVector2, spawnVector3, spawnVector4 };

        Vector3 spawnVector = spawnList[Random.Range(0,spawnList.Count)];

        return spawnVector;
    }

    IEnumerator SpawnEnemy(Vector3 spawn, int numberOfEnemies, int timeBetweenSpawn)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            CalcEnemyHealthDamage();
            GameObject newEnemy = Instantiate(enemy, spawn, Quaternion.identity);
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            enemyScript.turret = turret;
            enemies.Add(enemyScript);
            spawn = RandomSpawnVector();
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    private void CalcEnemyHealthDamage()
    {
        if (currentWave == 1)
        {
            enemyDamage = 1;
            enemyMaxHealth = 1;
        }
        else
        {
            enemyDamage = baseEnemyDamage * (currentWave * 1.05f);
            enemyMaxHealth = baseEnemyMaxHealth * (currentWave * 1.05f);
        }
    }
    // Wave start - added by op, may be changed later.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
        
            StartCoroutine(SpawnEnemy(RandomSpawnVector(), 30, 1));
        }
    }

    public void updateScore()
    {
        score++;
        uiText.text = $"Score: {score}";
    }

    //Returns the enemy closest to the supplied position
    /*public Enemy GetClosestEnemy(Vector2 position)
    {
        //Used to keep track of the closest enemy found while searching
        float closestDistance = Mathf.Infinity;

        //Stores the closest enemy found so far
        Enemy ClosestEnemy = null;

        //Checks every active enemy 
        foreach (Enemy enemy in enemies)
        {
            //Calculates the distance from the supplied position to this enemy
            float distance = Vector2.Distance(position, enemy.transform.position);

            //If this enemy is closer than the current closest, remember it
            if (distance < closestDistance)
            {
                closestDistance = distance;
                ClosestEnemy = enemy;
            }
        }

        //Returns the closest enemy, or null if there aren't any
        return ClosestEnemy;
    }*/
}
