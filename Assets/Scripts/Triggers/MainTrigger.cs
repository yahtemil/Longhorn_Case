using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTrigger : MonoBehaviour, INextEvent
{
    public Cup cup;
    public GameObject helpObject;
    public Collider col;

    public virtual void Selected()
    {
        col.enabled = true;
        helpObject.gameObject.SetActive(true);
        enabled = true;
    }
    public virtual void TriggerEnter()
    {
        ClickControl.Instance.selectIDrag = null;
        ClickControl.Instance.selectInteractable = null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cup"))
        {
            Cup cup = other.GetComponent<Cup>();
            if (cup != null)
            {
                TriggerEnter();
                col.enabled = false;
            }
        }
    }

    public virtual void NextEvent()
    {

    }
}
