using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FuncBoolUtil
{
    private List<Func<bool>> _evaluatedTrueMethods;
    private List<Func<bool>> _unsatisfiedMethods;


    /// <summary>
    ///     Evaluate the final value of a Func<bool> event. It is true only if all the methods in it are true.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public static bool Evaluate(Func<bool> func)
    {
        if (func == null)
        {
            return true;
        }

        return func.GetInvocationList().Cast<Func<bool>>().All(method => method());
    }
}