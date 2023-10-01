using BuffSystem.Common;

namespace BuffSystem.Interface
{
    public interface IAttackEffectBuff
    {
        void OnAttack(IBuffable target);
    }
}