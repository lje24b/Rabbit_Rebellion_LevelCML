using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int numberOfKeys = 0;
    public int numberOfSeeds = 0;

    public TextMeshProUGUI keyText;
    public TextMeshProUGUI seedText;

    public AudioSource audioSource;
    public AudioClip keyClip;
    public AudioClip coinClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        keyText.text = "Keys Found: " + numberOfKeys;

        seedText.text = "Seeds Stolen: " + numberOfSeeds;
    }
}