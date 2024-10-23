using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class TargetIsCanAttack : Decorator
    {
        public SharedTransform In_TargetTrans;

        public override bool CanExecute()
        {
            var town = In_TargetTrans.Value;
            var ownerTrans = Owner.GetComponent<Transform>();
            Solider solider;
            if (town.TryGetComponent<Solider>(out solider))
            {
                if (solider.IsDead())
                {
                    return false;
                }
            }
            Town_City city;
            if (town.TryGetComponent<Town_City>(out city))
            {
                var selfCamp = Owner.GetComponent<Solider>().campType;
                if (city.Camp() == selfCamp)
                {
                    return false;
                }
            }

            var attackRedius = Owner.GetComponent<Solider>().ViewAttackRedius;
            if (Vector3.Distance(ownerTrans.position,town.position) < attackRedius)
            {
                return true;
            }
            return false;
        }
    }
}