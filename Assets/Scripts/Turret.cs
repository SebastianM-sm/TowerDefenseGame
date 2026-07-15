//Copyright DigiPen Inst. of Technology. All Rights Reserved.

using UnityEngine;

public class Turret : MonoBehaviour
{
    //Used to determine turret range
    public float range = Mathf.Infinity;
    //Used to determine turret fire rate
    public float fireRate = 2f;
    private float fireTimer;

    //Used to spawn in copies of the projectile(s)
    public GameObject projectilePrefab;

    //Turret location, used for calculating direction
    public Transform pointA;
    //The enemy the turret is currently targeting
    private Enemy currentTarget;
    //Used to determine the speed of rotation
    public float rotationSpeed = 90f;

    float maxTurretHealth = 100f;
    float currentTurretHealth;

    public static Turret Instance;


    //Used on the enemy finder to reduce the amount of times per second it runs
    private float searchTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentTurretHealth = maxTurretHealth;
    }

    public void TakeDamageTurret(float damage)
    {
        currentTurretHealth = Mathf.Clamp(currentTurretHealth - damage, 0, maxTurretHealth);

        Debug.Log($"{currentTurretHealth}/{maxTurretHealth}");

        if (currentTurretHealth == 0)
            GameOver();
    }

    private void GameOver()
    {
        Destroy(gameObject);
        Debug.Log("Game over");
    }

    void Update()
    {

        fireTimer -= Time.deltaTime;
        if (fireTimer <=0)
        {
            if (currentTarget != null)
            {
                Fire();
            }
            fireTimer = 1f / fireRate;
        }

        void Fire()
        {
            GameObject projectile = Instantiate(projectilePrefab, pointA.position, transform.rotation);
            projectile.GetComponent<Projectile>()
                .SetTarget(currentTarget);

            float distance = Vector2.Distance(pointA.position, currentTarget.transform.position);

            if (distance > range)
            {
                currentTarget = null;
            }
        }

        //As mentioned before this is used to help the game run better
        searchTimer -= Time.deltaTime;

        if (searchTimer <= 0f)
        {
            searchTimer = 0.2f; //This means it searches 5 times every second instead of 60

            currentTarget = EnemyHandler.Instance.GetClosestEnemy(pointA.position);

            if (currentTarget != null)
            {
                float distance = Vector2.Distance(pointA.position, currentTarget.transform.position);
                
                if (distance > range)
                {
                    currentTarget = null;
                }
            }

        }

        if (currentTarget == null)
        {
            return;
        }

        //Calculates the vector from the turret to the targeted enemy
        Vector2 vectorToTarget = (Vector2)currentTarget.transform.position - (Vector2)pointA.position;
        //Calculates the magnitude of the movement vector then assigns it to a variable. sqrt((X^2)+(Y^2))
        float distanceToTarget = vectorToTarget.magnitude;
        //Normalizes the vectors so they can be used for direction. (X/magniude, Y/magnitude)
        Vector2 direction = vectorToTarget.normalized;

        /*Calculates the angle the turret needs to face to point towards the target (in radians), 
        it then converts from radians to degrees (on the Z-axis)*/
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90;
        //Puts the angle the turret is currently facing into a variable.
        float currentAngle = transform.eulerAngles.z;

        /*Mathf.Lerp(A,B,T). Mathf.Lerp sets A as a starting point and moves by an increment the size of T, until it reaches B. 
        Mathf.LerpAngle does the same but makes sure it works correctly for when it wraps around 360 degrees*/
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, 0.1f);
        //Sets the roation of the turret to the value of newAngle. It only affects the Z-axis, this is because in 2d the Z-axis is the only one we can see changing.
        transform.eulerAngles = new Vector3(0, 0, newAngle);


    }
}
