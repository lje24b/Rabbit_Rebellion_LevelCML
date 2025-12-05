using UnityEngine;
using UnityEngine.SceneManagement;
public class EpicWin : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            // write something to the Console just to make 
            // sure this function is being called
            Debug.Log("****#### WE ARE SO AWSOME! ####****");

            // use SceneManager to load the Next scene
            // the LoadScene function just wants a NUMBER of the scene to load
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}