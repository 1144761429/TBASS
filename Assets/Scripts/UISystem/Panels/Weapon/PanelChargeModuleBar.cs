using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using UnityEngine.UI;
using WeaponSystem;

public class PanelChargeModuleBar : PanelBase
{
    [SerializeField] private Image _fill;

    private Loadout _playerLoadout;
    private ChargeModule _chargeModule;

    private void Update()
    {
    }

    public override void Init()
    {
        base.Init();
        
        _playerLoadout = GameObject.FindWithTag("PlayerLoadout").GetComponent<Loadout>();
        if (_playerLoadout == null)
        {
            Debug.LogError(
                "Cannot find player loadout. Please check if Player Loadout GameObject is correctly tagged.");
            return;
        }

        _playerLoadout.OnSwitchWeapon += (_) =>
        {
            if (_playerLoadout.CurrentWeapon.StaticData.HasChargeModule)
            {
                _chargeModule = _playerLoadout.CurrentWeapon.gameObject.GetComponent<ChargeModule>();
            }
        };

        // _playerLoadout.OnChangeWeapon += () =>
        // {
        //     if (_playerLoadout.CurrentWeapon.StaticData.HasChargeModule)
        //     {
        //         _chargeModule = _playerLoadout.CurrentWeapon.gameObject.GetComponent<ChargeModule>();
        //     }
        // };
    }

    public void UpdateVisual()
    {
        if (_chargeModule == null)
        {
            throw new Exception(
                "Charge Module is null but still trying to call UpdateVisual() of PanelChargeModuleBar.");
        }

        if (_chargeModule.ChargeThreshold == 0)
        {
            throw new Exception("Charge Threshold of a charge module cannot be 0");
        }

        _fill.fillAmount = _chargeModule.ChargeProgress / _chargeModule.ChargeThreshold;
    }

    public void SetChargeModule(ChargeModule chargeModule)
    {
        _chargeModule = chargeModule;
    }
}