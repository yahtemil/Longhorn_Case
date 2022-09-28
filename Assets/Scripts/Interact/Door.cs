using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : Interactable
{
    public GameObject DoorObject;
    public override void Interact()
    {
        base.Interact();
        DoorObject.transform.DOLocalRotate(new Vector3(0f, -95f, 0f), 1f);
        UIManager.Instance.TutorialText.gameObject.SetActive(false);
        GameManager.Instance.LevelCompleted();
        HelpEffectObject.SetActive(false);
        col.enabled = false;
    }
}
