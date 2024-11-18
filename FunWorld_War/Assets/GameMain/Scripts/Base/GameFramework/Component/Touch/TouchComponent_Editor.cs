using System;
using UnityEngine;

public class TouchComponent_Editor : TouchComponent
{
    
    private float doubleTapTimeThreshold = 0.3f;
    private float longPressTimeThreshold = 0.5f;
    private float minPinchDistance = 10f;

    private float lastTapTime;
    private Vector2 lastTapPosition;
    private float touchStartTime;
    private bool isLongPressing;
    private Vector2 touchStartPos;



    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            HanlderMouseButtonUp();
        }
    }
    
    private void HanlderMouseButtonUp()
    {
        TriggerSingleTap(Input.mousePosition);
    }
}