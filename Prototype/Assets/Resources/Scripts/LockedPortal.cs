using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LockedPortal : Portal
{
    
    private Animator _animator;

	// Use this for initialization
	protected override void Start ()
    {
        IsLocked = true;
        base.Start();
        _animator = _portal.GetComponent<Animator>();
        
	}
    
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Called from the player script (maybe) after completing an earlier portal's challenge to unlock the portal
    /// </summary>
    public void Unlock()
    {
        if (IsLocked)
        {
            IsLocked = false;

            _animator.runtimeAnimatorController = GetUnlockedPortalAnimation();

            SetDialogueText();
        }
    }

    private RuntimeAnimatorController GetUnlockedPortalAnimation()
    {
        return (RuntimeAnimatorController)Instantiate(Resources.Load("Animation/Portal", typeof(RuntimeAnimatorController)));
    }
}
