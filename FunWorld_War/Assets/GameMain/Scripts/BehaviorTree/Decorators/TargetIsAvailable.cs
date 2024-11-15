using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class TargetIsCanAttack : Decorator
    {
        public SharedTransform In_TargetTrans;

        public override bool CanExecute()
        {
            if (In_TargetTrans == null) return false;
            var town = In_TargetTrans.Value;
            if (town is null) return false;
            
            var ownerTrans = Owner.GetComponent<Transform>();
            Solider solider;
            if (town.TryGetComponent<Solider>(out solider))
            {
                if (solider.IsDead())
                {
                    return false;
                }
            }
            Town city;
            if (town.TryGetComponent<Town>(out city))
            {
                var selfCamp = Owner.GetComponent<Solider>().CampType;
                var cityCamp = city.Camp();
                if (cityCamp == selfCamp)
                {
                    return false;
                }
            }

            var attackRedius = Owner.GetComponent<Solider>().AttackRedius;
            if (Vector3.Distance(ownerTrans.position,town.position) < attackRedius)
            {
                return true;
            }
            return false;
        }
    }
}