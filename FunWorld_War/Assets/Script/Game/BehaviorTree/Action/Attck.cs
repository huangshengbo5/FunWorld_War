using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class Solider_Attack : Action
    {
        public override void OnAwake()
        {
            base.OnAwake();
            var solider = Owner.GetComponent<Solider>();
            solider.DoAttack();
        }
    }
}