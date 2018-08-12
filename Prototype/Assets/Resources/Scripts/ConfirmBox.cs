using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBox : MonoBehaviour
{

    private GameObject _panel;
    private TextManager _textBox;

    private string _worldNumber;
    private string _levelNumber;

    public void InitializeThePanel()
    {
        _panel = gameObject;
        var textBoxOptions = FindObjectsOfType<Text>();
        var textBox = textBoxOptions.Where(t => t.tag == _panel.tag).FirstOrDefault();
        _textBox = textBox.GetComponent<TextManager>();
    }

    public void ActivateConfirmBox(string boxText, string worldNumber = "", string levelNumber = "")
    {
        SceneManagerScript.UpdatePause(true);
        _worldNumber = worldNumber;
        _levelNumber = levelNumber;
        _textBox.ChangeText(boxText);
        _panel.SetActive(true);
    }

    public void DeactivateConfirmBox()
    {
        _panel.SetActive(false);
        SceneManagerScript.UpdatePause(false);
    }

    public void EnterLevel()
    {
        SceneManagerScript.UpdatePause(false);
        SceneManagerScript.StartLevel(_worldNumber, _levelNumber);
    }

    public void ReturnToMain()
    {
        SceneManagerScript.ReturnToMain(true);
    }
}
