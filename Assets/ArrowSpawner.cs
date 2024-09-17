using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;
    public GameObject notch; // Puto di spawn delle frecce

    private XRGrabInteractable _bow; // Ci dice quando afferiamo un arco o no
    private bool _arrowNotched = false; // Serve per non far spawnare frecce continuamente
    private GameObject _currentArrow = null;

    // Start is called before the first frame update
    void Start()
    {
        _bow = GetComponent<XRGrabInteractable>();
        PullInteraction.PullActionReleased += NotchEmpty; // Se Notch è Empty saremo pronti per generare una nuova freccia
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= NotchEmpty;
    }

    // Update is called once per frame
    void Update()
    {
        if (_bow.isSelected && _arrowNotched == false)
        {
            _arrowNotched=true;
            StartCoroutine("DelayedSpawn");
        }
        if (!_bow.isSelected && _currentArrow != null)
        {
            Destroy(_currentArrow);
            NotchEmpty(1f);
        }
    }

    private void NotchEmpty(float value)
    {
        _arrowNotched = false;
        _currentArrow = null;
    }

    // La seguente funzione serve per introdurre un ritardo nella generazione delle frecce
    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(1f);
        _currentArrow = Instantiate(arrow, notch.transform);
    }
}
