using UnityEngine;

public class PitMonster : MonoBehaviour
{
    public bool goLeft = true;
    public float moveSpeed = 2.0f;
    private float direction = -1.0f;
    private Vector3 startingScale;

    // Start is called before the first frame update
    void Start()
    {
        startingScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // going left is along the negative X direction
        if (goLeft)
        {
            direction = -1.0f;
            transform.localScale = startingScale;
        }
        else
        {
            direction = 1.0f;
            transform.localScale = new Vector3(-startingScale.x, startingScale.y, startingScale.z);
        }
        transform.Translate(new Vector3(direction, 0, 0) * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // change the direction we are heading
        goLeft = !goLeft;
    }
}