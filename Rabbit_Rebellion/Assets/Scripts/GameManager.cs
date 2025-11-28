using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int numberOfKeys = 0;
    public int numberOfSeeds = 0;

    public TextMeshProUGUI keyText;
    public TextMeshProUGUI seedText;

    public AudioSource audioSource;
    public AudioClip coinClip; // optional: assign a clip for game-manager sfx

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
        // Initialize UI once at start
        UpdateUI();

        // Helpful warnings for missing references
        if (keyText == null) Debug.LogWarning("GameManager: keyText not assigned in Inspector.");
        if (seedText == null) Debug.LogWarning("GameManager: seedText not assigned in Inspector.");
    }

    // Call this whenever counts change
    public void UpdateUI()
    {
        if (keyText != null) keyText.text = "Keys Found: " + numberOfKeys;
        if (seedText != null) seedText.text = "Seeds Stolen: " + numberOfSeeds;
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

    // Optional helper to play sfx
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
}
