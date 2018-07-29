using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour
{
    public float Speed;

    private GameObject _confirmPanel;
    private ConfirmBox _confirmBox;
    private Rigidbody2D _rigidBody;
    private int _portalTouched = 0;

    private bool IsPaused = false;

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _confirmPanel = GameObject.FindGameObjectWithTag("ConfirmPanel");
        _confirmBox = _confirmPanel.GetComponent<ConfirmBox>();
        _confirmBox.InitializeThePanel();
        _confirmBox.DeactivateConfirmBox();
        //HideThemNewPortals();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        while (!IsPaused)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            var movement = new Vector2(moveHorizontal, moveVertical);

            _rigidBody.AddForce(movement * Speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gameObj = collision.gameObject;

        if (gameObj.layer == 9)
        {
            IsPaused = true;

            var displayText = gameObj.GetComponent<Portal>().DialogueText;
            //trigger dialogue
            ActivateConfirmBox(displayText);
            
            _portalTouched = GetTagNumber(gameObj.tag);
            // will happen after successful completion, in the live run
            if (_portalTouched < 4)
                SwitchOnPortal(_portalTouched);

            if (!_confirmPanel.activeSelf)
            {
                IsPaused = false;
            }
        }
    }

    private string GetDisplayText(GameObject gameObj)
    {
        var portalScript = gameObj.GetComponent<Portal>();

        return portalScript.DialogueText;
    }

    private void ActivateConfirmBox(string displayText)
    {
        _confirmBox.ActivateConfirmBox(displayText);
    }

    private void SwitchOnPortal(int tagNumber)
    {
        var portalTag = $"Level{tagNumber + 3}";
        var portalToFlip = GameObject.FindGameObjectWithTag(portalTag);
        var script = portalToFlip.GetComponent<LockedPortal>();
        script.Unlock();
    }

    private int GetTagNumber(string tag)
    {
        var tagLength = tag.Length;
        var lastTwoCharacters = tag.Substring(tagLength - 2);
        var lastCharacter = tag.Substring(tagLength - 1);
        int returnValue = 0;

        if (int.TryParse(lastTwoCharacters, out returnValue))
        {
            return returnValue;
        }
        if (int.TryParse(lastCharacter, out returnValue))
        {
            return returnValue;
        }

        throw new System.ArgumentException($"No Parsable Number Found At The End Of Tag [{tag}]");
    }

}
