using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInput PlayerInput;
    
    #region Movement

    private InputAction _movementAction;
    private InputAction _sprintAction;
    
    public static Vector2 MovementInput { get; private set; }
    public static bool IsHoldingSprint { get; private set; }
    
    #endregion

    #region Weapon

    // private InputAction _weaponMainFuncAction;
    // private InputAction _weaponAltFuncAction;
    private InputAction _weaponReloadAction;
    private InputAction _switchToPrimaryWeaponAction;
    private InputAction _switchToSecondaryWeaponAction;
    private InputAction _switchToAdeptWeaponAction;
    
    public static bool IsWeaponMainFuncPressedThisFrame { get; private set; }
    public static bool IsWeaponMainFuncPressed { get; private set; }
    public static bool IsWeaponMainFuncCanceled { get; private set; }
    public static bool IsWeaponAltFuncPressedThisFrame { get; private set; }
    public static bool IsWeaponAltFuncPressed { get; private set; }
    public static bool IsWeaponAltFuncCanceled { get; private set; }
    public static bool IsWeaponReloadPressedThisFrame { get; private set; }
    public static bool IsWeaponReloadPressed { get; private set; }
    public static bool IsSwitchToPrimaryWeaponPressedThisFrame { get; private set; }
    public static bool IsSwitchToSecondaryWeaponPressedThisFrame { get; private set; }
    public static bool IsSwitchToAdeptWeaponPressedThisFrame { get; private set; }
    
    #endregion
    
    
    private InputAction _abilityQAction;
    
    public static bool IsAbilityQPressedThisFrame { get; private set; }
    
    
    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _movementAction = PlayerInput.actions["Movement"];
        _sprintAction = PlayerInput.actions["Sprint"];
        
        // _weaponMainFuncAction = PlayerInput.actions["WeaponMainFunc"];
        // _weaponAltFuncAction = PlayerInput.actions["WeaponAltFunc"];
        _weaponReloadAction = PlayerInput.actions["WeaponReload"];
        _switchToPrimaryWeaponAction = PlayerInput.actions["SwitchToPrimaryWeapon"];
        _switchToSecondaryWeaponAction = PlayerInput.actions["SwitchToSecondaryWeapon"];
        _switchToAdeptWeaponAction = PlayerInput.actions["SwitchToAdeptWeapon"];
        
        _abilityQAction = PlayerInput.actions["AbilityQ"];
    }
    
    private void Update()
    {
        MovementInput = _movementAction.ReadValue<Vector2>();
        IsHoldingSprint = _sprintAction.IsPressed();
        
        // IsWeaponMainFuncPressedThisFrame = _weaponMainFuncAction.WasPressedThisFrame();
        // IsWeaponMainFuncPressed = _weaponMainFuncAction.IsPressed();
        // IsWeaponMainFuncCanceled = _weaponMainFuncAction.WasReleasedThisFrame();
        //
        // IsWeaponAltFuncPressedThisFrame = _weaponAltFuncAction.WasPressedThisFrame();
        // IsWeaponAltFuncPressed = _weaponAltFuncAction.IsPressed();
        // IsWeaponAltFuncCanceled = _weaponAltFuncAction.WasReleasedThisFrame();

        IsWeaponMainFuncPressedThisFrame = Mouse.current.leftButton.wasPressedThisFrame;
        IsWeaponMainFuncPressed = Mouse.current.leftButton.IsPressed();
        IsWeaponMainFuncCanceled = Mouse.current.leftButton.wasReleasedThisFrame;
        
        IsWeaponAltFuncPressedThisFrame = Mouse.current.rightButton.wasPressedThisFrame;
        IsWeaponAltFuncPressed = Mouse.current.rightButton.wasPressedThisFrame;
        IsWeaponAltFuncCanceled = Mouse.current.rightButton.wasPressedThisFrame;

        IsWeaponReloadPressedThisFrame = _weaponReloadAction.WasPressedThisFrame();
        IsWeaponReloadPressed = _weaponReloadAction.IsPressed();

        IsSwitchToPrimaryWeaponPressedThisFrame = _switchToPrimaryWeaponAction.WasPressedThisFrame();
        IsSwitchToSecondaryWeaponPressedThisFrame = _switchToSecondaryWeaponAction.WasPressedThisFrame();
        IsSwitchToAdeptWeaponPressedThisFrame = _switchToAdeptWeaponAction.WasPressedThisFrame();
        
        IsAbilityQPressedThisFrame = _abilityQAction.WasPressedThisFrame();
    }
}
