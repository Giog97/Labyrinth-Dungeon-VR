using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAR : MonoBehaviour
{
    public static GameManagerAR Instance; // Singleton per accedere facilmente al GameManager
    public GameObject mapObject; // La mappa da mostrare
    private int targetsHit = 0; // Contatore dei bersagli colpiti
    private int totalTargets = 10; // Numero totale di bersagli

    void Awake()
    {
        // Configurazione del Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Assicurati che la mappa sia nascosta all'inizio
        if (mapObject != null)
        {
            mapObject.SetActive(false);
        }
    }

    // Metodo chiamato ogni volta che un bersaglio viene colpito
    public void TargetHit()
    {
        targetsHit++;
        if (targetsHit >= totalTargets)
        {
            ShowMap(); // Mostra la mappa quando tutti i bersagli sono colpiti
        }
    }

    // Mostra la mappa
    private void ShowMap()
    {
        if (mapObject != null)
        {
            mapObject.SetActive(true); // Rendi visibile la mappa
        }
    }
}

