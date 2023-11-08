using UnityEngine;

namespace Characters.Enemies
{
    public class PatrolRioter : Enemy
    {
        private EnemyStateMachine_PatrolRioter _sm;

        public override int ID => 10001;

        protected override void Awake()
        {
            base.Awake();

            _sm = new EnemyStateMachine_PatrolRioter(this);
            _sm.Init();
        }

        protected override void Update()
        {
            base.Update();
            
            _sm.UpdateStateMachine();
            
        }
    }
}