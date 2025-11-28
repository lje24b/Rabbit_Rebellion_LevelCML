using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameManager gameManager; // assign in Inspector (preferred)
    private bool didCountSeed = false;

    void Start()
    {
        // Auto-find the scene GameManager if not assigned in the inspector
        if (gameManager == null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("PickUp: GameManager not assigned and not found in scene!");
            }
            else
            {
                Debug.Log("PickUp: auto-assigned GameManager -> " + gameManager.name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !didCountSeed)
        {
            didCountSeed = true;

            if (gameManager != null)
            {
                gameManager.AddSeed(); // use the GameManager method to update count + UI
                Debug.Log("PickUp: added seed. New total: " + gameManager.numberOfSeeds);
            }
            else
            {
                Debug.LogWarning("PickUp: attempted to add seed but gameManager is null.");
            }

            // play sfx via global SoundManager
            SoundManagerScript.PlaySound("seedPickUp");

            Destroy(gameObject);
        }
    }
}
