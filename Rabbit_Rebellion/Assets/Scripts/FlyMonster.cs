using UnityEngine;

public class EnemyVerticalMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;      // How fast it moves up and down
    [SerializeField] private float moveDistance = 3f;   // How far up/down from start point

    private Vector2 startPos;
    private bool movingUp = true;

    void Start()
    {
        // Record the starting position to move relative to it
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate new position
        if (movingUp)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            // Check if reached top limit
            if (transform.position.y >= startPos.y + moveDistance)
                movingUp = false;
        }
        else
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;

            // Check if reached bottom limit
            if (transform.position.y <= startPos.y - moveDistance)
                movingUp = true;
        }
    }
}