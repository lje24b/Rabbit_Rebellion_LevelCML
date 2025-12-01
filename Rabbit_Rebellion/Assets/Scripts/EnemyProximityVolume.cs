using UnityEngine;

public class EnemyProximityVolume : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float maxDistance = 8f;  // beyond this it's silent
    [SerializeField] private float minDistance = 1f;  // full volume within this
    private AudioSource audioSrc;

    void Awake()
    {
        audioSrc = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSrc.playOnAwake = false;
        audioSrc.loop = true;
        audioSrc.clip = clip;
        if (player == null) player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Start()
    {
        if (clip != null) audioSrc.Play();
    }

    void Update()
    {
        if (player == null) return;
        float dist = Vector2.Distance(transform.position, player.position);
        float t = Mathf.InverseLerp(maxDistance, minDistance, dist); // 0 at maxDistance -> 1 at minDistance
        audioSrc.volume = Mathf.Clamp01(t);
    }
}
