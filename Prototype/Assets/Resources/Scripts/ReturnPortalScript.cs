using UnityEngine;
using System.Collections;

public class ReturnPortalScript : Portal
{

    protected override int GetLevelNumber()
    {
        return 99;
    }

    protected override void SetDialogueText()
    {
        DialogueText = "Back To The Ship?";
    }
}
