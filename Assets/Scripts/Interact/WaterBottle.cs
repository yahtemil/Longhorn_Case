using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBottle : Interactable
{
    public WaterDrinkerTrigger waterDrinkerTrigger;

    public override void Interact()
    {
        base.Interact();
        col.enabled = false;
        UIManager.Instance.TutorialTextControl(true, "click the cup");
        waterDrinkerTrigger.WaterAnimStart();
        InteractControl(false);
    }

}
