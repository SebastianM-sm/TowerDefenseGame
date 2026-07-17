using System;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    float moveSpeed = 1f;

    public float maxEnemyHealth;
    float currentEnemyHealth;

    public Transform turret;

    float attackDistanceEnemy = 1;
    public float attackDamageEnemy;

    public GameObject enemyDamageSound;



    private void OnDestroy()
    {
        if (EnemyHandler.Instance != null)
        {
            EnemyHandler.Instance.enemies.Remove(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxEnemyHealth = EnemyHandler.Instance.enemyMaxHealth;
        currentEnemyHealth = maxEnemyHealth;
        attackDamageEnemy = EnemyHandler.Instance.enemyDamage;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, turret.position);
        /*if(distance <= attackDistanceEnemy)
            return;*/

        Vector2 direction = (turret.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        if (distance <= attackDistanceEnemy)
        {
            Turret.Instance.TakeDamageTurret(attackDamageEnemy);
            Destroy(gameObject);
        }
    }

    public void TakeDamageEnemy(int damage)
    {
        currentEnemyHealth -= damage;

        enemyDamageSound.GetComponent<AudioSource>().Play();

        if (currentEnemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        EnemyHandler.Instance.updateScore();
        Destroy(gameObject);
    }


}
