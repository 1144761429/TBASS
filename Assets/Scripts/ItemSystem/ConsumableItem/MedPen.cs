using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedPen : ItemConsumableSupply
{
    public override void Consume()
    {
        base.Consume();
        print("Consumed Med Pen. Real effect to be implemented");
    }
}
