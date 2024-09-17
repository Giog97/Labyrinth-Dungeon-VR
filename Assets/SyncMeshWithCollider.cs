using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncMeshWithCollider : MonoBehaviour
{
    // Non serve assegnare nulla se Mesh Filter è sullo stesso GameObject
    private void Update()
    {
        // Sincronizza la posizione e la rotazione
        transform.position = GetComponent<Collider>().transform.position;
        transform.rotation = GetComponent<Collider>().transform.rotation;
    }
}

