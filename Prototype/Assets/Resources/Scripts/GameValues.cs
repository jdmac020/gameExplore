using Assets.Resources.Scripts.LevelManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameValues : MonoBehaviour {

    public static GameValues Instance { get; private set; }

    public bool Paused;

    public List<LevelStatus> LevelRecords = new List<LevelStatus>();

    public int CurrentWorldNumber;
    public int RemainingLivesCurrentLevel;

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
