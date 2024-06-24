using System.Collections;
using DG.Tweening;
using Script.Game;
using UnityEngine;
using UnityEngine.AI;

public class BaseSolider : MonoBehaviour
{
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
    
    protected Transform target;

    //敌人
    protected BaseSolider Solider_Enemy;
    public NavMeshAgent navMeshAgent;

    //血量
    public int Hp;
    //伤害
    public int Damage;
    
    //造成伤害时间间隔信息
    private float AttackTimeStamp;
    private float AttackInterval = 1;
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public Transform GetTarget()
    {
        return this.target;
    }

    public void SufferInjure(int injure)
    {
        Hp -= injure;
    }
    

    void Update()
    {
        if (Solider_Enemy == null)
        {
            CheckEnemy();
        }
        if (Solider_Enemy != null && curState != State.Attack_Enemy)
        {
            navMeshAgent.SetDestination(Solider_Enemy.transform.position);
            if (Vector3.Distance(this.transform.position,Solider_Enemy.transform.position) <= ViewAttackRedius)
            {
                DoRotateToTarget(Solider_Enemy.transform);
                ChangeState(State.Attack_Enemy);
                DoAttack();
            }
            else
            {
                if (curState != State.Idleing)
                {
                    ChangeState(State.Idleing);
                    Solider_Enemy = null;
                }
            }
        }
    }

    void DoRotateToTarget(Transform targetTrans)
    {
        // var dir = targetTrans.position - transform.position;
        // transform.DOLookAt( dir, 0.5f);
        transform.LookAt(targetTrans.position);
    }
    
    void DoAttack()
    {
        AttackTimeStamp += Time.deltaTime;
        if (AttackTimeStamp >= AttackInterval)
        {
            AttackTimeStamp = 0;
            Solider_Enemy.BeAttack(this,Damage);
        }
    }

    //判断周围是否有敌人
    protected bool CheckEnemy()
    {
        if (Solider_Enemy != null && Solider_Enemy.IsDead() == false)
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
                var tempSolider = hits[i].GetComponent<BaseSolider>(); 
                if (tempSolider && tempSolider.OwnerType != this.OwnerType)
                {
                    Solider_Enemy = hits[i].GetComponent<BaseSolider>();
                }
            }
        }
        return Solider_Enemy != null;
    }

    public bool IsDead()
    {
        return false;
    }

    //被攻击
    protected void BeAttack(BaseSolider attacker, int damageNum)
    {
        SufferInjure(damageNum);
        if (Hp <= damageNum)
        {
            ChangeState(State.Dead);
            StartCoroutine(DeadSuccess());
        }

        if (Solider_Enemy != null && Solider_Enemy != attacker)
        {
            Solider_Enemy = attacker;
            ChangeState(State.Attack_Enemy);
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
    protected void ChangeState(State state)
    {
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
    }

    protected void ResetAniState()
    {
        Animator.SetBool("Move",false);
        Animator.SetBool("Idle",false);
        Animator.SetBool("Attack",false);
    }
    
    public  IEnumerator MoveToTarget(Vector3 targetPoint)
    {
        ChangeState(State.Moving);
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(targetPoint);
        navMeshAgent.stoppingDistance = 2;
        yield return null;
    }
}
