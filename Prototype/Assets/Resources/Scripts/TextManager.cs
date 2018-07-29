﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public Text _textBox;

	// Use this for initialization
	void Start () {
        _textBox = GetComponent<Text>(); 
        _textBox.text = string.Empty;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeText(string textToUse)
    {
        _textBox.text = textToUse;
    }
}