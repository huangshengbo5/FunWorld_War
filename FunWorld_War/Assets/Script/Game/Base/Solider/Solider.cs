using System.Collections;
using DG.Tweening;
using Script.Game;
using Script.Game.Base;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

public class Solider : MonoBehaviour,SoliderInterface
{

    public void Init()
    {
        InitFsm();
    }

    public void InitFsm()
    {
        var fsmName = "FSM_" + this.gameObject.name;
        var fsm = GameEntry.Fsm.CreateFsm<Solider>(fsmName,this, 
            new FSMSoliderIdle(),
            new FSMSoliderDead(),
            new FSMSoliderAttack(),
            new FSMSoliderMoveToTarget());
        fsm.Start<FSMSoliderIdle>();
    }
    
    public enum State
    {
        Idleing,      //休闲
        Moving,       //移动
        Attack_Enemy,  //攻击敌人
        Dead,          //死亡
    }

    public TownOwnerType OwnerType; 
    public State curState;   //当前的单位状态
    
    public int ViewRedius;   //视野半径
    public int ViewAttackRedius;  //攻击半径
    
    public Animator Animator;
    
    protected BaseTown targetTown;

    //敌人
    protected Solider targetSolider;

    //士兵归属的城镇
    protected BaseTown ownerTown;
    public Solider TargetSolider
    {
        get
        {
            return this.targetSolider;
        }
        set
        {
            this.targetSolider = value;
        }
    }

    public BaseTown TargetTown
    {
        get
        {
            return this.targetTown;
        }
        set
        {
            this.targetTown = value;
        }
    }

    public BaseTown OwnerTown
    {
        get
        {
            return this.ownerTown;
        }
        set
        {
            this.ownerTown = value;
        }
    }
    
    public NavMeshAgent navMeshAgent;

    //血量
    public int Hp;
    //伤害
    public int Damage;
    
    //造成伤害时间间隔信息
    private float AttackTimeStamp;
    private float AttackInterval = 1;
    public void SetTargetTown(BaseTown targetTown)
    {
        this.targetTown = targetTown;
    }

    public BaseTown GetTargetTown()
    {
        return this.targetTown;
    }

    public void SufferInjure(float injure)
    {
        throw new System.NotImplementedException();
    }

    public void SufferInjure(int injure)
    {
        Hp -= injure;
    }
    
    public void DoAttack()
    {
        AttackTimeStamp += Time.deltaTime;
        if (AttackTimeStamp >= AttackInterval)
        {
            AttackTimeStamp = 0;
            targetSolider.BeAttack(this,Damage);
        }
    }

    //判断周围是否有敌人
    public bool CheckEnemy()
    {
        if (targetSolider != null && targetSolider.IsDead() == false)
        {
            return true;
        }
        RaycastHit hit = new RaycastHit();
        Collider[] hits = new Collider[]{};
        hits = Physics.OverlapSphere(this.transform.position, ViewRedius);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                var tempSolider = hits[i].GetComponent<Solider>(); 
                if (tempSolider && tempSolider.OwnerType != this.OwnerType)
                {
                    targetSolider = hits[i].GetComponent<Solider>();
                }
            }
        }
        return targetSolider != null;
    }

    public bool IsCanAttackEnemy()
    {
        CheckEnemy();
        if (targetSolider != null)
        {
            navMeshAgent.SetDestination(targetSolider.transform.position);
            if (Vector3.Distance(this.transform.position,targetSolider.transform.position) <= ViewAttackRedius)
            {
                navMeshAgent.isStopped = true;
                return true;
            }
        }
        return false;
    }
    
    public bool IsDead()
    {
        return false;
    }

    //被攻击
    protected void BeAttack(Solider attacker, int damageNum)
    {
        SufferInjure(damageNum);
        if (Hp <= damageNum)
        {
            if (ChangeState(State.Dead))
            {
                StartCoroutine(DeadSuccess());
            }
        }
        if (targetSolider == null || targetSolider != attacker)
        {
            targetSolider = attacker;
        }
    }

    protected IEnumerator DeadSuccess()
    {
        yield return new WaitForSeconds(1);
        DestroyImmediate(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,ViewRedius);
    }

    //改变装填
    public bool ChangeState(State state)
    {
        if (curState == state)
        {
            return false;
        }
        ResetAniState();
        switch (state)
        {
            case State.Moving:
                Animator.SetBool("Move",true);
                break;
            case State.Idleing:
                Animator.SetBool("Idle",true);
                break;
            case State.Dead:
                Animator.SetTrigger("Dead");
                break;
            default:
                Animator.SetBool("Attack",true);
                break;
        }
        curState = state;
        return true;
    }

    protected void ResetAniState()
    {
        Animator.SetBool("Move",false);
        Animator.SetBool("Idle",false);
        Animator.SetBool("Attack",false);
    }
    
    public void MoveToTarget(Vector3 targetPoint)
    {
        ChangeState(State.Moving);
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(targetPoint);
        navMeshAgent.stoppingDistance = 2;
    }
}