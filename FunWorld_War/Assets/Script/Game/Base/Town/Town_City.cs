using System;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics;
using Script.Game;
using Script.Game.Base;
using UnityEngine;
using Random = System.Random;

public class Town_City : BaseTown
{
    public GameObject ObjSolider;


    private void Start()
    {
        CurSoliderNum = DefaultSoliderNum;
        CurSoliderNum_Txt.SetText(CurSoliderNum.ToString());
    }

    public void InitTown()
    {
        
    }
    protected BaseSolider CreateSolider()
    {
        var solider = (GameObject)Instantiate(ObjSolider);
        var soliderTans = solider.GetComponent<Transform>();
        soliderTans.position = GetSoliderPosition();
        soliderTans.localScale = Vector3.one;
        soliderTans.rotation = Quaternion.identity;
        var soliderCom = solider.GetComponent<SoliderPlayer>();
        return soliderCom;
    }

    Vector3 GetSoliderPosition()
    {
        var selfPosition = this.gameObject.transform.position;
        var random = new Random();
        var posx = random.Next((int)selfPosition.x,(int)selfPosition.x+5);
        var posy = random.Next((int)selfPosition.y,(int)selfPosition.y+5);
        var posz = random.Next((int)selfPosition.z,(int)selfPosition.z+5);
        return new Vector3(posx,posy,posz);
    }

    private void Update()
    {
        switch (OwnerType)
        {
            case TownOwnerType.Player:
                Update_Controller_Player();
                break;
        }
      
        // if (Input.GetMouseButtonDown(0))
        // {
        //     var solider = CreateSolider();
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray,out hit))
        //     {
        //         var terrian = hit.collider.gameObject.GetComponent<Terrain>();
        //         if (terrian != null)
        //         {
        //             var postion = hit.point;
        //             // Transform trans = new RectTransform();
        //             // trans.transform.position = postion;
        //             // solider.SetTarget(trans.transform);
        //             solider.MoveToTarget(postion);
        //         }
        //     }
        // }
    }

    void Update_Controller_Player()
    {
        if (LastCreateTimeStamp >= CreateSoliderInterval)
        {
            CurSoliderNum += 1;
            CurSoliderNum_Txt.SetText(CurSoliderNum.ToString());
            LastCreateTimeStamp = 0;
        }
        else
        {
            LastCreateTimeStamp += Time.deltaTime;
        }
    }
}
