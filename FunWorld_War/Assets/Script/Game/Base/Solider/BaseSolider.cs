using System.Collections;
using System.Collections.Generic;
using Script.Game.Base;
using UnityEngine;

public class BaseSolider : MonoBehaviour,SoliderInterface
{
    public enum State
    {
        Idleing,      //休闲
        Moving,       //移动
        Attack_City,   //攻击城市
        Attack_Enemy,  //攻击敌人
        Dead,          //死亡
    }
    
    //士兵目标类型
    public enum TargetType 
    {
        City,   //城市
        Enemy,  //敌人
    }

    public State curState;   //当前的单位状态
    
    public int ViewRedius;   //视野半径
    
    protected Transform target;
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public Transform GetTarget()
    {
        return this.target;
    }

    public virtual void MoveToTarget(Vector3 targetPoint)
    {
        
    }

    public void SufferInjure(float injure)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        
    }
}
