using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
    //Ohter components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            //Rotate the transform according to the client aim vector
            transform.forward = networkInputData.aimForwardVector;

            //Cancel out rotation on X axis as we don't want our character to tilt
            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;

            //Move
            Vector3 moveDirection = transform.forward * networkInputData.direction.z + transform.right * networkInputData.direction.x;
            moveDirection.Normalize();

            networkCharacterControllerPrototypeCustom.Move(moveDirection);

            //Jump
            if (networkInputData.isJumpPressed)
            {
                networkCharacterControllerPrototypeCustom.Jump();
            }

            //Check if we've fallen off the world.
            CheckFallRespawn();
        }
    }

    void CheckFallRespawn()
    {
        if (transform.position.y < -12)
            transform.position = Utils.GetRandomSpawnPoint();
    }
}
