using UISystem;
using UnityEngine;

public class UIManagerScreenSpace : UIManager
{
    public static UIManagerScreenSpace Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindWithTag("UIManagerScreenSpace").GetComponent<UIManagerScreenSpace>();
            }

            return _instance;
        }
    }

    private static UIManagerScreenSpace _instance;

    public GameObject ChargeModuleBarGameObj { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);

        // Basic HUD
        // OpenPanelFirstTime(EPanelType.PanelPlayerHealthBar);
        // OpenPanelFirstTime(EPanelType.PanelPlayerLoadout);
        // OpenPanelFirstTime(EPanelType.PanelPlayerInventory);
        // OpenPanelFirstTime(EPanelType.PanelRecentAcquired);
        // OpenPanelFirstTime(EPanelType.PanelPlayerBuff);

        // ClosePanel(EPanelType.PanelPlayerInventory, _inventoryPanelGameObj.name);

        // Weapon system related UI
        // ChargeModuleBarGameObj = OpenPanelFirstTime(EPanelType.PanelChargeModuleBar);
        // ChargeModuleBarPanel = ChargeModuleBarGameObj.GetComponent<PanelChargeModuleBar>();
        // ClosePanel(EPanelType.PanelChargeModuleBar, ChargeModuleBarGameObj.name);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Tab))
    //     {
    //         if (_inventoryPanelGameObj.activeSelf)
    //         {
    //             ClosePanel(EPanelType.PanelPlayerInventory, _inventoryPanelGameObj.name);
    //         }
    //         else
    //         {
    //             OpenPanel(EPanelType.PanelPlayerInventory, _inventoryPanelGameObj.name);
    //         }
    //     }
    // }

    // private void InitSingleton()
    // {
    //     if (Instance != null && Instance != this)
    //     {
    //         Debug.LogError("There are multiple instance of UIManagerScreenSpace. " +
    //                        $"\nGameObject of additional instance: {gameObject.name}.");
    //         Destroy(gameObject);
    //         return;
    //     }
    //
    //     Instance = this;
    //     DontDestroyOnLoad(this);
    // }
}