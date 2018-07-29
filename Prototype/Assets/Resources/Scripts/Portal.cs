using UnityEngine;
using System.Collections;
using static Assets.Resources.Scripts.NavigationConstants;

public class Portal : MonoBehaviour
{
    public string DialogueText;
    public bool IsLocked;

    protected GameObject _portal;

    // Use this for initialization
    protected virtual void Start()
    {
        _portal = gameObject;
        SetDialogueText();
    }

    /// <summary>
    /// Sets the dialogue text based on the portal's tag
    /// </summary>
    protected void SetDialogueText()
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
    void Update()
    {

    }
}
