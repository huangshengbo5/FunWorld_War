using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��¼һ���ǳص�ս����Ϣ
//ÿ�����ж������Լ���ս������
public class BattleNode
{
    private Dictionary<TownOwnerType, List<Solider>> EnemyDictionary;
    private Dictionary<TownOwnerType, int> BattleLeftInfo = new Dictionary<TownOwnerType, int>();
    public void Init()
    {
        EnemyDictionary = new Dictionary<TownOwnerType, List<Solider>>();
    }

    //һ�ӵо�����ս��
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
        BattleLeftInfo.Clear();
        var leftTroopNum = 0;
        foreach (var enemyItem in EnemyDictionary)
        {
            if (enemyItem.Value.Count == 0)
            {
                if (enemyItem.Key == TownOwnerType.Neutral)
                {
                    
                }
            }
            else
            {
                BattleLeftInfo[enemyItem.Key] = enemyItem.Value.Count;
                leftTroopNum += 1;
            }
        }

        if (leftTroopNum == 1)  //ֻ��һ��ʤ��ʱ����Ҫ����
        {
            GameEntry.Event.Fire(EventDefine.EventMax);
        }
        
        return result;
    }
    
}
