using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DaltonismFilterEffect : MonoBehaviour
{
    public Material daltonismMaterial;
    private bool isDaltonismActive = false;

    void Update()
    {
        // Vérifie si la touche "D" est pressée
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Inverse l'état du filtre (si actif, on le désactive et vice-versa)
            isDaltonismActive = !isDaltonismActive;
            Debug.Log("Filtre daltonien " + (isDaltonismActive ? "activé" : "désactivé"));
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (daltonismMaterial != null)
        {
            // Applique le filtre seulement si le filtre daltonien est activé
            if (isDaltonismActive)
            {
                Graphics.Blit(src, dest, daltonismMaterial);
            }
            else
            {
                Graphics.Blit(src, dest);  // Rendu normal sans filtre
            }
        }
        else
        {
            Debug.Log("Le matériel de daltonisme n'est pas assigné !");
            Graphics.Blit(src, dest);  // Rendu normal sans filtre
        }
    }
}
