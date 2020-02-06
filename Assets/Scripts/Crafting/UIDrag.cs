using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrag : MonoBehaviour
{
    float OffsetX;
    float OffsetY;

    public void BeginDrag()
    {
        OffsetX = transform.position.x - Input.mousePosition.x;
        OffsetY = transform.position.y - Input.mousePosition.y;
    }

    public void OnDrag()
    {
        transform.position = new Vector3(OffsetX + Input.mousePosition.x, OffsetY + Input.mousePosition.y);
    }
}
