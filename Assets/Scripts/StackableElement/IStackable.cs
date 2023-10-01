namespace StackableElement
{
    public interface IStackable
    {
        int Stack { get; }
        int MinStack { get; }
        int MaxStack { get; }
        bool IsStackable { get; }
        bool IsFrozen { get; }
    }
}