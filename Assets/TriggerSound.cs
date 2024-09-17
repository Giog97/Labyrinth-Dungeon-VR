using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioSource audioSource;  // L'AudioSource che riprodurrà il suono
    private bool hasPlayed = false;  // Per assicurarsi che il suono venga riprodotto una sola volta

    private void OnTriggerEnter(Collider other)
    {
        // Assicurati che solo il giocatore attivi il trigger (puoi controllare con tag o altro)
        if (other.CompareTag("Player") && !hasPlayed)
        {
            audioSource.Play();  // Riproduci il suono
            hasPlayed = true;    // Assicurati che il suono non venga riprodotto di nuovo
        }
    }
}

