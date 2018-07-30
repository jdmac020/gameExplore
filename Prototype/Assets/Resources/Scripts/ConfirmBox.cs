﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBox : MonoBehaviour
{

    private GameObject _panel;
    private TextManager _textBox;

    public void InitializeThePanel()
    {
        _panel = gameObject;
        var textBoxOptions = FindObjectsOfType<Text>();
        var textBox = textBoxOptions.Where(t => t.tag == _panel.tag).FirstOrDefault();
        _textBox = textBox.GetComponent<TextManager>();
    }

    public void ActivateConfirmBox(string boxText)
    {
        Debug.Log("Inside ConfirmBox Class: " + boxText);
        SceneManager.UpdatePause(true);
        _textBox.ChangeText(boxText);
        _panel.SetActive(true);
    }

    public void DeactivateConfirmBox()
    {
        _panel.SetActive(false);
        SceneManager.UpdatePause(false);
    }
}
