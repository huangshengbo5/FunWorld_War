using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class TargetIsTown : Decorator
    {
        public SharedTransform In_TargetTrans;

        public override bool CanExecute()
        {
            var town = In_TargetTrans.Value;
            Town_City city;
            if (town.TryGetComponent<Town_City>(out city))
            {
                return true;
            }
            return false;
        }
    }
}