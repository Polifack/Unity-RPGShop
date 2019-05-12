using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IConfirmationWindowCallbacks : MonoBehaviour
{
    public GameObject sourceGameObject;
    public virtual void setSource(GameObject newGameObject)
    {
        this.sourceGameObject = newGameObject;
    }
    public abstract void OnConfirm();
    public abstract void OnCancel();
}
