using System.Collections;
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

    public enum AniState
    {
        Move,
        Attack,
        Die,
        Idle,
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
    
    private float AttackTimeStamp;
    private float AttackInterval = 1;
    void Update()
    {
        if (Solider_Enemy == null)
        {
            CheckEnemy();
        }

        bool IsCanAttck = false;
        if (Solider_Enemy != null )
        { 
            navMeshAgent.SetDestination(Solider_Enemy.transform.position);
            if (Vector3.Distance(this.transform.position,Solider_Enemy.transform.position) <= ViewAttackRedius)
            {
                ChangeState(State.Attack_Enemy);
                UpdateAttack();
            }
            else
            {
                ChangeState(State.Idleing);
                Solider_Enemy = null;
            }
        }
    }

    void UpdateAttack()
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

    //攻击敌人
    protected IEnumerator Attack()
    {
        if (Solider_Enemy != null && Solider_Enemy.IsDead() == false)
        {
            ChangeState(State.Moving);
            navMeshAgent.SetDestination(Solider_Enemy.transform.position);
            navMeshAgent.stoppingDistance = ViewAttackRedius;
            yield return  new WaitUntil(()=> navMeshAgent.isStopped);
            ChangeState(State.Attack_Enemy);
            Solider_Enemy.BeAttack(this,Damage);
            yield return new WaitUntil(() => Solider_Enemy.IsDead());
            ChangeState(State.Idleing);
        }
        else
        {
            yield return null;
        }
    }

    protected void AttackSucces()
    {
        if (Solider_Enemy != null && Solider_Enemy.IsDead() == false)
        {
            Solider_Enemy.BeAttack(this,Damage);
        }
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
