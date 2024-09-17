using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectXY : MonoBehaviour
{
    public float speed = 1f;           // Velocità del movimento
    public float rangeX = 0f;          // Range di movimento sull'asse X (2 metri)
    public float rangeY = 0f;          // Range di movimento sull'asse Y (2 metri)

    private Vector3 startPosition;     // Posizione iniziale dell'oggetto
    private bool movingRight = true;   // Direzione sull'asse X
    private bool movingUp = true;      // Direzione sull'asse Y

    void Start()
    {
        // Salva la posizione iniziale dell'oggetto
        startPosition = transform.position;
    }

    void Update()
    {
        // Movimento lungo l'asse X
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x > startPosition.x + rangeX)
            {
                movingRight = false; // Cambia direzione
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x < startPosition.x - rangeX)
            {
                movingRight = true; // Cambia direzione
            }
        }

        // Movimento lungo l'asse Y
        if (movingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (transform.position.y > startPosition.y + rangeY)
            {
                movingUp = false; // Cambia direzione
            }
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y < startPosition.y - rangeY)
            {
                movingUp = true; // Cambia direzione
            }
        }
    }
}
