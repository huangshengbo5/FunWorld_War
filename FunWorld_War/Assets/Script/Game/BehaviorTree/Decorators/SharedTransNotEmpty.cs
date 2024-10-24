using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class SharedTransNotEmpty : Decorator
    {
        public SharedTransform In_SharedTrans;

        public override bool CanExecute()
        {
            return In_SharedTrans != null && In_SharedTrans.Value != null;
        }
    }
}