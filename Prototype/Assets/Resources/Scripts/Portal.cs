using UnityEngine;
using System.Collections;
using static Assets.Resources.Scripts.NavigationConstants;

public class Portal : MonoBehaviour
{
    public string DialogueText;
    public bool IsLocked;
    public int WorldNumber;
    public int LevelNumber;

    protected GameObject _portal;

    // Use this for initialization
    protected virtual void Start()
    {
        _portal = gameObject;
        SetDialogueText();
        LevelNumber = GetLevelNumber();
    }

    protected virtual int GetLevelNumber()
    {
        var tag = _portal.tag;

        var tagLength = tag.Length;
        var lastTwoCharacters = tag.Substring(tagLength - 2);
        var lastCharacter = tag.Substring(tagLength - 1);
        int returnValue = 0;

        if (int.TryParse(lastTwoCharacters, out returnValue))
        {
            return returnValue;
        }
        if (int.TryParse(lastCharacter, out returnValue))
        {
            return returnValue;
        }

        throw new System.ArgumentException($"No Parsable Number Found At The End Of Tag [{tag}]");
    }

    /// <summary>
    /// Sets the dialogue text based on the portal's tag
    /// </summary>
    protected virtual void SetDialogueText()
    {

        if (IsLocked)
        {
            DialogueText = PortalLocked;
        }
        else
        {
            switch (_portal.tag)
            {
                case "Level1":
                    DialogueText = PortalOneDialogueText;
                    break;
                case "Level2":
                    DialogueText = PortalTwoDialogueText;
                    break;
                case "Level3":
                    DialogueText = PortalThreeDialogueText;
                    break;
                case "Level4":
                    DialogueText = PortalFourDialogueText;
                    break;
                case "Level5":
                    DialogueText = PortalFiveDialogueText;
                    break;
                case "Level6":
                    DialogueText = PortalSixDialogueText;
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
}
