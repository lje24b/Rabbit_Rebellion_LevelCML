using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private bool didCountKey = false;

    void Start()
    {
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !didCountKey)
        {
            didCountKey = true;
            gameManager.numberOfKeys++;

            //play sounds
            gameManager.audioSource.clip = gameManager.keyClip;
            gameManager.audioSource.Play();

            Destroy(gameObject);
        }
    }
}
