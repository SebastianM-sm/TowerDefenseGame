using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage = 1;
    float speed = 40f;

    private Vector2 moveDirection; 

    public float lifeTime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy == null)
            return;

        enemy.TakeDamageEnemy(damage);

        Destroy(gameObject);
    }
}
