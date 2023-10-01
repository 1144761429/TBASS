//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Weapons/WeaponInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @WeaponInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @WeaponInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""WeaponInput"",
    ""maps"": [
        {
            ""name"": ""Normal"",
            ""id"": ""961a04c6-8e6e-43d9-affb-0026d570c20a"",
            ""actions"": [
                {
                    ""name"": ""Start Main Function"",
                    ""type"": ""Button"",
                    ""id"": ""9bcf7a29-56de-401a-a333-3602fbe24e3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel Main Function"",
                    ""type"": ""Button"",
                    ""id"": ""f9b4a6a0-0722-454e-93c5-fb3c9fe0971b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Start Alternative Function"",
                    ""type"": ""Button"",
                    ""id"": ""2562519a-49ac-45ae-a85e-db018f98187b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel Alternative Function"",
                    ""type"": ""Button"",
                    ""id"": ""3eb373fa-a934-4b55-aa04-f08172a41197"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""c8d73fc3-82d3-4a9d-a0aa-c5e451ec83ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Switch To Primary"",
                    ""type"": ""Button"",
                    ""id"": ""a7449cf7-dc86-4747-a01a-ff609197f696"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Switch To Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""bca89265-453d-4968-9be7-e560339475d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Switch To Adept"",
                    ""type"": ""Button"",
                    ""id"": ""fa82298a-efb6-4607-9fc1-ab8a8afec5e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2e4fcb43-5f42-41e4-8070-0b11ea975aec"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start Main Function"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e985fab6-94b2-4ec7-a980-bd8d4067015b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start Alternative Function"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b99458cb-23ba-460c-b33c-e57e7f31a283"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54fca12f-99bf-40dd-bf42-15e42c97d3cc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel Main Function"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90761f66-e360-4f54-8d42-08bfc335c06b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel Alternative Function"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34459f02-4436-42d9-9b4e-066d7061ae02"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch To Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""746c26f4-c088-4629-8f6a-0624c21c8e36"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch To Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50719f9f-a3d6-4f2d-b5ac-81844379fe00"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch To Adept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Normal
        m_Normal = asset.FindActionMap("Normal", throwIfNotFound: true);
        m_Normal_StartMainFunction = m_Normal.FindAction("Start Main Function", throwIfNotFound: true);
        m_Normal_CancelMainFunction = m_Normal.FindAction("Cancel Main Function", throwIfNotFound: true);
        m_Normal_StartAlternativeFunction = m_Normal.FindAction("Start Alternative Function", throwIfNotFound: true);
        m_Normal_CancelAlternativeFunction = m_Normal.FindAction("Cancel Alternative Function", throwIfNotFound: true);
        m_Normal_Reload = m_Normal.FindAction("Reload", throwIfNotFound: true);
        m_Normal_SwitchToPrimary = m_Normal.FindAction("Switch To Primary", throwIfNotFound: true);
        m_Normal_SwitchToSecondary = m_Normal.FindAction("Switch To Secondary", throwIfNotFound: true);
        m_Normal_SwitchToAdept = m_Normal.FindAction("Switch To Adept", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Normal
    private readonly InputActionMap m_Normal;
    private INormalActions m_NormalActionsCallbackInterface;
    private readonly InputAction m_Normal_StartMainFunction;
    private readonly InputAction m_Normal_CancelMainFunction;
    private readonly InputAction m_Normal_StartAlternativeFunction;
    private readonly InputAction m_Normal_CancelAlternativeFunction;
    private readonly InputAction m_Normal_Reload;
    private readonly InputAction m_Normal_SwitchToPrimary;
    private readonly InputAction m_Normal_SwitchToSecondary;
    private readonly InputAction m_Normal_SwitchToAdept;
    public struct NormalActions
    {
        private @WeaponInput m_Wrapper;
        public NormalActions(@WeaponInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @StartMainFunction => m_Wrapper.m_Normal_StartMainFunction;
        public InputAction @CancelMainFunction => m_Wrapper.m_Normal_CancelMainFunction;
        public InputAction @StartAlternativeFunction => m_Wrapper.m_Normal_StartAlternativeFunction;
        public InputAction @CancelAlternativeFunction => m_Wrapper.m_Normal_CancelAlternativeFunction;
        public InputAction @Reload => m_Wrapper.m_Normal_Reload;
        public InputAction @SwitchToPrimary => m_Wrapper.m_Normal_SwitchToPrimary;
        public InputAction @SwitchToSecondary => m_Wrapper.m_Normal_SwitchToSecondary;
        public InputAction @SwitchToAdept => m_Wrapper.m_Normal_SwitchToAdept;
        public InputActionMap Get() { return m_Wrapper.m_Normal; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NormalActions set) { return set.Get(); }
        public void SetCallbacks(INormalActions instance)
        {
            if (m_Wrapper.m_NormalActionsCallbackInterface != null)
            {
                @StartMainFunction.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnStartMainFunction;
                @StartMainFunction.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnStartMainFunction;
                @StartMainFunction.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnStartMainFunction;
                @CancelMainFunction.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnCancelMainFunction;
                @CancelMainFunction.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnCancelMainFunction;
                @CancelMainFunction.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnCancelMainFunction;
                @StartAlternativeFunction.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnStartAlternativeFunction;
                @StartAlternativeFunction.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnStartAlternativeFunction;
                @StartAlternativeFunction.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnStartAlternativeFunction;
                @CancelAlternativeFunction.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnCancelAlternativeFunction;
                @CancelAlternativeFunction.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnCancelAlternativeFunction;
                @CancelAlternativeFunction.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnCancelAlternativeFunction;
                @Reload.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnReload;
                @SwitchToPrimary.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnSwitchToPrimary;
                @SwitchToPrimary.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnSwitchToPrimary;
                @SwitchToPrimary.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnSwitchToPrimary;
                @SwitchToSecondary.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnSwitchToSecondary;
                @SwitchToSecondary.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnSwitchToSecondary;
                @SwitchToSecondary.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnSwitchToSecondary;
                @SwitchToAdept.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnSwitchToAdept;
                @SwitchToAdept.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnSwitchToAdept;
                @SwitchToAdept.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnSwitchToAdept;
            }
            m_Wrapper.m_NormalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @StartMainFunction.started += instance.OnStartMainFunction;
                @StartMainFunction.performed += instance.OnStartMainFunction;
                @StartMainFunction.canceled += instance.OnStartMainFunction;
                @CancelMainFunction.started += instance.OnCancelMainFunction;
                @CancelMainFunction.performed += instance.OnCancelMainFunction;
                @CancelMainFunction.canceled += instance.OnCancelMainFunction;
                @StartAlternativeFunction.started += instance.OnStartAlternativeFunction;
                @StartAlternativeFunction.performed += instance.OnStartAlternativeFunction;
                @StartAlternativeFunction.canceled += instance.OnStartAlternativeFunction;
                @CancelAlternativeFunction.started += instance.OnCancelAlternativeFunction;
                @CancelAlternativeFunction.performed += instance.OnCancelAlternativeFunction;
                @CancelAlternativeFunction.canceled += instance.OnCancelAlternativeFunction;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @SwitchToPrimary.started += instance.OnSwitchToPrimary;
                @SwitchToPrimary.performed += instance.OnSwitchToPrimary;
                @SwitchToPrimary.canceled += instance.OnSwitchToPrimary;
                @SwitchToSecondary.started += instance.OnSwitchToSecondary;
                @SwitchToSecondary.performed += instance.OnSwitchToSecondary;
                @SwitchToSecondary.canceled += instance.OnSwitchToSecondary;
                @SwitchToAdept.started += instance.OnSwitchToAdept;
                @SwitchToAdept.performed += instance.OnSwitchToAdept;
                @SwitchToAdept.canceled += instance.OnSwitchToAdept;
            }
        }
    }
    public NormalActions @Normal => new NormalActions(this);
    public interface INormalActions
    {
        void OnStartMainFunction(InputAction.CallbackContext context);
        void OnCancelMainFunction(InputAction.CallbackContext context);
        void OnStartAlternativeFunction(InputAction.CallbackContext context);
        void OnCancelAlternativeFunction(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnSwitchToPrimary(InputAction.CallbackContext context);
        void OnSwitchToSecondary(InputAction.CallbackContext context);
        void OnSwitchToAdept(InputAction.CallbackContext context);
    }
}
