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
        public override void OnReset()
        {
            base.OnReset();
            StartCoroutine(DelayMove());
        }
    
        private IEnumerator DelayMove()
        {
            yield return new WaitForSeconds(delay);
            nav = Owner.GetComponent<NavMeshAgent>();
            var target = targetTrans.GetValue() as Transform;
            nav.SetDestination(target.position);
            nav.stoppingDistance = stoppingDistance;
        }
    
        public override void OnBehaviorComplete()
        {
            base.OnBehaviorComplete();
            nav.isStopped = true;
        }
    
        public override void OnEnd()
        {
            base.OnEnd();
            nav.isStopped = true;
        }
    
        public override void OnPause(bool paused)
        {
            base.OnPause(paused);
            if (paused)
            {
                nav.isStopped = true;
            }
        }
    }
}