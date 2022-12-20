using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector3 moveInputVector = Vector3.zero;
    Vector3 viewInputVector = Vector3.zero;
    bool isJumpButtonPressed = false;

    //Other components
    LocalCameraHandler localCameraHandler;

    private void Awake()
    {
        localCameraHandler = GetComponentInChildren<LocalCameraHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //View input
        viewInputVector.x = Input.GetAxis("Mouse X");
        viewInputVector.z = Input.GetAxis("Mouse Y") * -1; //Invert mouse look

        //Move input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.z = Input.GetAxis("Vertical");

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            isJumpButtonPressed = true;
        }

        //Set view
        localCameraHandler.SetViewInputVector(viewInputVector);
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //Aim data
        networkInputData.aimForwardVector = localCameraHandler.transform.forward;

        //Move data
        networkInputData.direction = moveInputVector;

        //Jump data
        networkInputData.isJumpPressed = isJumpButtonPressed;

        //Reset variables now that we have read their states
        isJumpButtonPressed = false;

        return networkInputData;
    }
}
