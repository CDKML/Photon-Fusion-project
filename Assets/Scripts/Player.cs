using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour, IPlayerLeft
{
    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
        {
            Debug.Log("Player left...");
            Runner.Despawn(Object);
        }
    }

    public static Player Local { get; set; }

    // Update is called once per frame
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;

            //Disable main camera
            Camera.main.gameObject.SetActive(false);

            Debug.Log("Spawned local player");
        }
        else
        {
            //Disable the camera if we are not the local player
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;

            //Only 1 audio listner is allowed in the scene so disable remote players audio listner
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;

            Debug.Log("Spawned remote player");
        }

        //Make it easier to tell which player is which.
        transform.name = $"Player_{Object.Id}";
    }

}
