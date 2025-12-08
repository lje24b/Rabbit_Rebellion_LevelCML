using UnityEngine;
using System.Collections;

public class BossLaser : MonoBehaviour
{
    public LineRenderer line;
    public Transform firePoint;
    public float maxDistance = 15f;
    public float laserDuration = 7f;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    public void Fire()
    {
        StartCoroutine(FireLaser());
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;

        Vector3 start = firePoint.position;
        Vector3 dir = firePoint.right;

        RaycastHit2D hit = Physics2D.Raycast(start, dir, maxDistance);

        Vector3 end = hit ? hit.point : start + dir * maxDistance;

        line.SetPosition(0, start);
        line.SetPosition(1, end);

        yield return new WaitForSeconds(laserDuration);

        line.enabled = false;
    }
}
