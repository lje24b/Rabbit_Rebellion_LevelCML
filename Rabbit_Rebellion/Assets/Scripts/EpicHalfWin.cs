using UnityEngine;

public class EpicHalfWin : MonoBehaviour
{
    GameManager gameManager;
    private bool hasTriggered = false;
    public Color endColor = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !hasTriggered)
        {
            hasTriggered = true;
            gameManager.spawnPoint = this.transform.position;

            GetComponent<SpriteRenderer>().color = endColor;
        }
    }
}