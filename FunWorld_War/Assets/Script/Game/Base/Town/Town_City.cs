using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Script.Game.Base;
using UnityEngine;
using Random = System.Random;

public class Town_City : BaseTown
{
    private BattleNode BattleNode;
    public GameObject ObjSolider;
    //目标城镇
    public BaseTown TargetTown;

    private void Start()
    {
        Init();
        RegisterEvent();
        CurSoliderNum = DefaultMaxSoliderNum;
        CurSoliderNum_Txt.SetText(CurSoliderNum.ToString());
        StartCoroutine(DelayGenerateSolider());
    }

    IEnumerator DelayGenerateSolider()
    {
        yield return new WaitForSeconds(1f);
        if (OwnerCamp == global::CampType.Player)
        {
            CreateSolider();
            AttackTargetTown();
        }
        yield return null;
    }

    private void Init()
    {
        BattleNode = new BattleNode();
        BattleNode.Init(this.OwnerCamp);
        // var fsmName = "FSM_" + this.gameObject.name;
        // var fsm = GameEntry.Fsm.CreateFsm<BaseTown>(fsmName, this,
        //     new FSMTownIdle(),
        //     new FSMTownBattle(),
        //     new FSMTownBattleEnd());
        // fsm.Start<FSMTownIdle>();
    }

    private void RegisterEvent()
    {
        GameEntry.Event.Subscribe(BattleSingleTownResultEventArgs.EventId,OnSingleTownResult); 
    }
    
    void OnSingleTownResult(object sender, GameEventArgs e)
    {
        var eventData = e as BattleSingleTownResultEventArgs;
        var type = eventData.OwnerType;
        Debug.Log(string.Format("胜利{0}",type));
    }
    
    protected override void CreateSolider()
    {
        for (int i = 0; i < DefaultMaxSoliderNum; i++)
        {
            var createSolider = CreateSolider(i); 
            Soliders.Add(createSolider);
            createSolider.Init();
            if (TargetTown)
            {
                createSolider.ChangeTargetObject(TargetTown);    
            }
            createSolider.CampType = this.Camp();
        }
        DefaultMaxSoliderNum = 0;
        //BattleNode.JoinBattle(Soliders);
    }

    public void AttackTargetTown()
    {
        if (OwnerCamp == global::CampType.Player && TargetTown != null)
        {
            JoinBattle(TargetTown, GetAllSoliders());
        }
    }
    
    //创建士兵
    protected Solider CreateSolider(int index)
    {
        var solider = (GameObject)Instantiate(ObjSolider);
        solider.name = string.Format("Solider_{0}_{1}",OwnerCamp.ToString(),index) ;
        var soliderTans = solider.GetComponent<Transform>();
        soliderTans.position = GetSoliderPosition();
        soliderTans.localScale = Vector3.one;
        soliderTans.rotation = Quaternion.identity;
        var soliderCom = solider.GetComponent<Solider>();
        soliderCom.OwnerTown = this;
        return soliderCom;
    }

    //获取士兵位置
    Vector3 GetSoliderPosition()
    {
        var selfPosition = this.gameObject.transform.position;
        var random = new Random();
        var posx = random.Next((int)selfPosition.x+3,(int)selfPosition.x+4);
        //var posy = random.Next((int)selfPosition.y,(int)selfPosition.y+10);
        var posz = random.Next((int)selfPosition.z+3,(int)selfPosition.z+4);
        return new Vector3(posx,0,posz);
    }

    //检查战斗结果
    public override Tuple<bool, CampType> CheckBattleResult()
    {
        return BattleNode.CheckBattleResult();
    }

    //是否正在发生战斗
    public override bool IsInBattle()
    {
        return BattleNode.IsInBattle();
    }

    //加入一只敌方部队
    public override void JoinBattle(List<Solider> enemy)
    {
        BattleNode.JoinBattle(enemy);
    }

    public void JoinBattle(BaseTown targetTown, List<Solider> enemy)
    {
        targetTown.JoinBattle(enemy);
    }
}