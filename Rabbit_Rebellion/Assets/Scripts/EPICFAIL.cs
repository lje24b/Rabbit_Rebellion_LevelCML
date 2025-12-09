using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EPICFAIL : MonoBehaviour
{
    [Header("Assign GameManager here or it will try to find Canvas")]
    [SerializeField] private GameManager gameManager;

    [Header("Collision Settings")]
    private bool isColliding = false;

    private void Start()
    {
        // Try to find GameManager if not assigned
        if (gameManager == null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                gameManager = canvas.GetComponent<GameManager>();
                if (gameManager == null)
                {
                    Debug.LogError("EPICFAIL: GameManager component not found on Canvas!");
                }
            }
            else
            {
                Debug.LogError("EPICFAIL: No Canvas found in scene!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding) return;

        if (other.CompareTag("Player"))
        {
            isColliding = true;

            if (gameManager != null)
            {
                gameManager.numberOfLives--;
                gameManager.UpdateLivesUI();

                if (gameManager.numberOfLives <= 0)
                {
                    gameManager.numberOfLives = 3;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    return; // exit early to avoid respawning player below
                }
            }

            // Respawn player at spawn point
            if (gameManager != null && gameManager.spawnPoint != null)
            {
                other.transform.position = gameManager.spawnPoint;
            }

            StartCoroutine(ResetCollision());
        }
    }

    private IEnumerator ResetCollision()
    {
        yield return new WaitForSeconds(1f);
        isColliding = false;
    }
}
