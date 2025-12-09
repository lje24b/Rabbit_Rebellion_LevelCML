using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    [Header("Laser Object / Beam")]
    public GameObject laserBeamObject;

    [Header("Timing")]
    public float laserDuration = 1.5f;

    public void PerformAttack()
    {
        if (laserBeamObject == null)
        {
            Debug.LogWarning("LaserAttack: No laserBeamObject assigned.");
            return;
        }

        // Activate beam
        laserBeamObject.SetActive(true);
        Debug.Log("Boss fired Laser Attack!");

        // Disable after duration
        Invoke(nameof(DisableLaser), laserDuration);
    }

    private void DisableLaser()
    {
        if (laserBeamObject != null)
            laserBeamObject.SetActive(false);
    }
}
