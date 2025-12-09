using UnityEngine;

public class SpikeAttack : MonoBehaviour
{
    [Header("Spike Prefab")]
    public GameObject spikePrefab;

    [Header("Spawn Points")]
    public Transform[] spikeSpawnPoints;

    [Header("Optional Settings")]
    public float spikeLifetime = 3f;

    public void PerformAttack()
    {
        if (spikePrefab == null)
        {
            Debug.LogWarning("SpikeAttack: No spikePrefab assigned.");
            return;
        }

        foreach (Transform point in spikeSpawnPoints)
        {
            GameObject spike = Instantiate(spikePrefab, point.position, point.rotation);

            if (spikeLifetime > 0)
                Destroy(spike, spikeLifetime);
        }

        Debug.Log("Boss performed Spike Attack.");
    }
}
