using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomCatController : MonoBehaviour
{
    public float Speed;
    public float DetectRange;

    protected GameObject _playerTarget;
    protected Rigidbody2D _rigidBody;

    protected float _playerX;
    protected float _playerY;
    protected float _thisX;
    protected float _thisY;
    protected float _distanceToPlayer;

    protected bool _isPlayerToLeft;
    protected bool _isMovingLeft;

	// Use this for initialization
	void Start ()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerTarget = GameObject.FindGameObjectWithTag("Player");
        _isMovingLeft = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePositions();

        //Debug.Log($"Player is to the left: {_isPlayerToLeft}");
        //Debug.Log($"Enemy is moving left: {_isMovingLeft}");
        //Debug.Log($"Distance to Player Is: {_distanceToPlayer}");

        if (_distanceToPlayer <= DetectRange)
        {
            ChaseIsAfoot();
        }

        
    }

    protected void ChaseIsAfoot()
    {
        Vector2 direction = new Vector2();

        if (_isPlayerToLeft)
        {
            direction = GetVectorToPlayer(_playerX, _playerY);
        }
        else
        {
            _isMovingLeft = false;

            direction = GetVectorToPlayer(_playerX * -1, _playerY);
        }

        _rigidBody.AddForce(direction * Speed);

    }

    protected void UpdatePositions()
    {
        _playerX = _playerTarget.transform.position.x;
        _playerY = _playerTarget.transform.position.y;

        _thisX = transform.position.x;
        _thisY = transform.position.y;

        _distanceToPlayer = Vector2.Distance(new Vector2(_playerX,_playerY), new Vector2(_thisX, _thisY));

        if (_playerX < _thisX)
        {
            _isPlayerToLeft = true;
        }
        else
        {
            _isPlayerToLeft = false;
        }
    }

    protected Vector2 GetVectorToPlayer(float playerX, float playerY)
    {
        return new Vector2(playerX, playerY);
    }
}
