using System;
using System.Collections.Generic;

//��¼һ���ǳص�ս����Ϣ
//ÿ�����ж������Լ���ս������
public class BattleNode
{
    private Dictionary<TownOwnerType, List<Solider>> EnemyDictionary;
    private Dictionary<TownOwnerType, int> BattleLeftInfo;

    private int OwnerTownId;
    private TownOwnerType ownerTownType;
    
    Tuple<bool, TownOwnerType> battleResult = new Tuple<bool, TownOwnerType>(false,TownOwnerType.None);
    public void Init(TownOwnerType ownerType)
    {
        ownerTownType = ownerType;
        EnemyDictionary = new Dictionary<TownOwnerType, List<Solider>>();
        BattleLeftInfo = new Dictionary<TownOwnerType, int>();
    }

    private void ResetBattleResult()
    {
        battleResult = new Tuple<bool, TownOwnerType>(false,TownOwnerType.None);
    }
    
    //һֻ�о��μ�ս��
    public void JoinBattle(List<Solider> enemy)
    {
        var townOwnerType = enemy[0].OwnerType;
        if (!EnemyDictionary.ContainsKey(townOwnerType))
        {
            EnemyDictionary[townOwnerType] = new List<Solider>();
        }
        EnemyDictionary[townOwnerType].AddRange(enemy);
    }

    //���ս�����
    public Tuple<bool, TownOwnerType> CheckBattleResult()
    {
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
        if (leftTroopNum == 1)  //ֻ��һ��ʤ��ʱ����Ҫ����
        {
            TownOwnerType winType = TownOwnerType.None;
            foreach (var leftInfoItem in BattleLeftInfo)
            {
                if (winType == TownOwnerType.None)
                {
                    winType = leftInfoItem.Key;
                }
            }
            //GameEntry.Event.Fire(this,BattleSingleTownResultEventArgs.Create(OwnerTownId,winType));
        }
        return battleResult;
    }
}