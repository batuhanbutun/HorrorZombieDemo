using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameStats _gameStats;

    private float mouseSensitivity = 500f;
    private float xRotation = 0f;
    public Transform playerBody;
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {

        if (_gameStats.isGameOver == false)
        {
            float mouseX = (Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime);
            float mouseY = (Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime);
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX, Space.Self);
        }
    }

  
   

}
