using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public GameObject HelpEffectObject;
    public Collider col;

    private void EffectActiveControl(bool Active)
    {
        HelpEffectObject.gameObject.SetActive(Active);
    }

    public void InteractControl(bool Active)
    {
        EffectActiveControl(Active);
        enabled = Active;
    }

    public virtual void Interact()
    {
        EffectActiveControl(false);
    }
}
