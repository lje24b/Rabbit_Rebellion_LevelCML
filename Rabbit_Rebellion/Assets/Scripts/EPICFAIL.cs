using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EPICFAIL : MonoBehaviour
{
    GameManager gameManager;
    private bool isColliding = false;

    private void Start()
    {
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding) return;

        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;

            // ↓↓↓ THIS WAS MISSING ↓↓↓
            gameManager.numberOfLives--;
            gameManager.UpdateLivesUI();   // <-- UPDATE THE UI HERE
            // ↑↑↑ ADD THIS LINE ↑↑↑

            if (gameManager.numberOfLives <= 0)
            {
                gameManager.numberOfLives = 3;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                other.gameObject.transform.position = gameManager.spawnPoint;
            }

            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        isColliding = false;
    }
}
