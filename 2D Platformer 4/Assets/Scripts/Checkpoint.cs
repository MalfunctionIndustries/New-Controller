using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public Vector3 spawnPoint;

    public Vector3 getSpawnPoint()
    {
        return transform.position + spawnPoint;
    }

    void OnDrawGizmos()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = Color.red;
            float size = 0.3f;

            Vector3 globalWaypointPosition = transform.position + spawnPoint;

           Gizmos.DrawLine(globalWaypointPosition - Vector3.up * size, globalWaypointPosition + Vector3.up * size);
           Gizmos.DrawLine(globalWaypointPosition - Vector3.left * size, globalWaypointPosition + Vector3.left * size);
        }
    }

}
