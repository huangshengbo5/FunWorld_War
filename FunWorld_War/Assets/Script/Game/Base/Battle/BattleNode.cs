using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

//记录一个城池的战斗信息
//每个城市都会有自己的战斗数据
public class BattleNode
{
    private Dictionary<TownOwnerType, List<Solider>> Dic_BattleTroop;   //参战部队表
    private Dictionary<TownOwnerType, int> Dic_BattleLeftInfo;          //剩余参战部队表

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
    
    //一只部队参加战斗
    public void JoinBattle(List<Solider> enemy)
    {
        var townOwnerType = enemy[0].OwnerType;
        if (!Dic_BattleTroop.ContainsKey(townOwnerType))
        {
            Dic_BattleTroop[townOwnerType] = new List<Solider>();
        }
        Dic_BattleTroop[townOwnerType].AddRange(enemy);
    }

    //是否在战斗中
    public bool IsInBattle()
    {
        var leftTroopNum = 0;
        foreach (var enemyItem in Dic_BattleTroop)
        {
            if (enemyItem.Value.Count > 0)
            {
                leftTroopNum += 1;
            }
        }
        return leftTroopNum > 1;
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
        if (leftTroopNum == 1)  //只有一方胜出时才需要处理
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