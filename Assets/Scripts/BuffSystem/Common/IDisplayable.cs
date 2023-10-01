namespace BuffSystem.Common
{
    public interface IDisplayable
    {
        /// <summary>
        /// The smaller the number is, the higher the priority is.
        /// E.g., priority of 1 displays before priority 3.
        /// </summary>
        int Priority { get; }
    }
}