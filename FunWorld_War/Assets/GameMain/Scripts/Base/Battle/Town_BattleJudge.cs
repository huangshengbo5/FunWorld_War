using System;
using System.Collections.Generic;
using UnityEngine;

public class Town_BattleJudge
{
    //参与夺城的部队
    private Dictionary<CampType, List<SoliderCommander>> LeftSoliderCommanders;
    //当前争夺的城池
    private Town targetTown;

    Tuple<bool, CampType> battleResult = new Tuple<bool, CampType>(false,CampType.None);
    
    public void Init(Town town)
    {
        targetTown = town;
        LeftSoliderCommanders = new Dictionary<CampType, List<SoliderCommander>>();
    }
    
    //加入一场战斗
    public void JoinBattle(SoliderCommander soliderCommander)
    {
        var camp = soliderCommander.Camp;
        if (!LeftSoliderCommanders.ContainsKey(camp))
        {
            LeftSoliderCommanders[camp] = new List<SoliderCommander>();
        }
        LeftSoliderCommanders[camp].Add(soliderCommander);
    }
    
    //士兵部队离开一场战斗，士兵部队的人全部被消灭
    public void LeaveBattle(SoliderCommander soliderCommander)
    {
        var camp = soliderCommander.Camp;
        if (LeftSoliderCommanders.ContainsKey(camp))
        {
            if (LeftSoliderCommanders[camp].Contains(soliderCommander))
            {
                LeftSoliderCommanders[camp].Remove(soliderCommander);
            }
        }
        if (LeftSoliderCommanders[camp].Count == 0)
        {
            LeftSoliderCommanders.Remove(camp);
        }
    }
    
    public void DoUpdate()
    {
        
    }
    
    //是否还在战斗中
    public bool IsInBattle()
    {
        return LeftSoliderCommanders.Count > 0;
    }

    //检查战斗结果
    public Tuple<bool, CampType> CheckBattleResult()
    {
        CampType leftCamp = CampType.None;
        if (LeftSoliderCommanders.Count  == 1)
        {
            foreach (var commander in LeftSoliderCommanders)
            {
                if (leftCamp == CampType.None)
                {
                    leftCamp = commander.Key;     
                }
            }
        }
        battleResult = new Tuple<bool, CampType>(leftCamp != CampType.None, leftCamp);
        return battleResult;
    }

    //占城成功
    public void Town_OccupiedSuccess()
    {
        Tuple<bool, CampType> battleResult = CheckBattleResult();
        if (battleResult.Item1 == false)
        {
            Debug.LogError("城池已被攻占，战斗却尚未结束！！！");
            return;
        }
        var winner_camp = battleResult.Item2;
        targetTown.ChangeCamp(winner_camp);
        Debug.Log($"城池已被占领，胜利方阵营为：{winner_camp}");
        LeftSoliderCommanders[winner_camp][0].OnBattleWin();
    }

    //守城成功
    public void Town_ProtectSuccess()
    {
        
    }
    
}