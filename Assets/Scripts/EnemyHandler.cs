using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class EnemyHandler : MonoBehaviour
{
    public static EnemyHandler Instance;

    public GameObject enemy;

    public List<Enemy> enemies = new List<Enemy>();

    public Transform turret;

 


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Vector3 spawnVector = new Vector3(Random.Range(-17f, -10f), Random.Range(-9.2f, 9.2f));
        /*for (int i = 0; i < 30; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-17f, -10f), Random.Range(-9.2f, 9.2f), 0);
            GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            enemyScript.turret = turret;
            enemies.Add(enemyScript);
        }*/
        SpawnEnemy(spawnVector, 30, 2);
        Debug.Log(enemies.Count);

    }

    void SpawnEnemy(Vector3 spawn, int numberOfEnemies, float timeBetweenSpawn)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemy, spawn, Quaternion.identity);
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            enemyScript.turret = turret;
            enemies.Add(enemyScript);
            spawn = new Vector3(Random.Range(-17f, -10f), Random.Range(-9.2f, 9.2f));
            StartCoroutine(SpawnTimer(timeBetweenSpawn));
        }
    }

    IEnumerator SpawnTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    void Update()
    {
        
    }

    //Returns the enemy closest to the supplied position
    public Enemy GetClosestEnemy(Vector2 position)
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
    }
}
