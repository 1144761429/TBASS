using System;

namespace WeaponSystem
{
    /// <summary>
    /// A class for storing the common events a weapon has.
    /// </summary>
    public class WeaponEventBundle
    {
        public Func<bool> MainFuncTriggerCondition;
        public Action MainFunc;
        public Func<bool> MainFuncCancelCondition;
        public Action MainFuncCancelCallback;

        public Action MainFuncConditionFail;
            
        public Func<bool> AltFuncTriggerCondition;
        public Action AltFunc;
        public Func<bool> AltFuncCancelCondition;
        public Action AltFuncCancelCallback;

        public Action AltFuncConditionFail; 
        
        public Func<bool> ReloadTriggerCondition;
        public Action ReloadCallback;

        /// <summary>
        /// Set all the events of a weapon to null.
        /// </summary>
        public void Reset()
        {
            MainFuncTriggerCondition = null;
            MainFunc = null;
            MainFuncCancelCondition = null;
            MainFuncCancelCallback = null;

            MainFuncConditionFail = null;
            
            AltFuncTriggerCondition = null;
            AltFunc = null;
            AltFuncCancelCondition = null;
            AltFuncCancelCallback = null;

            AltFuncConditionFail = null;

            ReloadTriggerCondition = null;
            ReloadCallback = null;
        }
    }
}