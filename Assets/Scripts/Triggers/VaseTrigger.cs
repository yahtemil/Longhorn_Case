using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VaseTrigger : MainTrigger, INextEvent
{
    public ParticleSystem CompletedEffect;
    public TrashbinTrigger trashbinTrigger;
    public ParticleSystem WaterEffect;
    public Material mat;

    private void Start()
    {
        mat.DOTiling(new Vector2(1f, 0.1f), 0.1f);
    }
    public override void Selected()
    {
        base.Selected();
        UIManager.Instance.TutorialTextControl(true, "Drag to Vase");
    }

    public override void TriggerEnter()
    {
        ClickControl.Instance.selectIDrag = null;
        ClickControl.Instance.selectInteractable = null;
        GameManager.Instance.GameState = GameManager.GameStates.Wait;
        cup.gameObject.transform.DOMove(transform.position + Vector3.up * 0.5f + Vector3.forward * 0.2f, 0.25f).OnComplete(() =>
        {
            cup.VasoWaterPS.Play();
            cup.WaterPS.Play();
            cup.gameObject.transform.DOLocalRotate(new Vector3(-90f, 0f, 0f), 1f);
            mat.DOTiling(new Vector2(1f,1f), 1f);
            cup.material.DOTiling(new Vector2(4.53f,2f),1f).OnComplete(() => 
            {
                cup.VasoWaterPS.Stop();
                CompletedEffect.Play();
                NextEvent();
                GameManager.Instance.GameState = GameManager.GameStates.Play;
                helpObject.SetActive(false);
            });
        });
    }

    public override void NextEvent()
    {
        cup.Break();
        cup.targetTrigger = trashbinTrigger;
        cup.gameObject.transform.DOLocalRotate(Vector3.zero, 1f);
        trashbinTrigger.enabled = true;
        enabled = false;
        cup.InteractControl(true);
        enabled = false;
    }
}
