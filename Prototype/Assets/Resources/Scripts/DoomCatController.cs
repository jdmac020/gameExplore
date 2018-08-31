using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomCatController : MonoBehaviour
{
    public float Speed;
    public float DetectRange;
    public float BackUpPoint;

    protected GameObject _playerTarget;
    protected Rigidbody2D _rigidBody;

    protected float _playerX;
    protected float _playerY;
    protected Vector2 _playerVector;
    protected float _thisX;
    protected float _thisY;
    protected Vector2 _thisVector;
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

        //Debug.Log($"Player X: {_playerX}");
        //Debug.Log($"This X: {_thisX}");
        //Debug.Log($"Player is to the left: {_isPlayerToLeft}");
        //Debug.Log($"Enemy is moving left: {_isMovingLeft}");
        Debug.Log($"Distance to Player Is: {_distanceToPlayer}");

        if (_distanceToPlayer <= BackUpPoint)
        {
            BackAway();
        }
        else if (_distanceToPlayer <= DetectRange)
        {
            ChaseIsAfoot();
        }
        

    }

    protected void BackAway()
    {
        Debug.Log("I'm in BACKAWAY");

        Vector2 direction = GetVectorToPlayer(_playerX, _playerY);
        var followVector = Vector2.MoveTowards(_thisVector, direction, -1 * Speed * Time.deltaTime);

        if (_isPlayerToLeft)
        {
            _isMovingLeft = false;

            followVector.x = followVector.x * -1;
        }

        _rigidBody.AddForce(followVector, ForceMode2D.Impulse);
    }

    protected void ChaseIsAfoot()
    {
        
        Vector2 direction = GetVectorToPlayer(_playerX, _playerY);
        var followVector = Vector2.MoveTowards(_thisVector, direction, Speed * Time.deltaTime);

        if (!_isPlayerToLeft)
        {
            _isMovingLeft = false;

            followVector.x = followVector.x * -1;
        }

        _rigidBody.AddForce(followVector);

    }

    protected void UpdatePositions()
    {
        _playerX = _playerTarget.transform.position.x;
        _playerY = _playerTarget.transform.position.y;
        _playerVector = new Vector2(_playerX, _playerY);

        _thisX = transform.position.x;
        _thisY = transform.position.y;
        _thisVector = new Vector2(_thisX, _thisY);

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
