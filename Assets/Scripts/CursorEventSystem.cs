using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorEventSystem : MonoBehaviour
{
    public CursorManager cursorManager;
    private GameObject last = null;

    private void FixedUpdate()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        if (selected != null && last!=selected)
        {
            if (cursorManager.changePosition(selected)) last = selected;
        }
    }
}
