using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PullInteraction : XRBaseInteractable
{
    public static event Action<float> PullActionReleased; // Segnalerà che abbiamo rilasciato l'oggetto

    public Transform start, end; // Sono i punti di inizio e fine del filo
    public GameObject notch; // Dove genereremo le frecce
    public float pullAmount { get; private set; } = 0.0f; // Quanto riusciamo a fare pullback sulle nostre stringhe

    private LineRenderer _lineRenderer; // Riferimento al render della linea per poterlo aggiornare
    // Polish: private IXRSelectInteractor _pullingInteractor = null;
    private IXRInteractor _pullingInteractor = null; // Riferimento all'interactor, ci dice quando stiamo tenendo la stringa e con che mano

    //Polish
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
        //Polish
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        _pullingInteractor = args.interactorObject;
    }

    // La seguente funzione serve per capire quando stiamo rilasciando la corda (String)
    public void Release()
    {
        PullActionReleased?.Invoke(pullAmount);
        _pullingInteractor = null;
        pullAmount = 0f;
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, 0f);
        UpdateString();

        //Polish
        PlayReleaseSound();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPosition = _pullingInteractor.transform.position;
                pullAmount = CalculatePull(pullPosition);

                UpdateString();

                //Polish
                HapticFeedback();
            }
        }
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        // Calcoliamo quanto sono vicine le posizioni di inizio e fine
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }

    private void UpdateString()
    {
        // Aggiorniamo la posizione della corda controllando di non superare i limiti tra start e end
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(start.transform.localPosition.z, end.transform.localPosition.z, pullAmount);
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, linePosition.z + .2f);
        _lineRenderer.SetPosition(1, linePosition);
    }

    //Polish
    private void HapticFeedback()
    {
        if (_pullingInteractor != null)
        {
            ActionBasedController currentController = _pullingInteractor.transform.gameObject.GetComponent<ActionBasedController>();
            currentController.SendHapticImpulse(pullAmount, .1f);
        }
    }

    //Polish
    private void PlayReleaseSound()
    {
        _audioSource.Play();
    }
}
