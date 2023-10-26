using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Util
{
    public abstract class VariableDebugger
    {
        public static string DictionaryToString<TKey, TValue>(Dictionary<TKey,TValue> dictionary)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var kyPair in dictionary)
            {
                stringBuilder.Append($"{kyPair.Key.ToString()} : {kyPair.Value.ToString()}\n");
            }
            
            return stringBuilder.ToString();
        }
        
        public static string ListToString<TValue>(List<TValue> list)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var value in list)
            {
                stringBuilder.Append($"{value.ToString()}\n");
            }
            
            return stringBuilder.ToString();
        }
        
        public static string ArrToString<TValue>(TValue[] arr)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var value in arr)
            {
                stringBuilder.Append($"{value.ToString()}\n");
            }
            
            return stringBuilder.ToString();
        }
    }
}