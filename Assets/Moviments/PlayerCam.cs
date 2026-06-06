using UnityEngine;
using UnityEngine.InputSystem;

// creditos: https://www.youtube.com/watch?v=f473C43s8nE&

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform playerBody;

    [HideInInspector] public bool canLook = true;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!canLook) return;
        float mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * sensX;
        float mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        playerBody.rotation = Quaternion.Euler(0, yRotation, 0); 
    }
}