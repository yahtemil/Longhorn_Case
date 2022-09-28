using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class Pencil : Interactable, INextEvent
{
    public Interactable nextInteractable;

    public override void Interact()
    {
        base.Interact();
        col.enabled = false;
        transform.DOLocalMoveY(0.4f, 0.5f).OnComplete(() => NextEvent());
        HelpEffectObject.SetActive(false);
        UIManager.Instance.TutorialTextControl(true, "Click the board");
    }

    public void NextEvent()
    {
        nextInteractable.InteractControl(true);
        nextInteractable.col.enabled = true;
        InteractControl(false);

    }
}
