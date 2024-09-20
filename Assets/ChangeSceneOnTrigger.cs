using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    public string sceneName;  // Nome della scena da caricare

    // Rileva l'entrata in un Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto che entra è il Player
        if (other.CompareTag("Player"))
        {
            // Cambia scena
            SceneManager.LoadScene(sceneName);
        }
    }
}

