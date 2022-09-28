using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Board : Interactable, INextEvent
{
    public Transform pencilTransform;
    public Pencil pencil;
    public Interactable nextInteractable;
    public Paint paint;
    public ParticleSystem CompletedEffect;

    public CinemachineVirtualCamera vcamBefore;
    public CinemachineVirtualCamera vcamSelf;
    public CinemachineVirtualCamera vcamNext;

    public override void Interact()
    {
        UIManager.Instance.TutorialTextControl(false, "");
        base.Interact();
        vcamBefore.Priority = 10;
        vcamSelf.Priority = 11;
        pencil.gameObject.transform.DOMove(pencilTransform.position, 0.5f);
        pencil.gameObject.transform.DORotate(pencilTransform.localEulerAngles, 0.5f);
        StartCoroutine(CamWaitTime());
    }

    IEnumerator CamWaitTime()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GameState = GameManager.GameStates.Wait;
        paint.enabled = true;
        UIManager.Instance.TutorialTextControl(true, "click board  <br> Paint the board black");
    }

    public void NextEvent()
    {
        StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime()
    {
        CompletedEffect.Play();
        pencil.gameObject.transform.DOMove(pencilTransform.position, 0.1f);
        pencil.gameObject.transform.DORotate(pencilTransform.localEulerAngles, 0.1f);
        paint.enabled = false;
        yield return new WaitForSeconds(1f);
        vcamSelf.Priority = 10;
        vcamNext.Priority = 11;
        GameManager.Instance.GameState = GameManager.GameStates.Play;
        nextInteractable.InteractControl(true);
        InteractControl(false);
        UIManager.Instance.TutorialTextControl(true, "click the cup");
    }
}
