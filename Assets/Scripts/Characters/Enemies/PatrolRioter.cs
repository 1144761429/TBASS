using UnityEngine;

namespace Characters.Enemies
{
    public class PatrolRioter : Enemy
    {
        public override int ID => 10001;

        public override int TargetPriority => 1;
    }
}