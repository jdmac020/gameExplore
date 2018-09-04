using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class PlayerScriptInLevel : PlayerScript
{
    public bool HitEnemy;
    public LayerMask GroundLayer;
    public bool FacingRight = true;

    public int CurrentHitPoints;
    public int MaxHitPoints;

    private SpriteRenderer _sprite;

    private GameObject _cureRayLeft;
    private GameObject _beamLeft;
    private GameObject _cureRayRight;
    private GameObject _beamRight;
    private bool _rayFiring = false;
    private int _fireCounter;

    private GameObject _returnPortal;
    private TextManager _livesText;
    private TextManager _hitPointsText;
    private HitPointScript _hitPointHearts;

    private bool _isGrounded = true;

    // Use this for initialization
    protected override void Start()
    {

        _rigidBody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();

        _returnPortal = GameObject.FindGameObjectWithTag("Finish");
        _returnPortal.SetActive(false);

        _cureRayLeft = GameObject.Find("ToolSpotL");
        _beamLeft = GameObject.Find("CureBeamL");
        _beamLeft.SetActive(false);
        _cureRayLeft.SetActive(false);

        _cureRayRight = GameObject.Find("ToolSpotR");
        _beamRight = GameObject.Find("CureBeamR");
        _beamRight.SetActive(false);


        //_cureRayActive = _cureRayRight;
        //_beamActive = _beamRight;
        //_beamRight.SetActive(false);
        //_beamActive.SetActive(false);

        InitializeConfirmBox();

        UpdateLives();
        UpdateHitPoints();
    }

    protected void UpdateHitPoints()
    {
        if (_hitPointsText == null)
        {
            //_hitPointsText = GameObject.FindGameObjectWithTag("HUD_HitPointsText").GetComponent<TextManager>();
            _hitPointHearts = GameObject.Find("HitPointHearts").GetComponent<HitPointScript>();
        }

        //_hitPointsText.ChangeText($"Hit Points: {CurrentHitPoints}/{MaxHitPoints}");

        if (CurrentHitPoints <= 0)
        {
            SceneManagerScript.RestartLevel();
        }

        _hitPointHearts.UpdateHitHearts(CurrentHitPoints);
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

        ReturnEnableCheck();

        if (_rayFiring && _fireCounter < 2)
        {
            _fireCounter++;
        }
        else if (_rayFiring && _fireCounter >= 2)
        {
            SwitchBeam(false);
            _rayFiring = false;
            _fireCounter = 0;
        }

        CheckGround();

        if (!isPaused)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            //Debug.Log($"Last Horizontal: {moveHorizontal}");
            var debugMessage = string.Empty;

            if (moveHorizontal > 0 && !FacingRight)
            {
                FacingRight = true;
                _cureRayRight.SetActive(true);
                _cureRayLeft.SetActive(false);
                
            }
            else if (moveHorizontal < 0 && FacingRight)
            {
                FacingRight = false;
                _cureRayLeft.SetActive(true);
                _cureRayRight.SetActive(false);
            }

            //if (FacingRight)
            //{
            //    debugMessage = "I am facing Right!";
            //}
            //else
            //{
            //    debugMessage = "I am Facing Left!";
            //}

            //Debug.Log(debugMessage);

            var moveVertical = 0;// Input.GetAxis("Vertical");

            //var movementSpeed = Mathf.Lerp(_rigidBody.velocity.x, Input.GetAxis("Horizontal") * Speed * Time.deltaTime, Time.deltaTime * 10); //new Vector2(moveHorizontal, moveVertical);
            var movement = new Vector2(moveHorizontal, moveVertical);

            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _rigidBody.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
                _isGrounded = false;
            }

            if (Input.GetKeyDown(KeyCode.H) && _fireCounter == 0)
            {
                Debug.Log("Shoot the 'Laser'!");

                SwitchBeam(true);

                _rayFiring = true;

                RaycastHit2D cureStrike = new RaycastHit2D();

                var thisVector = new Vector2(_rigidBody.transform.position.x, _rigidBody.transform.position.y - .13f);

                if (FacingRight)
                {

                    cureStrike = Physics2D.Raycast(thisVector, Vector2.right, 1);
                }
                else
                {
                    cureStrike = Physics2D.Raycast(thisVector, Vector2.left, 1);
                }

                //int layerMask = ~(1 << 8);

                if (cureStrike.collider == null)
                {
                    Debug.Log($"We hit nothing, Lebowski!");
                }
                else
                {
                    Debug.Log($"We hit {cureStrike.collider.gameObject.name}");
                    var script = cureStrike.collider.gameObject.GetComponent<DoomCatController>();
                    Debug.Log(script);
                    script.UpdateCurePoints();
                }


            }

            //_rigidBody.AddForce(new Vector2(movementSpeed, _rigidBody.velocity.x));

            //_rigidBody.velocity = new Vector2(movementSpeed, _rigidBody.velocity.x);

            _rigidBody.AddForce(movement * Speed);
            //_rigidBody.MovePosition(movement);
        }
    }

    private void SwitchBeam(bool beamOn)
    {
        if (_cureRayRight.activeSelf)
        {
            _beamRight.SetActive(beamOn);
        }
        else
        {
            _beamLeft.SetActive(beamOn);
        }
    }

    private void ReturnEnableCheck()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log($"Found {enemies.Length} Enemies!");

        var activeEnemies = 0;

        foreach (var enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                activeEnemies++;
            }
        }

        Debug.Log($"Found {activeEnemies} active enemies!");

        if (activeEnemies == 0)
        {
            _returnPortal.SetActive(true);
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

            if (HitEnemy)
            {
                HitEnemy = false;
            }
            else
            {
                CurrentHitPoints--;
                UpdateHitPoints();
                Debug.Log($"Hit Points Now {CurrentHitPoints}");
                HitEnemy = true;
            }



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
        Gizmos.DrawRay(new Vector2(_rigidBody.transform.position.x, _rigidBody.transform.position.y - .13f), Vector2.right);

        //Gizmos.color = new Color(0, 1, 0, .5f);
        //Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - .29f),
        //    new Vector2(.01f, .01f));
    }

    private void ActivateConfirmBox(string displayText)
    {
        _confirmBox.ActivateConfirmBox(displayText);
    }

}
