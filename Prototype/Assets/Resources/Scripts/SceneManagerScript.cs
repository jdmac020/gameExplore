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

    public static bool CheckPause()
    {
        return SceneValues.Instance.Paused;
    }

    public static bool LevelOneIsComplete()
    {
        return SceneValues.Instance.Level1Complete;
    }

    public static void UpdatePause(bool pauseStatus)
    {
        SceneValues.Instance.Paused = pauseStatus;
    }

    public static void StartScene1()
    {
        SceneManager.LoadScene(2);
    }

    public static void ReturnToMainFromFirst(bool levelComplete)
    {
        if (levelComplete)
        {
            SceneValues.Instance.Level1Complete = true;
        }

        SceneManager.LoadScene(0);
    }
}
