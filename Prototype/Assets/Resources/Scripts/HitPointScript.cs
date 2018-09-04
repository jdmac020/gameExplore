using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointScript : MonoBehaviour
{
    private GameObject _hudElement;
    private Image _heartRenderer;
    private Sprite[] _spriteOptions;

	// Use this for initialization
	void Start ()
    {
        _hudElement = gameObject;
        _heartRenderer = gameObject.GetComponent<Image>();
	}

    public void UpdateHitHearts(int hitPointsRemaining)
    {
        if (_spriteOptions == null)
        {
            LoadSpriteOptions();
        }


        if (hitPointsRemaining == 5)
        {
            Debug.Log($"Remaining HP {hitPointsRemaining}"); 
                Debug.Log($"loading sprite {_spriteOptions[0].name}");
            Debug.Log($"Existing Thing: {_heartRenderer}");
            //_heartRenderer.sprite = _spriteOptions[0];
        }
        else if (hitPointsRemaining == 4)
        {
            Debug.Log($"Remaining HP {hitPointsRemaining} loading sprite {_spriteOptions[1].name}");
            _heartRenderer.sprite = _spriteOptions[1];
        }
        else if (hitPointsRemaining == 3)
        {
            _heartRenderer.sprite = _spriteOptions[3];
        }
        else if (hitPointsRemaining == 2)
        {
            _heartRenderer.sprite = _spriteOptions[4];
        }
        else if (hitPointsRemaining == 1)
        {
            _heartRenderer.sprite = _spriteOptions[2];
        }
        else
        {
            _hudElement.SetActive(false);
        }
    }

    private void LoadSpriteOptions()
    {
        _spriteOptions = Resources.LoadAll<Sprite>("Sprites/HitPoints");

        var counter = 0;

        foreach (var sprite in _spriteOptions)
        {
            Debug.Log($"Index: {counter} | Filename {sprite.name}");
            counter++;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
