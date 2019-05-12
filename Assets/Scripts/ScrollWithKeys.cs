using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ScrollWithKeys : MonoBehaviour
{

    private RectTransform scrollRectTransform;
    private RectTransform contentPanel;
    private RectTransform selectedRectTransform;
    private GameObject lastSelected;

    public CursorManager cursor;
    [Range(20,40)]
    public float smoothness = 25f;

    void Start()
    {
        scrollRectTransform = GetComponent<RectTransform>();
        contentPanel = GetComponent<ScrollRect>().content;

        if (contentPanel == null) Debug.Log("null contentPannel");
        if (scrollRectTransform == null) Debug.Log("null scrollRectTransform");
    }

    void FixedUpdate()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        if (selected == null || selected.transform.parent != contentPanel.transform || selected == lastSelected)
        {
            return;
        }

        /* La idea es, si el objeto en el event system seleccionado está más abajo que la mitad, scrollear abajo (mover el content arriba)
         * y si el selected está mas arriba que la mitad, scrollear arriba (mover el content abajo)
         */


        selectedRectTransform = selected.GetComponent<RectTransform>();
        // The position of the selected UI element is the absolute anchor position,
        // ie. the local position within the scroll rect + its height if we're
        // scrolling down. If we're scrolling up it's just the absolute anchor position.


        //Posición Y del elemento seleccionado.
        float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;

        //The upper bound of the scroll view is the anchor position of the content we're scrolling.
        //The lower bound is the anchor position + the height of the scroll rect.
        float scrollViewMinY = contentPanel.anchoredPosition.y;
        float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;

        // If the selected position is below the current lower bound of the scroll view we scroll down.
        if ((selectedPositionY)> scrollViewMaxY)
        {
            float newY = selectedPositionY - smoothness;
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
        }

        // If the selected position is above the current upper bound of the scroll view we scroll up.
        else if ((Mathf.Abs(selectedRectTransform.anchoredPosition.y))< scrollViewMinY)
        {
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, (Mathf.Abs(selectedRectTransform.anchoredPosition.y)- smoothness));
        }
        lastSelected = selected;
    }
}
