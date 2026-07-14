using UnityEngine;

public class Projectile : MonoBehaviour
{

    Enemy target;
    public int damage = 1;
    public float speed = 12f;

    private Vector2 moveDirection; 

    public void SetTarget(Enemy newTarget)
    {
        target = newTarget;
        moveDirection = (target.transform.position - transform.position).normalized;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy == null)
            return;

        enemy.TakeDamage(damage);

        Destroy(gameObject);
    }
}
