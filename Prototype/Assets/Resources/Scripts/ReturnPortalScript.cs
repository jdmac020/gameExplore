using UnityEngine;
using System.Collections;

public class ReturnPortalScript : Portal
{

    protected override void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log($"Portal Found {enemies.Length} Enemies!");

        var activeEnemies = 0;

        foreach (var enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                activeEnemies++;
            }
        }

        Debug.Log($"Portal Found {activeEnemies} active enemies!");

        if (activeEnemies == 0)
        {
            gameObject.SetActive(true);
        }
    }

    protected override int GetLevelNumber()
    {
        return 99;
    }

    protected override void SetDialogueText()
    {
        DialogueText = "Back To The Ship?";
    }
}
