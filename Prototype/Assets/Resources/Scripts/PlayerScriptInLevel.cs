using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class PlayerScriptInLevel : PlayerScript
{
    //public bool HitEnemy;
    public LayerMask GroundLayer;
    public bool _isGrounded = true;

    public int CurrentHitPoints;
    public int MaxHitPoints;

    private GameObject _returnPortal;
    private TextManager _livesText;
    private TextManager _hitPointsText;
    

    // Use this for initialization
    protected override void Start()
    {

        _rigidBody = GetComponent<Rigidbody2D>();

        _returnPortal = GameObject.FindGameObjectWithTag("Finish");
        _returnPortal.SetActive(false);

        InitializeConfirmBox();

        UpdateLives();
        UpdateHitPoints();
    }

    protected void UpdateHitPoints()
    {
        if (_hitPointsText == null)
        {
            _hitPointsText = GameObject.FindGameObjectWithTag("HUD_HitPointsText").GetComponent<TextManager>();
        }

        _hitPointsText.ChangeText($"Hit Points: {CurrentHitPoints}/{MaxHitPoints}");

        if (CurrentHitPoints <= 0)
        {
            SceneManagerScript.RestartLevel();
        }
    }

    protected void UpdateLives()
    {
        if (_livesText == null)
        {
            _livesText = GameObject.FindGameObjectWithTag("HUD_LivesText").GetComponent<TextManager>();
        }

        var livesRemaining = SceneManagerScript.RemainingLives;

        _livesText.ChangeText(livesRemaining.ToString());

        
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        var isPaused = SceneManagerScript.CheckPause();
        
        CheckGround();

        if (!isPaused)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = 0;// Input.GetAxis("Vertical");

            //var movementSpeed = Mathf.Lerp(_rigidBody.velocity.x, Input.GetAxis("Horizontal") * Speed * Time.deltaTime, Time.deltaTime * 10); //new Vector2(moveHorizontal, moveVertical);
            var movement = new Vector2(moveHorizontal, moveVertical);

            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _rigidBody.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
                _isGrounded = false;
            }

            //_rigidBody.AddForce(new Vector2(movementSpeed, _rigidBody.velocity.x));

            //_rigidBody.velocity = new Vector2(movementSpeed, _rigidBody.velocity.x);

            _rigidBody.AddForce(movement * Speed);
            //_rigidBody.MovePosition(movement);
        }
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapArea(
            //new Vector2(transform.position.x - .5f, transform.position.y - .5f),
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(transform.position.x, transform.position.y - .29f),
            GroundLayer);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        var gameObj = collision.gameObject;
        var colliderType = collision.GetType();

        if (gameObj.layer == 9)
        {
            var displayText = gameObj.GetComponent<Portal>().DialogueText;
            var isLocked = gameObj.GetComponent<Portal>().IsLocked;

            //trigger dialogue
            ActivateConfirmBox(displayText);
        }

        if (gameObj.tag == "Enemy" && colliderType == typeof(CircleCollider2D))
        {
            CurrentHitPoints--;
            UpdateHitPoints();
            Debug.Log($"Hit Points Now {CurrentHitPoints}");
            //HitEnemy = true;
            //gameObj.SetActive(false);
            //HitEnemy = false;

            //_returnPortal.SetActive(true);
        }

        if (gameObj.tag == "DeathTrigger")
        {
            SceneManagerScript.RestartLevel();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - .29f),
            new Vector2(.01f, .01f));
    }

    private void ActivateConfirmBox(string displayText)
    {
        _confirmBox.ActivateConfirmBox(displayText);
    }

}
