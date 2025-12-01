using UnityEngine;

public class Pew_Pew : MonoBehaviour
{
    [Header("Bullet settings")]
    public GameObject bullet;
    [Tooltip("Drag the fire point GameObject here (child of player).")]
    public GameObject firePoint;

    [Header("Animation")]
    private Animator animator;

    [Header("Firing")]
    public bool fireForward = true;
    public float bulletForce = 1500.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("[Pew_Pew] Animator not found on " + gameObject.name + ". If you want attack animation, add an Animator or assign one.");
        }

        if (firePoint == null)
        {
            Transform fp = transform.Find("FirePoint");
            if (fp != null) firePoint = fp.gameObject;
        }

        if (firePoint == null)
        {
            Debug.LogWarning("[Pew_Pew] firePoint not assigned on " + gameObject.name + ". Create a child GameObject named 'FirePoint' or drag one into the Inspector.");
        }

        if (bullet == null)
        {
            Debug.LogError("[Pew_Pew] bullet prefab is NOT assigned on " + gameObject.name + ". Assign a bullet prefab in the Inspector.");
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            fireForward = true;
        }
        else if (horizontalInput < 0)
        {
            fireForward = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (animator != null)
            {
                SoundManagerScript.PlaySound("playerAttack");
                animator.SetTrigger("doAttack");
            }

            FireBullet();
        }
    }

    void FireBullet()
    {
        if (bullet == null)
        {
            Debug.LogWarning("[Pew_Pew] Cannot fire: bullet prefab is null.");
            return;
        }
        if (firePoint == null)
        {
            Debug.LogWarning("[Pew_Pew] Cannot fire: firePoint is null.");
            return;
        }

        GameObject newBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);

        Rigidbody2D tempRigidBody = newBullet.GetComponent<Rigidbody2D>();
        if (tempRigidBody == null)
        {
            Debug.LogWarning("[Pew_Pew] Instantiated bullet has no Rigidbody2D. Add one to the bullet prefab for physics-based movement.");
        }
        else
        {
            Vector2 direction = firePoint.transform.right;
            if (!fireForward) direction = -direction;

            tempRigidBody.AddForce(direction.normalized * bulletForce);
        }
        Destroy(newBullet, 2.0f);
    }
}
