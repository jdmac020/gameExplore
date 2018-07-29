using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBox : MonoBehaviour
{

    private GameObject _panel;
    private TextManager _textBox;

	// Use this for initialization
	void Start ()
    {
        //_panel = gameObject;
        //_textBox = _panel.GetComponent<Text>();

    }

    public void InitializeThePanel()
    {
        _panel = gameObject;
        _textBox = FindObjectOfType<TextManager>();
    }

    public void ActivateConfirmBox(string boxText)
    {
        SceneMap.IsPaused = true;
        _textBox.ChangeText(boxText);
        _panel.SetActive(true);
    }

    public void DeactivateConfirmBox()
    {
        _panel.SetActive(false);
        SceneMap.IsPaused = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
