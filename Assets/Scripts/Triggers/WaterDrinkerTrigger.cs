using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterDrinkerTrigger : MainTrigger
{
    public WaterBottle waterBottle;
    public Transform pos;
    public ParticleSystem WaterEffect;
    public ParticleSystem HelpEffect;
    public MainTrigger nextTrigger;

    public override void Selected()
    {
        base.Selected();
        UIManager.Instance.TutorialTextControl(true, "drag to Water Drinker");
    }

    public override void TriggerEnter()
    {
        ClickControl.Instance.selectIDrag = null;
        ClickControl.Instance.selectInteractable = null;
        GameManager.Instance.GameState = GameManager.GameStates.Wait;
        cup.col.enabled = false;
        HelpEffect.gameObject.SetActive(false);
        cup.gameObject.transform.DOMove(pos.position, 0.25f).OnComplete(() =>
        {
            waterBottle.InteractControl(true);
            waterBottle.col.enabled = true;
            cup.targetTrigger = nextTrigger;
            UIManager.Instance.TutorialTextControl(true, "click the Water bottle");
            GameManager.Instance.GameState = GameManager.GameStates.Play;

        });
        //base.TriggerEnter(iDrag);
    }

    public void WaterAnimStart()
    {
        StartCoroutine(WaterAnimTiming(cup));
    }

    IEnumerator WaterAnimTiming(Cup cup)
    {
        WaterEffect.Play();
        cup.material.DOTiling(new Vector2(4.53f, 1.4f), GameManager.Instance.WaterFillingSpeed);
        yield return new WaitForSeconds(GameManager.Instance.WaterFillingSpeed);
        WaterEffect.Stop();
        GameManager.Instance.GameState = GameManager.GameStates.Play;
        NextEvent();
    }

    public override void NextEvent()
    {
        nextTrigger.enabled = true;
        cup.InteractControl(true);
        cup.col.enabled = true;
        enabled = false;
    }
}
