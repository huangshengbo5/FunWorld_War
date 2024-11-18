using System;
using Script.Game.Base;
using UnityEngine;

public class Battle_Camera : MonoBehaviour
{
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        GameEntry.Touch.OnSingleTap += HandlerSingleTap;
    }

    void HandlerSingleTap(Vector2 position)
    {
        Ray ray = mainCamera.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            var baseObj = hit.collider.gameObject;
            var baseTown = baseObj.GetComponent<BaseObject>();
            if (baseTown)
            {
                baseTown.OnClick();
            }
            Debug.Log($"hit object:{hit.collider.name}");
        }
    }
}