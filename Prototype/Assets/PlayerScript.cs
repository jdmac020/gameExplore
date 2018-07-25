﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D _rigidBody;
    private int _portalTouched = 0;
    private GameObject[] _newPortals;

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _newPortals = GameObject.FindGameObjectsWithTag("NewPortal");
        HideThemNewPortals();
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

        if (gameObj.CompareTag("Portal"))
        {
            gameObj.SetActive(false);
            _portalTouched++;
        }

        if (gameObj.CompareTag("NewPortal"))
        {
            gameObj.SetActive(false);
            _portalTouched++;
        }

        if (_portalTouched == 3)
        {
            SpawnThemNewPortals();
        }
    }

    private void HideThemNewPortals()
    {
        foreach (var portal in _newPortals)
        {
            portal.SetActive(false);
        }
    }

    private void SpawnThemNewPortals()
    {
        foreach (var portal in _newPortals)
        {
            portal.SetActive(true);
        }
    }

}
