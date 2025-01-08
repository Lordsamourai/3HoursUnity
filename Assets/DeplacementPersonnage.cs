using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPersonnage : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    public float jumpForce = 5f;
    public float rotationSpeed = 2f; // Vitesse de rotation
    public Transform cameraTransform;

    private float pitch = 0f; // Rotation verticale (haut/bas) de la caméra
    private Rigidbody rb; // Référence au Rigidbody
    private bool isGrounded = true; // Vérifie si le joueur est au sol

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur
        Cursor.visible = false; // Cache le curseur
    }

    // Update is called once per frame
    void Update()
    {
        // Déplacement avec ZQSD
        float moveX = Input.GetAxis("Horizontal"); // Q/D
        float moveZ = Input.GetAxis("Vertical");   // Z/S

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 velocity = move * moveSpeed * Time.deltaTime;
        transform.position += velocity;

        // Rotation avec la souris
        float yaw = Input.GetAxis("Mouse X") * rotationSpeed; // Rotation horizontale
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;    // Rotation verticale
        pitch = Mathf.Clamp(pitch, -90f, 90f);                // Limite l'angle vertical

        transform.Rotate(0f, yaw, 0f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Saut (Espace)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si le joueur touche le sol, il peut sauter à nouveau
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Plateforme"))
        {
            isGrounded = true;
        }
    }
}
