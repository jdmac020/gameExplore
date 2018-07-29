using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D _rigidBody;
    private int _portalTouched = 0;

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        //HideThemNewPortals();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector2(moveHorizontal, moveVertical);

        _rigidBody.AddForce(movement * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gameObj = collision.gameObject;

        if (gameObj.layer == 9)
        {
            //trigger dialogue

            _portalTouched++;
            // will happen after successful completion, in the live run
            SwitchOnPortal(_portalTouched);
        }
    }

    private void SwitchOnPortal(int tagNumber)
    {
        var portalTag = $"Portal{tagNumber}";
        var portalToFlip = GameObject.FindGameObjectWithTag(portalTag);
        var script = portalToFlip.GetComponent<LockedPortal>();
        script.Unlock();
    }

}
