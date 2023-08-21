using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    // [SerializeField] private Transform gun;

    bool rotationLocked;

    private float xRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rotationLocked = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        if (rotationLocked == false)
        {    
            transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
            // if (gun != null) gun.localRotation = Quaternion.Euler(xRot, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

    }

// When entering the inventory screen, this method is accessed by "PlayerInventory" to turn "rotationLocked" to true, and then to false when the screen is exited
    public void LockRotation(bool turnOn)
    {
        if (turnOn)
        {
            rotationLocked = true;
        }

        if (!turnOn)
        {
            rotationLocked = false;
        }

        
    }
}
