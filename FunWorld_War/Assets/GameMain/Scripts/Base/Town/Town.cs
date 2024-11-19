using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Script.Game.Base;
using UnityEngine;
using Random = System.Random;

public class Town : BaseTown
{
    private Town_BattleJudge townBattleJudge;

    public Town_BattleJudge TownBattleJudge
    {
        get => townBattleJudge;
        set => townBattleJudge = value;
    }

    private List<SoliderCommander> SoliderCommanders;
    public GameObject ObjSolider;


    private void Start()
    {
        Init();
        RegisterEvent();
        CurSoliderNum = DefaultMaxSoliderNum;
        // CurSoliderNum_Txt.SetText(CurSoliderNum.ToString());
        
        //test code
        StartCoroutine(AttackTargetTown());
    }
    
    
    IEnumerator AttackTargetTown()
    {
        yield return new WaitForSeconds(1f);
        if (OwnerCamp == global::CampType.Player && TargetTown != null)
        {
            var soliderCommander = CreateSolider();
            JoinBattle(TargetTown, soliderCommander);
        }
        yield return null;
    }

    private void Init()
    {
        townBattleJudge = new Town_BattleJudge();
        townBattleJudge.Init(this);
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
    
    protected override SoliderCommander CreateSolider()
    {
        List<Solider> Soliders = new List<Solider>();
        for (int i = 0; i < CurSoliderNum; i++)
        {
            var createSolider = CreateSolider(i); 
            Soliders.Add(createSolider);
            createSolider.CampType = this.Camp();
            createSolider.Init();
            if (TargetTown)
            {
                createSolider.ChangeTargetObject(TargetTown);    
            }
        }

        SoliderCommander soliderCommander = new SoliderCommander();
        soliderCommander.Init(this,TargetTown as Town);
        soliderCommander.AddSoliders(Soliders);
        CurSoliderNum = 0;
        return soliderCommander;
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
        var posx = random.Next((int)selfPosition.x+3,(int)selfPosition.x+5);
        //var posy = random.Next((int)selfPosition.y,(int)selfPosition.y+10);
        var posz = random.Next((int)selfPosition.z+3,(int)selfPosition.z+5);
        return new Vector3(posx,0,posz);
    }

    //检查战斗结果
    public override Tuple<bool, CampType> CheckBattleResult()
    {
        return townBattleJudge.CheckBattleResult();
    }

    //是否正在发生战斗
    public override bool IsInBattle()
    {
        return townBattleJudge.IsInBattle();
    }

    //加入一只敌方部队
    public override void JoinBattle(SoliderCommander enemySoliderCommander)
    {
        townBattleJudge.JoinBattle(enemySoliderCommander);
    }

    public void JoinBattle(BaseTown targetTown, SoliderCommander enemySoliderCommander)
    {
        targetTown.JoinBattle(enemySoliderCommander);
    }
    
    //被攻击
    public override void BeAttack(BaseObject attacker, int damageNum)
    {
        if (IsOccupied == false)
        {
            SufferInjure(damageNum);
            if (Hp <= damageNum)
            {
                IsOccupied = true;
                //通知裁判，城池被占领
                townBattleJudge.Town_OccupiedSuccess();
            }            
        }
    }
    
    public void SufferInjure(int injure)
    {
        Hp -= injure;
    }
    
}