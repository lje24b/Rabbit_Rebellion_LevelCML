using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerAttackSound, robot1Sound, robot2Sound, robot3Sound, enemyDeathSound,
        seedPickUpSound, keyPickUpSound;
    static AudioSource audioSrc;

    void Start()
    {
        playerAttackSound = Resources.Load<AudioClip>("playerAttack");
        robot1Sound = Resources.Load<AudioClip>("robot1");
        robot2Sound = Resources.Load<AudioClip>("robot2");
        robot3Sound = Resources.Load<AudioClip>("robot3");
        enemyDeathSound = Resources.Load<AudioClip>("enemyDeath");
        seedPickUpSound = Resources.Load<AudioClip>("seedPickUp");
        keyPickUpSound = Resources.Load<AudioClip>("keyPickUp");

        audioSrc = GetComponent<AudioSource>();
        if (audioSrc == null)
        {
            audioSrc = gameObject.AddComponent<AudioSource>();
            audioSrc.playOnAwake = false;
        }
    }

    public static void PlaySound(string clipName)
    {
        if (audioSrc == null)
        {
            Debug.LogWarning("[SoundManager] AudioSource missing when trying to play: " + clipName);
            return;
        }

        AudioClip clipToPlay = null;

        switch (clipName)
        {
            case "playerAttack":
                clipToPlay = playerAttackSound;
                break;
            case "robot1":
                clipToPlay = robot1Sound;
                break;
            case "robot2":
                clipToPlay = robot2Sound;
                break;
            case "robot3":            
                clipToPlay = robot3Sound;
                break;
            case "enemyDeath":
                clipToPlay = enemyDeathSound;
                break;
            case "seedPickUp":
                clipToPlay = seedPickUpSound;
                break;
            case "keyPickUp":
                clipToPlay = keyPickUpSound;
                break;
            default:
                Debug.LogWarning("[SoundManager] Unknown clip name: " + clipName);
                break;
        }

        if (clipToPlay != null)
        {
            audioSrc.PlayOneShot(clipToPlay);
        }
        else
        {
            Debug.LogWarning("[SoundManager] Clip not found for: " + clipName);
        }
    }
}
