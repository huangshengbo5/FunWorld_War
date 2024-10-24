using System.Collections;
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
            nav.isStopped = false;
            nav.SetDestination(targetTrans.Value.position);
            nav.stoppingDistance = 0;
        }
        
        // public override void OnEnd()
        // {
        //     base.OnEnd();
        //     nav.isStopped = true;
        // }
    }
}