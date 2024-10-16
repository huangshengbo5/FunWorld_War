using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

//????????????????
//?????????????????????????
public class BattleNode
{
    private Dictionary<TownOwnerType, List<Solider>> Dic_BattleTroop;   //????????
    private Dictionary<TownOwnerType, int> Dic_BattleLeftInfo;          //??????????

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
    
    //????????????
    public void JoinBattle(List<Solider> enemy)
    {
        var townOwnerType = enemy[0].OwnerType;
        if (!Dic_BattleTroop.ContainsKey(townOwnerType))
        {
            Dic_BattleTroop[townOwnerType] = new List<Solider>();
        }
        Dic_BattleTroop[townOwnerType].AddRange(enemy);
    }

    //??????????
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

    //?????????
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