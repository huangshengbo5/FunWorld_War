using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class UnitTest : Unit
{
    public GameObject TargetActor;            //目标对象

    public enum UnitState
    {
        Idleing,      //休闲
        Moving,       //移动
        Attack_City,   //攻击城市
        Attack_Enemy,  //攻击敌人
        Dead,          //死亡
    }
    
    //士兵目标类型
    public enum UnitTargetType 
    {
        City,   //城市
        Enemy,  //敌人
    }

    public UnitState curUnitState;   //当前的单位状态

    
    protected override void Awake()
    {
        base.Awake();
        navigation = GetComponent<MovementNavigation>();
        animation = Obj_Ani.GetComponent<Animator>(); 
        resourceCollector = GetComponent<ResourceCollector>();
    }
    
    protected void MoveToEnemy_Internal(GameObject Enemy)
    {
        
    }

    protected void AttackAnim_Interanl(bool attack)
    {
        animation?.SetBool(UnitAnimation.StateNames.DoAttack, false);
    }
    
    public void MoveToCity(GameObject city)
    {
        var cityObj = city.GetComponent<ClickableObject>();
        if (cityObj)
        {
            curUnitState = UnitState.Moving;
            navigation.stoppingDistance = template.engageDistance;
            targetOfMovement = cityObj.transform.position;
            navigation.SetDestination(targetOfMovement.Value);
            navigation.isStopped = false;
            AttackAnim_Interanl(false);
        }
    }

    protected override void Update()
    {
        base.Update();
        
    }
    
    //references

    protected Animator animation;
    protected MovementNavigation navigation;
    protected ResourceCollector resourceCollector;

    protected List<AICommand> commandList = new List<AICommand>();

    protected bool commandRecieved, commandExecuted;

    protected AICommand.CustomActions? customAction;

    protected InteractableObject targetOfAttack;
    protected Vector3? targetOfMovement;
    protected Vector3? returnPoint;

    private Coroutine lerpingCombatReady;
    private Coroutine lerpingAttackEvent;

    // protected override void Awake()
    // {
    //     base.Awake();
    //     navigation = GetComponent<MovementNavigation>();
    //     //animation = GetComponent<UnitAnimation>();
    //     animation = Obj_Ani.GetComponent<Animator>(); 
    //     resourceCollector = GetComponent<ResourceCollector>();
    // }

    protected override void Start()
    {
        if (faction == null)
        {
            throw new System.NullReferenceException(string.Concat("No faction assigned to: ", gameObject.name));
        }
        faction.data.units.Add(this);

        UpdateMaterialTeamColor();

        //Set some defaults, including the default state
        SetSelected(false);

        base.Start();
    }



#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        if (navigation != null && navigation.isOnMesh && navigation.hasPath)
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawLine(transform.position, navigation.destination);

            if (targetOfMovement.HasValue)
            {
                UnityEditor.Handles.color = Color.yellow;
                UnityEditor.Handles.DrawLine(transform.position, targetOfMovement.Value);
            }
        }

        base.OnDrawGizmos();
    }
#endif

    public void AddCommand(AICommand command, bool clear = false)
    {
        // if (!CheckCommandViability(command))
        // {
        //     Debug.LogWarning(string.Concat("Command not accepted: ", command.commandType, " ", command.customAction), this);
        //     return;
        // }
        if (clear || command.commandType == AICommand.CommandTypes.Stop)
        {
            commandList.Clear();
            commandExecuted = false;
            commandRecieved = false;
        }
        if (command.commandType != AICommand.CommandTypes.Stop)
        {
            commandList.Add(command);
        }
    }

    private bool CheckCommandViability(AICommand command)
    {
        //make units be able to denie command... oh what could possibly go wrong
        switch (command.commandType)
        {
            case AICommand.CommandTypes.MoveTo:
                //case AICommand.CommandTypes.AttackMoveTo:
                //case AICommand.CommandTypes.Guard:
                return !command.destination.IsNaN();
            case AICommand.CommandTypes.AttackTarget:
                return command.target != null && !command.target.attackable.isDead && command.target != this;
            case AICommand.CommandTypes.Stop:
            case AICommand.CommandTypes.Die:
                return true;
            case AICommand.CommandTypes.CustomActionAtPos:
                if (!command.customAction.HasValue)
                {
                    throw new System.ArgumentNullException();
                }
                return template.original.customActions.Contains(command.customAction.Value) && !command.destination.IsNaN();
            case AICommand.CommandTypes.CustomActionAtObj:
                if (!command.customAction.HasValue)
                {
                    throw new System.ArgumentNullException();
                }
                return template.original.customActions.Contains(command.customAction.Value) && command.target != null && !command.target.attackable.isDead;
        }
        throw new System.NotImplementedException(string.Concat("Command Type '", command.commandType.ToString(), "' not valid"));
    }

  
   
    private bool SeekNewEnemies()
    {
        IEnumerable<ClickableObject> enemies;

        Collider[] colliders = Physics.OverlapSphere(transform.position, template.guardDistance, InputManager.Instance.unitsLayerMask, QueryTriggerInteraction.Collide);
        enemies = colliders.Select(collider => collider.GetComponent<ClickableObject>()).Where(clickable => clickable != null && !FactionTemplate.IsAlliedWith(faction, clickable.faction));

        ClickableObject closest = null;
        float distanceToClosestSqr = float.PositiveInfinity;
        foreach (var enemy in enemies)
        {
            float distanceSqr = (enemy.transform.position - targetOfMovement.Value).sqrMagnitude;
            if (distanceSqr < distanceToClosestSqr)
            {
                distanceToClosestSqr = distanceSqr;
                closest = enemy;
            }
        }

        if (closest == null)
        {
            return false;
        }

        returnPoint = transform.position;
        targetOfAttack = closest;
        targetOfMovement = targetOfAttack.transform.position;
        return true;
    }

  
    private void ShootProjectileAtTarget(int damage)
    {
        if (template.projectile == null || template.projectile.GetComponent<Projectile>() == null)
        {
            throw new System.NullReferenceException("This unit has no Projectile set");
        }
        if (projectileFirePoint == null)
        {
            throw new System.NullReferenceException("This unit has no Projectile Fire Point set");
        }

        Projectile projectileInstance = Instantiate(template.projectile, projectileFirePoint.position, projectileFirePoint.rotation).GetComponent<Projectile>();
        projectileInstance.LaunchAt(targetOfAttack.transform, damage, this);
    }

    public override void Die()
    {

        base.Die();

        commandExecuted = true;

        commandList.Clear();

        navigation.isStopped = true;
        navigation.enabled = false;

        AttackAnim(false);
        animation.SetTrigger(UnitAnimation.StateNames.DoDeath);

        ///Remove itself from the selection Platoon
        GameManager.Instance.RemoveFromSelection(this);
        SetSelected(false);

        faction.data.units.Remove(this);

        ///Remove unneeded Components
        StartCoroutine(HideSeenThings(visionFadeTime));
        StartCoroutine(VisionFade(visionFadeTime, true));
        ///navMeshAgent.enabled = false;
        StartCoroutine(DecayIntoGround());
    }

    private void SetWalkingSpeed()
    {
        float navMeshAgentSpeed = navigation.velocity.magnitude;
        animation?.SetFloat(UnitAnimation.StateNames.Speed, navMeshAgentSpeed * 0.05f);
    }

    private void FaceTarget()
    {
        Vector3 dir = (targetOfMovement.Value - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(dir.ToWithY(0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
    }

    public override void SetVisibility(bool visibility, bool force = false)
    {
        if (!force && visibility == visible)
        {
            return;
        }

        base.SetVisibility(visibility, force);

        if (visible)
        {
            UIManager.Instance.AddHealthbar(this);
        }
        else
        {
            if (OnDisapearInFOW != null)
            {
                OnDisapearInFOW.Invoke(this);
            }
        }
    }

    public void AdjustModelAngleToGround()
    {
        Ray ray = new Ray(selectionCircle.transform.position + Vector3.up * 0.1f, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f, InputManager.Instance.groundLayerMask))
        {
            Quaternion newRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * selectionCircle.transform.parent.rotation;
            newRotation = Quaternion.Lerp(selectionCircle.transform.rotation, newRotation, Time.deltaTime * 8f);
            if (alignToGround)
            {
                modelHolder.rotation = newRotation;
            }
            selectionCircle.transform.rotation = newRotation;
        }
    }

    public bool SetCombatReady(bool state)
    {
        //if (animation.CheckParameterExistance(UnitAnimation.StateNames.DoCombatReady))
        //{
        //    float value = animation.GetFloat(UnitAnimation.StateNames.DoCombatReady);
        //    float stat = state.ToFloat();
        //    if (value != stat)
        //    {
        //        if (lerpingCombatReady != null)
        //        {
        //            StopCoroutine(lerpingCombatReady);
        //        }
        //        lerpingCombatReady = StartCoroutine(LerpCombatReadyAnim(state.ToFloat()));
        //        return true;
        //    }
        //}
        return false;
    }

    //private IEnumerator LerpCombatReadyAnim(float state)
    //{
    //    float start = animation.GetFloat(UnitAnimation.StateNames.DoCombatReady);
    //    float key = start;
    //    float value;
    //    for (; ; )
    //    {
    //        key = Mathf.MoveTowards(key, state, Time.deltaTime * combatReadySwitchTime);
    //        value = combatReadyAnimCurve.Evaluate(key);
    //        animation.SetFloat(UnitAnimation.StateNames.DoCombatReady, value);
    //        if (key != state)
    //        {
    //            yield return null;
    //        }
    //        else
    //        {
    //            lerpingCombatReady = null;
    //            yield break;
    //        }
    //    }
    //}

    private void AttackAnim(bool state)
    {
        if (state)
        {
            if (lerpingAttackEvent != null)
            {
                return;
            }
            else
            {
                lerpingAttackEvent = StartCoroutine(LerpAttackEvent());
            }
        }
        else if (lerpingAttackEvent != null)
        {
            animation?.SetBool(UnitAnimation.StateNames.DoAttack, false);
            StopCoroutine(lerpingAttackEvent);
            lerpingAttackEvent = null;
        }
    }

    private IEnumerator LerpAttackEvent()
    {
        animation?.SetBool(UnitAnimation.StateNames.DoAttack, false);
        yield return null;
        yield return null;
        animation?.SetBool(UnitAnimation.StateNames.DoAttack, true);

        float lenght = float.NaN;
        if (animation != null)
        {
            // lenght = animation.GetCurrentAnimationLenght();
            // yield return Yielders.Get(lenght * template.attackEventTime);
        }


        if (animation != null)
        {
            yield return Yielders.Get(lenght * (1f - template.attackEventTime));
        }

        lerpingAttackEvent = null;
    }

#if UNITY_EDITOR
    public List<AICommand> listForEditor { get { return commandList; } }
#endif
}
