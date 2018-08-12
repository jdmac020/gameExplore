﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneValues : MonoBehaviour {

    public static SceneValues Instance { get; private set; }

    public bool Paused;
    public bool Level1Complete;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
