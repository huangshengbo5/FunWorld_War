using System;
using System.Collections.Generic;

//记录一个城池的战斗信息
//每个城市都会有自己的战斗数据
public class BattleNode
{
    private Dictionary<TownOwnerType, List<Solider>> EnemyDictionary;
    private Dictionary<TownOwnerType, int> BattleLeftInfo;

    private int OwnerTownId;
    public void Init()
    {
        EnemyDictionary = new Dictionary<TownOwnerType, List<Solider>>();
        BattleLeftInfo = new Dictionary<TownOwnerType, int>();
    }

    //一只敌军参加战斗
    public void JoinBattle(List<Solider> enemy)
    {
        var townOwnerType = enemy[0].OwnerType;
        if (!EnemyDictionary.ContainsKey(townOwnerType))
        {
            EnemyDictionary[townOwnerType] = new List<Solider>();
        }
        EnemyDictionary[townOwnerType].AddRange(enemy);
    }

    //检查战斗结果
    public Tuple<bool, TownOwnerType> CheckBattleResult()
    {
        var result = new Tuple<bool, TownOwnerType>(false,TownOwnerType.None);
        var isEnd = false;
        BattleLeftInfo.Clear();
        var leftTroopNum = 0;
        foreach (var enemyItem in EnemyDictionary)
        {
            if (enemyItem.Value.Count > 0)
            {
                BattleLeftInfo[enemyItem.Key] = enemyItem.Value.Count;
                leftTroopNum += 1;
            }
        }
        if (leftTroopNum == 1)  //只有一方胜出时才需要处理
        {
            TownOwnerType winType = TownOwnerType.None;
            foreach (var leftInfoItem in BattleLeftInfo)
            {
                if (winType == TownOwnerType.None)
                {
                    winType = leftInfoItem.Key;
                }
            }
            GameEntry.Event.Fire(this,BattleSingleTownResultEventArgs.Create(OwnerTownId,winType));
        }
        return result;
    }
}