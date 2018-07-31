using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour {

    public static bool IsPaused;

	// Use this for initialization
	void Start ()
    {
        IsPaused = SceneValues.Instance.Paused;
	}

    public static void CheckPause()
    {
        IsPaused = SceneValues.Instance.Paused;
    }

    public static void UpdatePause(bool pauseStatus)
    {
        SceneValues.Instance.Paused = pauseStatus;
    }

    public static void StartScene1()
    {
        SceneManager.LoadScene(1);
    }
}
