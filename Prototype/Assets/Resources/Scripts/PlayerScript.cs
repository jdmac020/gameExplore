using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class PlayerScript : MonoBehaviour
{
    public float Speed;
    public float JumpPower;

    protected GameObject _confirmPanel;
    protected GameObject _lockedPanel;
    protected ConfirmBox _lockedBox;
    protected ConfirmBox _confirmBox;
    protected Rigidbody2D _rigidBody;

    // Use this for initialization
    protected virtual void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        InitializeConfirmBox();

        InitializedLockedConfirmBox();

        SceneManagerScript.TurnOnNewPortals();
    }

    protected void InitializeConfirmBox()
    {
        var _confirmPanels = GameObject.FindGameObjectsWithTag("ConfirmPanel");
        _confirmPanel = _confirmPanels.Where(cp => cp.name == "ConfirmPanel").FirstOrDefault();
        _confirmBox = _confirmPanel.GetComponent<ConfirmBox>();
        _confirmBox.InitializeThePanel();
        _confirmBox.DeactivateConfirmBox();
    }

    protected void InitializedLockedConfirmBox()
    {
        var _lockedPanels = GameObject.FindGameObjectsWithTag("LockedPanel");
        _lockedPanel = _lockedPanels.Where(lp => lp.name == "LockedPanel").FirstOrDefault();
        _lockedBox = _lockedPanel.GetComponent<ConfirmBox>();
        _lockedBox.InitializeThePanel();
        _lockedBox.DeactivateConfirmBox();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        var isPaused = SceneManagerScript.CheckPause();

        if (!isPaused)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            var movement = new Vector2(moveHorizontal, moveVertical);

            _rigidBody.AddForce(movement * Speed);
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var gameObj = collision.gameObject;

        if (gameObj.layer == 9)
        {
            var portal = gameObj.GetComponent<Portal>();
            var displayText = portal.DialogueText;
            var isLocked = portal.IsLocked;
            var world = portal.WorldNumber;
            var level = portal.LevelNumber;

            ActivateLevelConfirmBox(displayText, isLocked, world.ToString(), level.ToString());
        }
    }

    private void ActivateLevelConfirmBox(string displayText, bool isLocked, string worldNumber, string levelNumber)
    {
        if (isLocked)
        {
            _lockedBox.ActivateConfirmBox(displayText);
        }
        else
        {
            _confirmBox.ActivateConfirmBox(displayText, worldNumber, levelNumber);
        }
    }

}
