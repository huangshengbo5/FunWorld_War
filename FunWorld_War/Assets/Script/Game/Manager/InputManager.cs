using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Script.Game;
using Script.Game.Base;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Camera camera;

    private List<BaseTown> towns = new List<BaseTown>();
    
    
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit))
            {
                var hitObj = hit.collider.gameObject;
                if (hitObj && hitObj.GetComponent<BaseTown>() != null)
                {
                    var baseTown = hitObj.GetComponent<BaseTown>();
                    var soliders = baseTown.GetAllSoliders();
                    if (baseTown.TownType() != TownOwnerType.Player)
                    {
                        for (int i = 0; i < soliders.Count; i++)
                        {

                            //soliders[i].MoveToTarget();
                        }
                    }
                }
            }
        }
    }
}
