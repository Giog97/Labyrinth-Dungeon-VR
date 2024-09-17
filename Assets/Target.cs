using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool isHit = false; // Per evitare conteggi multipli sullo stesso bersaglio

    // Metodo chiamato quando il bersaglio viene colpito
    private void OnCollisionEnter(Collision collision)
    {
        if (!isHit && collision.gameObject.CompareTag("Arrow")) // Assicurati che le frecce abbiano il tag "Arrow"
        {
            isHit = true; // Segna il bersaglio come colpito
            GameManagerAR.Instance.TargetHit(); // Notifica al GameManager che un bersaglio è stato colpito
        }
    }
}

