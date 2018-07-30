using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    public static bool IsPaused;

	// Use this for initialization
	void Start ()
    {
        IsPaused = EmptyScript.Instance.Paused;
	}

    public static void CheckPause()
    {
        IsPaused = EmptyScript.Instance.Paused;
    }

    public static void UpdatePause(bool pauseStatus)
    {
        EmptyScript.Instance.Paused = pauseStatus;
    }
}
