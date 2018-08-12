using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour {

    public GameObject Player;       //Public variable to store a reference to the player game object
    public float TrackSpeed = 2;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MoveToPlayer();
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        float interpolation = TrackSpeed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, Player.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, Player.transform.position.x, interpolation);

        this.transform.position = position;
    }
}
