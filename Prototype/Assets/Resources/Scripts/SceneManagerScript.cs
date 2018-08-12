using Assets.Resources.Scripts.LevelManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour {

    public static bool IsPaused;

    public static int WorldNumber;
    public static string MainSceneName;

	// Use this for initialization
	void Start ()
    {
        IsPaused = GameValues.Instance.Paused;

        var worldValues = GameObject.FindGameObjectWithTag("WorldValues").GetComponent<WorldValues>();

        WorldNumber = worldValues.WorldNumber;
        GameValues.Instance.CurrentWorldNumber = WorldNumber;
        MainSceneName = worldValues.MainSceneName;
	}

    public static bool CheckPause()
    {
        return GameValues.Instance.Paused;
    }

    public static void UpdatePause(bool pauseStatus)
    {
        GameValues.Instance.Paused = pauseStatus;
    }

    public static void StartLevel(string worldNumber, string levelNumber)
    {

        GameValues.Instance.RemainingLivesCurrentLevel = 3;
        
        AddOrUpdateLevelRecord(int.Parse(worldNumber), int.Parse(levelNumber));

        var sceneName = GetSceneName(worldNumber, levelNumber);

        SceneManager.LoadScene(sceneName);
    }

    private static void AddOrUpdateLevelRecord(int worldNumber, int levelNumber)
    {

        var existingRecord = GameValues.Instance.LevelRecords.Where(record => record.WorldNumber == worldNumber && record.LevelNumber == levelNumber).FirstOrDefault();

        if (existingRecord == null)
        {
            var levelRecord = new LevelStatus { WorldNumber = worldNumber, LevelNumber = levelNumber, Attempts = 1 };

            GameValues.Instance.LevelRecords.Add(levelRecord);
        }
        else
        {
            existingRecord.Attempts++;
        }
    }

    private static void CompleteLevelRecord()
    {
        var sceneName = SceneManager.GetActiveScene().name;

        var levelRecord = GameValues.Instance.LevelRecords.Where(record => record.SceneName == sceneName).FirstOrDefault();

        if (levelRecord != null)
        {
            levelRecord.IsComplete = true;
        }
    }

    private static string GetSceneName(string worldNumber, string levelNumber)
    {
        return $"{worldNumber}-{levelNumber}";
    }

    public static void ReturnToMain(bool levelComplete)
    {
        if (levelComplete)
        {
            CompleteLevelRecord();
        }

        SceneManager.LoadScene(MainSceneName);
    }

    public static void RestartLevel()
    {
        GameValues.Instance.RemainingLivesCurrentLevel--;

        var currentScene = SceneManager.GetActiveScene();

        if (GameValues.Instance.RemainingLivesCurrentLevel > 0)
        {
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        else
        {
            ReturnToMain(false);
        }
    }

    public static void TurnOnNewPortals()
    {
        var levelsToUnlock = GameValues.Instance.LevelRecords.Where(record => record.IsComplete).Select(record => new { LevelNumber = record.LevelNumber + 3 } );

        Debug.Log($"Completed Level Count: {levelsToUnlock.Count()}");

        foreach (var level in levelsToUnlock)
        {
            var portalTag = $"Level{level.LevelNumber}";
            Debug.Log($"PortalTag Is {portalTag}");
            var portalToFlip = GameObject.FindGameObjectWithTag(portalTag);
            portalToFlip.GetComponent<LockedPortal>().Unlock();
        }
    }
}
