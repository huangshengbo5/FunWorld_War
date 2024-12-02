using System.Collections;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public partial class Solider : BaseObject
{
    private BehaviorTree behaviorTree;

    private bool isKill;
    //归属的部队
    private SoliderCommander ownerSoliderCommander;

    public SoliderHUD soliderHUD;

    public SoliderCommander OwnerSoliderCommander
    {
        get => ownerSoliderCommander;
        set => ownerSoliderCommander = value;
    }

    public override ObjectType ObjectType()
    {
        return global::ObjectType.Solider;
    }

    public void Init()
    {
        MaxHp = Hp;
        InitBehaviorTree();
        isKill = false;
        soliderHUD.Init(this);
        soliderHUD.UpdatgeHP(Hp,MaxHp);
    }

    public void Init_SoliderCommander(SoliderCommander soliderCommander)
    {
        if (OwnerSoliderCommander != soliderCommander)
        {
            OwnerSoliderCommander = soliderCommander;
        }
        else
        {
            Debug.LogError("OwnerSoliderCommander == soliderCommander!");
        }
    }
    
    public void InitBehaviorTree()
    {
        behaviorTree= this.gameObject.GetComponent<BehaviorTree>();
    }
    
    public enum State
    {
        Idleing,      //休闲
        Moving,       //移动
        Attack_Enemy,  //攻击敌人
        Dead,          //死亡
    }

    private CampType campType;

    public CampType CampType
    {
        get => campType;
        set => campType = value;
    }

    public State curState;   //当前的单位状态
    
    public int ViewRedius;   //视野半径
    public int AttackRedius;  //攻击半径
    
    public Animator Animator;
    
    protected BaseObject targetObject;
    

    //士兵归属的城镇
    protected Town ownerTown;

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

    public Town OwnerTown
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
        targetObject = targetTown;
        behaviorTree.SetVariableValue("TargetTrans",targetObject ? targetObject.transform : null);
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
        soliderHUD.UpdatgeHP(Hp,MaxHp);
    }
    
    public void DoAttack()
    {
        ChangeAnimatorState(State.Attack_Enemy);
    }

    public void ChangeState(State state)
    {
        ChangeAnimatorState(state);
    }
    

    public void OnAttackHited()
    {
        if (targetObject is Solider)
        {
            var targetSolider = targetObject as Solider;
            if (!targetSolider.IsDead())
            {
                targetSolider.BeAttack(this,this.Damage);
            }
            else
            {
                ChangeTargetObject(null);
            }
        }
        else if (targetObject is Town)
        {
            var targetTown = targetObject as Town;
            if (targetTown.IsOccupied == false)
            {
                targetTown.BeAttack(this,this.Damage);
            }
        }
    }
    
    public bool IsDead()
    {
        return curState == State.Dead;
    }

    //被攻击
    public override void BeAttack(BaseObject attacker, int damageNum)
    {
        if (isKill)
        {
            return;
        }
        SufferInjure(damageNum);
        if (Hp <= damageNum)
        {
            if (ChangeAnimatorState(State.Dead))
            {
                isKill = true;
                StartCoroutine(DeadSuccess());
            }
        }
        else if (targetObject == null || targetObject != attacker)
        {
            ChangeTargetObject(attacker);
        }
    }

    protected IEnumerator DeadSuccess()
    {
        ownerSoliderCommander.RemoveSolider(this);
        behaviorTree.OnDestroy();
        behaviorTree = null;
        yield return new WaitForSeconds(1);
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

    //进城，攻城成功或者守城成功
    public void EnterTown()
    {
        ChangeAnimatorState(State.Moving);
        Debug.Log("士兵进城！！！");
        behaviorTree.OnDestroy();
        behaviorTree = null;
        this.gameObject.SetActive(false);
    }

    public BaseObject FindEnemy()
    {
        return OwnerSoliderCommander.SoliderFindTarget(this);
    }
}