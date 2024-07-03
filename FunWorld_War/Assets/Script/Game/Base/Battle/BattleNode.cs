using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//记录一个城池的战斗信息
//每个城市都会有自己的战斗数据
public class BattleNode
{
    private Dictionary<TownOwnerType, List<Solider>> EnemyDictionary;
    public void Init()
    {
        EnemyDictionary = new Dictionary<TownOwnerType, List<Solider>>();
    }

    //一队敌军参与战斗
    public void Join(List<Solider> enemy)
    {
        var townOwnerType = enemy[0].OwnerType;
        if (!EnemyDictionary.ContainsKey(townOwnerType))
        {
            EnemyDictionary[townOwnerType] = new List<Solider>();
        }
        EnemyDictionary[townOwnerType].AddRange(enemy);
    }

    public Tuple<bool, TownOwnerType> CheckBattleResult()
    {
        var result = new Tuple<bool, TownOwnerType>(false,TownOwnerType.None);
        var isEnd = false;
        foreach (var enemyItem in EnemyDictionary)
        {
            if (enemyItem.Value.Count == 0)
            {
                
            }
        }
        return result;
    }
    
}
