using UnityEngine;

namespace Characters.Enemies
{
    public class PatrolRioter : Enemy
    {
        public override int ID => 10001;

        public override int Priority => 1; 
        
        private EnemyStateMachine_PatrolRioter _sm;
        
        
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