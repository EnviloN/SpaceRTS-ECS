using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float gizmoRadius = 2f;
    /// <summary>
    /// Draw a white wire sphere at the transform's position
    /// </summary>
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }
}
