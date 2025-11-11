using UnityEngine;

public class EnemyDeath : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // lets check to see if it was a BULLET we intersected with...
        if (other.gameObject.tag == "Bullet")
        {
            // destroy bullet so it doesn't keep hitting everyone else
            Destroy(other.gameObject);

            if (animator != null)
            {
                animator.SetTrigger("gotKilled");

                // give enough time to play animation, THEN delete self
                Destroy(gameObject, 0.5f);
            }
            else
            {
                // no animation to play, just delete self right away
                Destroy(gameObject);
            }
        }
    }
}
