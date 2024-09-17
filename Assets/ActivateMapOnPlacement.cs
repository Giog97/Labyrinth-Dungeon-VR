using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateMapOnPlacement : MonoBehaviour
{
    public XRSocketInteractor socket;  // Riferimento al socket dello STAND
    public GameObject mappa;           // Riferimento alla MAPPA

    private void Start()
    {
        // Disattiva la MAPPA all'inizio
        mappa.SetActive(false);

        // Aggiungi un listener all'evento selezione completa del socket
        socket.selectEntered.AddListener(OnObjectPlaced);
    }

    private void OnDestroy()
    {
        // Rimuovi il listener per evitare problemi di memoria
        socket.selectEntered.RemoveListener(OnObjectPlaced);
    }

    // Questo metodo viene chiamato quando la CrystalBall viene inserita nello STAND
    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("CrystalBall"))  // Verifica che l'oggetto sia la SFERA
        {
            // Attiva la MAPPA
            mappa.SetActive(true);
        }
    }
}


