using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConfirmationWindow : MonoBehaviour
{
    /*
     * cancelElement: Elemento al que se cambia el focus al pulsar cancel
     * callbacks: Elemento que contiene los callbacks al ejecutar por cada uno de los botones
     */
    private GameObject cancelElement;
    private IConfirmationWindowCallbacks callbacks;


    //Las funciones asociadas a los botones, llaman al callback y cambian el focus, así como hacen desaparecer la ventana. 
    public void onConfirmationDenied()
    {
        callbacks.OnCancel();
        EventSystem.current.SetSelectedGameObject(cancelElement);
        this.gameObject.SetActive(false);
    }

    public void onConfirmationAccepted()
    {
        callbacks.OnConfirm();
        this.gameObject.SetActive(false);
    }

    //Setters del cancel element y de los callbacks.
    public void setCancelElement(GameObject element)
    {
        this.cancelElement = element;
    }
    public void setCallbacks(IConfirmationWindowCallbacks callbacks)
    {
        this.callbacks = callbacks;
    }


}
