using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;


public class TrashbinTrigger : MainTrigger
{
    public ParticleSystem CompletedEffect;

    public override void Selected()
    {
        base.Selected();
        UIManager.Instance.TutorialTextControl(true, "drag to Trashbin");
    }

    public override void TriggerEnter()
    {
        GameManager.Instance.GameState = GameManager.GameStates.Wait;
        helpObject.SetActive(false);
        cup.gameObject.transform.DOMove(transform.position + Vector3.down * 0.5f, 1f).OnComplete(() =>
        {
            StartCoroutine(WaitTiming());
        });
    }

    IEnumerator WaitTiming()
    {
        CompletedEffect.Play();
        yield return new WaitForSeconds(0.5f);
        cup.NextEvent();
    }
}
