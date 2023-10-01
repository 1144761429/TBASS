namespace BuffSystem.Common
{
    public interface IBuffable
    {
        /// <summary>
        /// True if a <c>Buff</c> can be added to the <c>IBuffable</c> object.
        /// False if the object cannot. 
        /// </summary>
        bool CanTakeBuff { get; }

        /// <summary>
        /// True if <c>Bleed</c> can be added to the <c>IBuffable</c> object.
        /// False if the object cannot.
        /// </summary>
        bool IsBleedResist { get; }

        BuffHandler BuffHandler { get; }
    }
}