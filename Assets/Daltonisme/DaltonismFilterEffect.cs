using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DaltonismFilterEffect : MonoBehaviour
{
    public Material daltonismMaterial;
    private bool isDaltonismActive = false;

    void Update()
    {
        // V�rifie si la touche "D" est press�e
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Inverse l'�tat du filtre (si actif, on le d�sactive et vice-versa)
            isDaltonismActive = !isDaltonismActive;
            Debug.Log("Filtre daltonien " + (isDaltonismActive ? "activ�" : "d�sactiv�"));
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (daltonismMaterial != null)
        {
            // Applique le filtre seulement si le filtre daltonien est activ�
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
            Debug.Log("Le mat�riel de daltonisme n'est pas assign� !");
            Graphics.Blit(src, dest);  // Rendu normal sans filtre
        }
    }
}
