using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 2f;

    public float directionChangeTime = 2f;

    private Vector2 moveDirection;

    private float directionTimer;

    public int maxHelath = 1;
    private int currentHealth;

    public Transform turret;

    public float attackDistance = 1f;

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
        currentHealth = maxHelath;
        PickNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, turret.position);

        if(distance <= attackDistance)
        {
            return;
        }

        Vector2 direction = (turret.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    void PickNewDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        directionTimer = Random.Range(1f, directionChangeTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
