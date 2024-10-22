using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class BattleNode
{
    //参与的部队列表
    private Dictionary<TownOwnerType, List<Solider>> Dic_BattleTroop;
    //当前剩余的部队列表
    private Dictionary<TownOwnerType, int> Dic_BattleLeftInfo;   

    private int OwnerTownId;
    private TownOwnerType ownerTownType;
    
    Tuple<bool, TownOwnerType> battleResult = new Tuple<bool, TownOwnerType>(false,TownOwnerType.None);
    public void Init(TownOwnerType ownerType)
    {
        ownerTownType = ownerType;
        Dic_BattleTroop = new Dictionary<TownOwnerType, List<Solider>>();
        Dic_BattleLeftInfo = new Dictionary<TownOwnerType, int>();
        ResetBattleResult();
    }

    private void ResetBattleResult()
    {
        battleResult = new Tuple<bool, TownOwnerType>(false,TownOwnerType.None);
    }
    
    //加入一场战斗
    public void JoinBattle(List<Solider> enemy)
    {
        if (enemy.Count > 0 )
        {
            var townOwnerType = enemy[0].OwnerType;
            if (!Dic_BattleTroop.ContainsKey(townOwnerType))
            {
                Dic_BattleTroop[townOwnerType] = new List<Solider>();
            }
            Dic_BattleTroop[townOwnerType].AddRange(enemy);
        }
    }

    //是否还在战斗中
    public bool IsInBattle()
    {
        var enemyTownType = TownOwnerType.None;
        foreach (var enemyItem in Dic_BattleTroop)
        {
            if (enemyItem.Value.Count > 0)
            {
                if (enemyItem.Key != ownerTownType)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //检查战斗结果
    public Tuple<bool, TownOwnerType> CheckBattleResult()
    {
        Dic_BattleLeftInfo.Clear();
        var leftTroopNum = 0;
        foreach (var enemyItem in Dic_BattleTroop)
        {
            if (enemyItem.Value.Count > 0)
            {
                Dic_BattleLeftInfo[enemyItem.Key] = enemyItem.Value.Count;
                leftTroopNum += 1;
            }
        }
        if (leftTroopNum == 1)  //???????????????????
        {
            foreach (var leftInfoItem in Dic_BattleLeftInfo)
            {
                if (ownerTownType != leftInfoItem.Key)
                {
                    ownerTownType = leftInfoItem.Key;
                    break;
                }
            }
            battleResult = new Tuple<bool, TownOwnerType>(true, ownerTownType);
            //GameEntry.Event.Fire(this,BattleSingleTownResultEventArgs.Create(OwnerTownId,winType));
        }
        else
        {
            ResetBattleResult();
        }
        return battleResult;
    }
}