using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPlateforme : MonoBehaviour
{

    public float speed = 5f; // Vitesse de déplacement
    private int direction = 1; // Direction actuelle (1 = droite, -1 = gauche)
    public Sens sens;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        switch (sens)
        {
            // Déplace la plateforme en fonction du sens choisi
            case Sens.AxeX:
                transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
                break;

            case Sens.AxeY:
                transform.Translate(Vector3.up * direction * speed * Time.deltaTime);
                break;

            case Sens.AxeZ:
                transform.Translate(Vector3.forward * direction * speed * Time.deltaTime);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision détectée avec : " + collision.gameObject.name);

        // Vérifie si la plateforme touche un objet avec le layer "Ground"
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Plateforme"))
        {
            // Inverse la direction
            direction *= -1;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Si le joueur quitte la plateforme, réinitialise son parent
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}

public enum Sens
{
    AxeX,
    AxeY,
    AxeZ
}