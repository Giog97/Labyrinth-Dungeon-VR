using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Serve per fare in modo che il cestino funzioni
// Ci dice quando un rigidbody entra in un collider
public class TriggerZone : MonoBehaviour
{
    public string targetTag;
    public UnityEvent<GameObject> onEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            onEnterEvent.Invoke(other.gameObject);
        }
    }
}