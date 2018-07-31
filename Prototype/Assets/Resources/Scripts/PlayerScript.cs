using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class PlayerScript : MonoBehaviour
{
    public float Speed;

    private GameObject _confirmPanel;
    private GameObject _lockedPanel;
    private ConfirmBox _lockedBox;
    private ConfirmBox _confirmBox;
    private Rigidbody2D _rigidBody;
    private int _portalTouched = 0;

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        var _confirmPanels = GameObject.FindGameObjectsWithTag("ConfirmPanel");
        _confirmPanel = _confirmPanels.Where(cp => cp.name == "ConfirmPanel").FirstOrDefault();
        _confirmBox = _confirmPanel.GetComponent<ConfirmBox>();
        _confirmBox.InitializeThePanel();
        _confirmBox.DeactivateConfirmBox();

        var _lockedPanels = GameObject.FindGameObjectsWithTag("LockedPanel");
        _lockedPanel = _lockedPanels.Where(lp => lp.name == "LockedPanel").FirstOrDefault();
        _lockedBox = _lockedPanel.GetComponent<ConfirmBox>();
        _lockedBox.InitializeThePanel();
        _lockedBox.DeactivateConfirmBox();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var isPaused = SceneManagerScript.IsPaused;

        if (!isPaused)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            var movement = new Vector2(moveHorizontal, moveVertical);

            _rigidBody.AddForce(movement * Speed);
        }

        SceneManagerScript.CheckPause();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gameObj = collision.gameObject;

        if (gameObj.layer == 9)
        {
            var displayText = gameObj.GetComponent<Portal>().DialogueText;
            var isLocked = gameObj.GetComponent<Portal>().IsLocked;
            //trigger dialogue
            ActivateConfirmBox(displayText, isLocked);

            _portalTouched = GetTagNumber(gameObj.tag);
            // will happen after successful completion, in the live run
            if (_portalTouched < 4)
                SwitchOnPortal(_portalTouched);
        }
    }

    private string GetDisplayText(GameObject gameObj)
    {
        var portalScript = gameObj.GetComponent<Portal>();

        return portalScript.DialogueText;
    }

    private void ActivateConfirmBox(string displayText, bool isLocked)
    {
        if (isLocked)
        {
            Debug.Log("Under Is Locked: " + displayText);
            _lockedBox.ActivateConfirmBox(displayText);
        }
        else
        {
            Debug.Log("Under Not Locked: " + displayText);
            _confirmBox.ActivateConfirmBox(displayText);
        }
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
