using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int numberOfKeys = 0;
    public int numberOfSeeds = 0;

    public TextMeshProUGUI keyText;
    public TextMeshProUGUI seedText;

    public AudioSource audioSource;
    public AudioClip coinClip; // optional: assign a clip for game-manager sfx

    public Vector3 spawnPoint;

    public int numberOfPickups = 0;
    public int numberOfLives = 3;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    void Awake()
    {
        // Ensure AudioSource exists on this GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        numberOfSeeds = 0;    
        numberOfKeys = 0;
        numberOfPickups = 0;
        numberOfLives = 3;

        // Initialize UI once at start
        UpdateUI();

        // Helpful warnings for missing references
        if (keyText == null) Debug.LogWarning("GameManager: keyText not assigned in Inspector.");
        if (seedText == null) Debug.LogWarning("GameManager: seedText not assigned in Inspector.");

        spawnPoint = new Vector3(0, 0, 0);
    }

    // Call this whenever counts change
    public void UpdateUI()
    {
        if (keyText != null) keyText.text = "Keys Found: " + numberOfKeys;
        if (seedText != null) seedText.text = "Seeds Stolen: " + numberOfSeeds;

        if (scoreText != null) scoreText.text = "Keys: " + numberOfPickups;
        if (livesText != null) livesText.text = "Lives: " + numberOfLives;
    }

    public void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + numberOfLives;
    }

    public void AddSeed(int amount = 1)
    {
        numberOfSeeds += amount;
        UpdateUI();
    }

    public void AddKey(int amount = 1)
    {
        numberOfKeys += amount;
        UpdateUI();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        if (audioSource == null)
        {
            Debug.LogWarning("GameManager: audioSource missing when trying to play SFX.");
            return;
        }
        audioSource.PlayOneShot(clip);
    }

    // -----------------------------
    // NEW: Life Management & Respawn
    // -----------------------------
    public void LoseLife(GameObject player)
    {
        numberOfLives--;
        UpdateLivesUI(); // update UI immediately

        if (numberOfLives <= 0)
        {
            // Show 0 lives for a moment, then reload scene
            StartCoroutine(ReloadSceneAfterDelay(0.5f));
        }
        else
        {
            // Respawn player at spawn point
            player.transform.position = spawnPoint;

            // Reset velocity if Rigidbody2D exists
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        numberOfLives = 3; // reset lives
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
