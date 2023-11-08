public class WeaponInputHandler
{
    public static WeaponInputHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new WeaponInputHandler();
            }
            
            return _instance;
        }
    }
    private static WeaponInputHandler _instance;
    
    private readonly WeaponInput _weaponInputActions;

    public bool MainFunctionKeyPressed => _weaponInputActions.Normal.StartMainFunction.triggered;
    public bool MainFunctionKeyHeld { get; private set; }
    public bool MainFunctionKeyReleased => _weaponInputActions.Normal.CancelMainFunction.triggered;

    public bool AltFunctionKeyPressed => _weaponInputActions.Normal.StartAlternativeFunction.triggered;
    public bool AltFunctionKeyHeld { get; private set; }
    public bool AltFunctionKeyReleased => _weaponInputActions.Normal.CancelAlternativeFunction.triggered;

    public bool ReloadKeyPressed => _weaponInputActions.Normal.Reload.triggered;

    public bool SwitchToPrimaryKeyPressed => _weaponInputActions.Normal.SwitchToPrimary.triggered;
    public bool SwitchToSecondaryKeyPressed => _weaponInputActions.Normal.SwitchToSecondary.triggered;
    public bool SwitchToAdeptKeyPressed => _weaponInputActions.Normal.SwitchToAdept.triggered;

    private WeaponInputHandler()
    {
        _weaponInputActions = new WeaponInput();
        _weaponInputActions.Enable();

        _weaponInputActions.Normal.StartMainFunction.started +=
            (context) => { MainFunctionKeyHeld = true; };
        _weaponInputActions.Normal.CancelMainFunction.performed +=
            (context) => { MainFunctionKeyHeld = false; };

        _weaponInputActions.Normal.StartAlternativeFunction.started +=
            (context) => { AltFunctionKeyHeld = true; };
        _weaponInputActions.Normal.CancelAlternativeFunction.performed +=
            (context) => { AltFunctionKeyHeld = false; };
    }
}