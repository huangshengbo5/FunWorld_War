using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

//��¼һ���ǳص�ս����Ϣ
//ÿ�����ж������Լ���ս������
public class BattleNode
{
    private Dictionary<TownOwnerType, List<Solider>> Dic_BattleTroop;   //��ս���ӱ�
    private Dictionary<TownOwnerType, int> Dic_BattleLeftInfo;          //ʣ���ս���ӱ�

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
    
    //һֻ���Ӳμ�ս��
    public void JoinBattle(List<Solider> enemy)
    {
        var townOwnerType = enemy[0].OwnerType;
        if (!Dic_BattleTroop.ContainsKey(townOwnerType))
        {
            Dic_BattleTroop[townOwnerType] = new List<Solider>();
        }
        Dic_BattleTroop[townOwnerType].AddRange(enemy);
    }

    //�Ƿ���ս����
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

    //���ս�����
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
        if (leftTroopNum == 1)  //ֻ��һ��ʤ��ʱ����Ҫ����
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