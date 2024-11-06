using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class MoveToTarget : Action
    {
        //目标
        public SharedTransform targetTrans; 
        
        //距离目标多远停止
        public float stoppingDistance = 2f;
        
        //延迟时间
        public float delay; 
        
        private NavMeshAgent nav;

        public override void OnAwake()
        {
            base.OnAwake();
            nav = Owner.GetComponent<NavMeshAgent>();
        }

        public override void OnStart()
        {
            base.OnStart();
            StartCoroutine(DelayMove());
        }

        private IEnumerator DelayMove()
        {
            yield return new WaitForSeconds(delay);
            if (targetTrans != null && targetTrans.Value != null)
            {
                nav.isStopped = false;
                nav.SetDestination(targetTrans.Value.position);
            }
            else
            {
                nav.isStopped = true;
            }
            nav.stoppingDistance = 4;
        }

        public override TaskStatus OnUpdate()
        {
            var isStop = CheckDestinationReached(nav);
            Debug.Log("Solider MoveToTarget  is Stopped :"+isStop);
            return isStop ? TaskStatus.Success : TaskStatus.Running;
        }

        public bool CheckDestinationReached(NavMeshAgent agent)
        {
            if (agent.hasPath)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    return true;
                }
            }
            return false;
        }
    }
}