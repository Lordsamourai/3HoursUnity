using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Nécessaire pour charger des scènes

public class Teleporter : MonoBehaviour
{
    public Etat etat;  // État du téléporteur
    public GameObject[] cubes;  // Liste des cubes à changer de couleur
    public int currentLevel = 1;  // Le niveau actuel du joueur
    // Start is called before the first frame update
    void Start()
    {
        // Change la couleur des cubes en fonction de l'état
        ChangeCubeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Fonction qui change la couleur des cubes
    void ChangeCubeColor()
    {
        // Parcours chaque cube et change sa couleur en fonction de l'état
        foreach (GameObject cube in cubes)
        {
            Renderer cubeRenderer = cube.GetComponent<Renderer>();

            if (cubeRenderer != null)
            {
                if (etat == Etat.Active)
                {
                    cubeRenderer.material.color = Color.blue;  // Cube bleu si actif
                }
                else if (etat == Etat.Desactive)
                {
                    cubeRenderer.material.color = Color.red;  // Cube rouge si désactivé
                }
            }
        }

        if (etat == Etat.Desactive)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Charger la scène suivante automatiquement
            currentLevel++;  // Augmente le numéro de niveau
            SceneManager.LoadScene("Niveau" + currentLevel);  // Charger la scène suivante selon le numéro de niveau
        }
    }
}

public enum Etat
{
    Active,
    Desactive,
}