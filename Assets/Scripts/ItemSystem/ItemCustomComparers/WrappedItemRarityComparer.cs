using System.Collections.Generic;
using ArgumentNullException = System.ArgumentNullException;

namespace ItemSystem.ItemCustomComparers
{
    public class WrappedItemRarityComparer : IComparer<WrappedItem>
    {
        public int Compare(WrappedItem left, WrappedItem right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left),"Wrapped item to compare cannot be null.");
            }
            
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right),"Wrapped item to compare cannot be null.");
            }
            
            EItemRarity leftRarity = left.StaticData.Rarity;
            EItemRarity rightRarity = right.StaticData.Rarity;

            int result = leftRarity - rightRarity;

            if (result == 0)
            {
                result = left.ID - right.ID;
            }
            
            return result;
        }
    }
}