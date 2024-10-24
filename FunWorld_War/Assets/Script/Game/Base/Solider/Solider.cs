using System.Collections;
using BehaviorDesigner.Runtime;
using Script.Game.Base;
using UnityEngine;
using UnityEngine.AI;

public partial class Solider : BaseObject, SoliderInterface
{
    private BehaviorTree behaviorTree;

    public override ObjectType ObjectType()
    {
        return global::ObjectType.Solider;
    }

    public void Init()
    {
        InitBehaviorTree();
    }
    
    public void InitBehaviorTree()
    {
        behaviorTree= this.gameObject.GetComponent<BehaviorTree>();
    }
    
    // public void InitFsm()
    // {
    //     var fsmName = "FSM_" + this.gameObject.name;
    //     var fsm = GameEntry.Fsm.CreateFsm<Solider>(fsmName,this, 
    //         new FSMSoliderIdle(),
    //         new FSMSoliderDead(),
    //         new FSMSoliderAttack(),
    //         new FSMSoliderMoveToTarget());
    //     fsm.Start<FSMSoliderIdle>();
    // }
    
    public enum State
    {
        Idleing,      //休闲
        Moving,       //移动
        Attack_Enemy,  //攻击敌人
        Dead,          //死亡
    }

    public CampType campType;

    public CampType CampType
    {
        get => campType;
        set => campType = value;
    }

    public State curState;   //当前的单位状态
    
    public int ViewRedius;   //视野半径
    public int ViewAttackRedius;  //攻击半径
    
    public Animator Animator;
    
    protected BaseObject targetObject;
    

    //士兵归属的城镇
    protected BaseTown ownerTown;

    private bool retreat;  //撤退

    public bool Retreat
    {
        get => retreat;
        set => retreat = value;
    }
    

    public BaseObject TargetObject
    {
        get
        {
            return this.targetObject;
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
    //伤害数值
    public int Damage;
    
    //造成伤害时间间隔信息
    private float AttackTimeStamp;
    private float AttackInterval = 1;

    public Solider(bool retreat)
    {
        Retreat = retreat;
    }

    public void ChangeTargetObject(BaseObject targetTown)
    {
        this.targetObject = targetTown;
        behaviorTree.SetVariableValue("TargetTrans",this.targetObject.transform);
    }

    
    public BaseObject GetTargetObject()
    {
        return this.targetObject;
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
        ChangeAnimatorState(State.Attack_Enemy);
        Debug.Log("Solider DoAttack");
    }

    public void OnAttackHited()
    {
        if (targetObject is Solider)
        {
            var targetSolider = targetObject as Solider;
            if (! targetSolider.IsDead())
            {
                targetSolider.BeAttack(this,this.Damage);
            }
            else
            {
                ChangeTargetObject(null);
            }
        }
    }
    
    //判断周围是否有敌人
    public bool CheckEnemy()
    {
        // if (targetSolider != null && targetSolider.IsDead() == false)
        // {
        //     return true;
        // }
        // RaycastHit hit = new RaycastHit();
        // Collider[] hits = new Collider[]{};
        // hits = Physics.OverlapSphere(this.transform.position, ViewRedius);
        // if (hits.Length > 0)
        // {
        //     for (int i = 0; i < hits.Length; i++)
        //     {
        //         var tempSolider = hits[i].GetComponent<Solider>(); 
        //         if (tempSolider && tempSolider.campType != this.campType)
        //         {
        //             targetSolider = hits[i].GetComponent<Solider>();
        //         }
        //     }
        // }
        // return targetSolider != null;
        return false;
    }

    public bool IsCanAttackEnemy()
    {
        // CheckEnemy();
        // if (targetSolider != null)
        // {
        //     navMeshAgent.SetDestination(targetSolider.transform.position);
        //     if (Vector3.Distance(this.transform.position,targetSolider.transform.position) <= ViewAttackRedius)
        //     {
        //         navMeshAgent.isStopped = true;
        //         return true;
        //     }
        // }
        return false;
    }
    
    public bool IsDead()
    {
        return this.Hp <=0 ;
    }

    //被攻击
    protected void BeAttack(Solider attacker, int damageNum)
    {
        SufferInjure(damageNum);
        if (Hp <= damageNum)
        {
            if (ChangeAnimatorState(State.Dead))
            {
                StartCoroutine(DeadSuccess());
            }
        }
        if (targetObject == null || targetObject != attacker)
        {
            ChangeTargetObject(attacker);
        }
    }

    protected IEnumerator DeadSuccess()
    {
        behaviorTree.OnDestroy();
        behaviorTree = null;
        yield return new WaitForSeconds(1);
        //DestroyImmediate(this.gameObject);
        this.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,ViewRedius);
    }

    //改变装填
    public bool ChangeAnimatorState(State state)
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
        ChangeAnimatorState(State.Moving);
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(targetPoint);
        navMeshAgent.stoppingDistance = 2;
    }
    
}