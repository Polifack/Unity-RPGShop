using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool changePosition(GameObject target)
    {
        RectTransform rt = target.GetComponent<RectTransform>();
        float width = rt.rect.width;
        Vector3 demo = new Vector3(-width/2, 0, 0);
        Matrix4x4 mLW = target.transform.localToWorldMatrix;
        Vector3 newPos = target.transform.position;

        newPos.x = mLW.MultiplyPoint(demo).x;
        this.transform.position = newPos;

        transform.SetParent(target.transform);
        return (newPos.x-transform.position.x<1);
    }
}
