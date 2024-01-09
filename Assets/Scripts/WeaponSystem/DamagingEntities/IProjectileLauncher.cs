using System;
using UnityEngine;

namespace WeaponSystem.DamagingEntities
{
    public interface IProjectileLauncher
    {
        /// <summary>
        /// A series of condition that has to be satisfied to start launch.
        /// Think this as a action that is required to do before launch.
        /// For example, pressing the left mouse button could be a trigger condition.
        /// </summary>
        Func<bool> LaunchTriggerCondition { get; }
        
        /// <summary>
        /// Action to be taken before checking launch condition.
        /// </summary>
        Action BeforeCheckLaunchCondition { get; }
        
        /// <summary>
        /// A series of condition that has to be satisfied first before continuing to check secondary launch condition launching.
        /// </summary>
        Func<bool> PrimaryLaunchCondition { get; }
        
        // TODO: consider to remove this. For more detail, check HarpoonMod.cs script
        /// <summary>
        /// A series of condition that has to be satisfied before launching.
        /// </summary>
        Func<bool> SecondaryLaunchCondition { get; }
        
        /// <summary>
        /// Action to be taken right before launching.
        /// </summary>
        Action BeforeLaunch { get; }
        
        /// <summary>
        /// Action to be taken when launching.
        /// </summary>
        Action OnLaunch { get; }

        /// <summary>
        /// Action to be taken right after launching.
        /// </summary>
        Action AfterLaunch { get; }
        
        /// <summary>
        /// Action to be taken when the launched projectile hit something.
        /// </summary>
        Action<GameObject> OnHit { get; }
        
        /// <summary>
        /// The projectile to be launched.
        /// </summary>
        Projectile Projectile { get; }

        /// <summary>
        /// The pattern of how projectile should be launched.
        /// </summary>
        ProjectilePattern ProjectilePattern { get; }
        
        /// <summary>
        /// Method for launching the projectile using this launcher.
        /// </summary>
        void Launch();
    }
}