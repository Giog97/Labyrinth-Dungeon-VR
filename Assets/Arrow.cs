using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f; // Velocità delle frecce
    public Transform tip; // Punta della freccia

    private Rigidbody _rigidbody;
    private bool _inAir = false; // Ci dice quando la freccia è in aria
    private Vector3 _lastPosition = Vector3.zero; // Ci dice l'ultima posizione della punta della freccia

    //Polish
    private ParticleSystem _particleSystem;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //Polish
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();

        PullInteraction.PullActionReleased += Release;
        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    // La seguente funzione è chiamata quando avviene il pull, e fa disicrivere dagli eventi
    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;
        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);

        Vector3 force = transform.forward * value * speed;
        _rigidbody.AddForce(force, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity()); //Serve per far ruotare la freccia in modo da renderla più reale possibile

        _lastPosition = tip.position;

        //Polish
        _particleSystem.Play();
        _trailRenderer.emitting = true;
    }

    // La seguente funzione ci permette di essere allineati alla realtà fisica della freccia
    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidbody.velocity, transform.up); // Ci permette di avere una rotazione in tempo reale
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (_inAir)
        {
            CheckCollision();
            _lastPosition += tip.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.gameObject.layer != 8) // Layer 8 è dove andrebbe settato il body in modo da non avere interazioni negative
            {
                if (hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidbody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                    body.AddForce(_rigidbody.velocity, ForceMode.Impulse);
                }
                Stop();
            }
        }
    }

    // La seguente funzione stoppa semplicemente le interazioni
    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);

        //Polish
        _particleSystem.Stop();
        _trailRenderer.emitting = false;
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidbody.useGravity = usePhysics;
        _rigidbody.isKinematic = !usePhysics;
    }
}
