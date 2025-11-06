using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PickUp : MonoBehaviour
{    
    [SerializeField] GameManager gameManager;
    private bool didCountCoin = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !didCountCoin)
        {
            didCountCoin = true;
            gameManager.numberOfCoins++;

            // play sounds
            gameManager.audioSource.clip = gameManager.coinClip;
            gameManager.audioSource.Play();


            Destroy(gameObject);
        }
    }
}