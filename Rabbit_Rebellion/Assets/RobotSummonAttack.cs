using UnityEngine;

public class RobotSummonAttack : MonoBehaviour
{
    [Header("Robot Prefab")]
    public GameObject robotPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("How Many Robots?")]
    public int robotsToSpawn = 3;

    public void PerformAttack()
    {
        if (robotPrefab == null)
        {
            Debug.LogWarning("RobotSummonAttack: No robotPrefab assigned.");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("RobotSummonAttack: No spawnPoints assigned.");
            return;
        }

        for (int i = 0; i < robotsToSpawn; i++)
        {
            Transform point = spawnPoints[i % spawnPoints.Length];
            Instantiate(robotPrefab, point.position, point.rotation);
        }

        Debug.Log("Boss summoned robots!");
    }
}
