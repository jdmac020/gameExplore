using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class PlayerScriptInLevel : MonoBehaviour
{
    public float Speed;
    public bool HitEnemy;
    public LayerMask GroundLayer;
    public bool _isGrounded = true;

    private GameObject _returnPortal;

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
        _returnPortal = GameObject.FindGameObjectWithTag("Finish");
        _returnPortal.SetActive(false);

        var _confirmPanels = GameObject.FindGameObjectsWithTag("ConfirmPanel");
        _confirmPanel = _confirmPanels.Where(cp => cp.name == "ConfirmPanel").FirstOrDefault();
        _confirmBox = _confirmPanel.GetComponent<ConfirmBox>();
        _confirmBox.InitializeThePanel();
        _confirmBox.DeactivateConfirmBox();

        //var _lockedPanels = GameObject.FindGameObjectsWithTag("LockedPanel");
        //_lockedPanel = _lockedPanels.Where(lp => lp.name == "LockedPanel").FirstOrDefault();
        //_lockedBox = _lockedPanel.GetComponent<ConfirmBox>();
        //_lockedBox.InitializeThePanel();
        //_lockedBox.DeactivateConfirmBox();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var isPaused = SceneManagerScript.CheckPause();

        CheckGround();

        if (!isPaused)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = 0;// Input.GetAxis("Vertical");

            var movementSpeed = Mathf.Lerp(_rigidBody.velocity.x, Input.GetAxis("Horizontal") * Speed * Time.deltaTime, Time.deltaTime * 10); //new Vector2(moveHorizontal, moveVertical);
            var movement = new Vector2(moveHorizontal, moveVertical);

            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _rigidBody.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
                _isGrounded = false;
            }

            //_rigidBody.AddForce(new Vector2(movementSpeed, _rigidBody.velocity.x));

            //_rigidBody.velocity = new Vector2(movementSpeed, _rigidBody.velocity.x);

            _rigidBody.AddForce(movement * Speed);
        }

        //SceneManagerScript.CheckPause();
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapArea(
            //new Vector2(transform.position.x - .5f, transform.position.y - .5f),
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(transform.position.x, transform.position.y -.29f),
            GroundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gameObj = collision.gameObject;

        if (gameObj.layer == 9)
        {
            var displayText = gameObj.GetComponent<Portal>().DialogueText;
            var isLocked = gameObj.GetComponent<Portal>().IsLocked;
            //trigger dialogue
            SceneManagerScript.UpdatePause(true);
            ActivateConfirmBox(displayText, isLocked);
        }

        if (gameObj.tag == "Enemy")
        {
            HitEnemy = true;
            gameObj.SetActive(false);
            HitEnemy = false;

            _returnPortal.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y -.29f),
            new Vector2(.01f, .01f));
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
            _lockedBox.ActivateConfirmBox(displayText);
        }
        else
        {
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

        throw new ArgumentException($"No Parsable Number Found At The End Of Tag [{tag}]");
    }

}
