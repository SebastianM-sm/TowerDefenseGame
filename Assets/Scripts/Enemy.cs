using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    float moveSpeed = 1f;

    float directionChangeTime = 2f;

    Vector2 moveDirection;

    float directionTimer;

    int maxEnemyHelath = 1;
    int currentEnenmyHealth;

    public Transform turret;

    float attackDistanceEnemy = 1f;
    float attackDamageEnemy = 1f;

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
        currentEnenmyHealth = maxEnemyHelath;
        PickNewDirection();
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


    void PickNewDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        directionTimer = Random.Range(1f, directionChangeTime);
    }

    public void TakeDamageEnemy(int damage)
    {
        currentEnenmyHealth -= damage;

        if (currentEnenmyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
