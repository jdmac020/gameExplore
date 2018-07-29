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
                case "Portal1":
                    DialogueText = PortalOneDialogueText;
                    break;
                case "Portal2":
                    DialogueText = PortalTwoDialogueText;
                    break;
                case "Portal3":
                    DialogueText = PortalThreeDialogueText;
                    break;
                case "Portal4":
                    DialogueText = PortalFourDialogueText;
                    break;
                case "Portal5":
                    DialogueText = PortalFiveDialogueText;
                    break;
                case "Portal6":
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
