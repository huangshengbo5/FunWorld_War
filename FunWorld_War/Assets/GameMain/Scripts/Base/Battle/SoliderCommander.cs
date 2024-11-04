using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderCommander
{
    private List<Solider> Soliders;

    //归属的城镇，可以为空
    private Town ownerTown;

    //归属的阵营，不能为空
    private CampType camp;

    public Town OwnerTown
    {
        get => ownerTown;
        set => ownerTown = value;
    }

    public CampType Camp
    {
        get => camp;
        set => camp = value;
    }

    public void Init(Town town)
    {
        OwnerTown = town;
        Soliders = new List<Solider>();
        Camp = OwnerTown.Camp();
    }
    public void AddSolider(Solider solider)
    {
        Soliders.Add(solider);
    }

    public void AddSoliders(List<Solider> soliders)
    {
        Soliders.AddRange(soliders);
    }

    //战斗胜利
    public void OnBattleWin()
    {
        //执行士兵进城
    }
}
