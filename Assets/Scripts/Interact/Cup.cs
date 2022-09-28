using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class Cup : Interactable, IDrag, INextEvent
{
    public Interactable nextInteractable;
    public Material material;
    public ParticleSystem WaterPS;
    public ParticleSystem VasoWaterPS;
    public MainTrigger targetTrigger;

    public CinemachineVirtualCamera vcamSelf;
    public CinemachineVirtualCamera vcamNext;
    Vector3 firstPos;

    private void Start()
    {
        material.DOTiling(new Vector2(4.53f,2f), 0.1f);
    }

    public void Break()
    {
        gameObject.transform.DOLocalMove(Vector3.zero, 1f).OnComplete(() =>
        {
            InteractControl(true);
            UIManager.Instance.TutorialTextControl(true, "click the cup");
        });
    }

    public void Down()
    {
        firstPos = gameObject.transform.position;
    }

    public void Drag(Vector2 pos)
    {
        Vector3 selfPos = firstPos + new Vector3(0f, pos.y, pos.x);
        selfPos.y = selfPos.y < 0.7f ? 0.7f : selfPos.y;
        selfPos.y = selfPos.y > 1.7f ? 1.7f : selfPos.y;
        selfPos.z = selfPos.z < -3.5f ? -3.5f : selfPos.z;
        selfPos.z = selfPos.z > -2.75f ? -2.75f : selfPos.z;
        gameObject.transform.DOMove(new Vector3(3.75f, selfPos.y, selfPos.z), 0.1f);
    }

    public override void Interact()
    {
        base.Interact();
        targetTrigger.Selected();
    }

    public void NextEvent()
    {
        UIManager.Instance.TutorialTextControl(true, "click the door");
        vcamSelf.Priority = 10;
        vcamNext.Priority = 11;
        nextInteractable.InteractControl(true);
        GameManager.Instance.GameState = GameManager.GameStates.Play;
        InteractControl(false);
    }

   
}
