using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameManager GameManager; // assign in Inspector (preferred)
    private bool didCountSeed = false;

    void Start()
    {
        // Auto-find the scene GameManager if not assigned in the inspector
        if (GameManager == null)
        {
            GameManager = FindAnyObjectByType<GameManager>();
            if (GameManager == null)
            {
                Debug.LogError("PickUp: GameManager not assigned and not found in scene!");
            }
            else
            {
                Debug.Log("PickUp: auto-assigned GameManager -> " + GameManager.name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !didCountSeed)
        {
            didCountSeed = true;

            if (GameManager != null)
            {
                GameManager.AddSeed(); // use the GameManager method to update count + UI
                Debug.Log("PickUp: added seed. New total: " + GameManager.numberOfSeeds);
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
