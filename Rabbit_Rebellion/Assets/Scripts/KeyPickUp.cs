using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    [SerializeField] private GameManager gameManager; // assign in Inspector if possible
    private bool didCountKey = false;

    void Start()
    {
        // Auto-find GameManager if not assigned
        if (gameManager == null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("KeyPickUp: GameManager not found in scene!");
            }
            else
            {
                Debug.Log("KeyPickUp: auto-assigned GameManager -> " + gameManager.name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !didCountKey)
        {
            didCountKey = true;

            if (gameManager != null)
            {
                gameManager.AddKey(); // update count & UI safely
                Debug.Log("KeyPickUp: added key. Total keys: " + gameManager.numberOfKeys);
            }
            else
            {
                Debug.LogWarning("KeyPickUp: attempted to add key but gameManager is null.");
            }

            // Play pickup sound
            SoundManagerScript.PlaySound("keyPickUp");

            Destroy(gameObject);
        }
    }
}
