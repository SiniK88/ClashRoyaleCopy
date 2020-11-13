// GENERATED AUTOMATICALLY FROM 'Assets/Jussi/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""952f1b17-f126-450e-8a40-b029ca4ef015"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""e6ace1b8-693c-45a9-b214-b307e9f01128"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Place"",
                    ""type"": ""PassThrough"",
                    ""id"": ""038aabb5-d3c4-49bd-8a3f-8c2fe7ec0a41"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""387ebb37-6a86-4e83-bf6b-61d978566514"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectionMinus"",
                    ""type"": ""Button"",
                    ""id"": ""caefe2b2-2f25-48bf-a488-1b2b4037daee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectionPlus"",
                    ""type"": ""Button"",
                    ""id"": ""2379ce2d-251d-493b-828b-40a42d3b9cb0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a1a6e50b-b4b4-4bf4-9102-dde2978beb96"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox One Control Scheme"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ba78f570-356a-4e0d-90e4-cf90383cd239"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Place"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f8dd58c1-fc0d-4e49-a6e7-bf5fd369ec34"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox One Control Scheme"",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4fe90f0a-33ca-4c49-afc0-708daffb078a"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox One Control Scheme"",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ec1a573d-a7d9-4563-bce6-3eada46d985a"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox One Control Scheme"",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2b9f7cef-581a-4252-9b15-498ae77189c7"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox One Control Scheme"",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""96b97034-5ecf-42cc-896b-4b48974b59c9"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox One Control Scheme"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""254f0da2-5dfb-43b6-b2b2-62a843ef8f5a"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectionMinus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c11960fc-9828-447a-8b1d-3e18c5347f14"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectionPlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Xbox One Control Scheme"",
            ""bindingGroup"": ""Xbox One Control Scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Select = m_Player.FindAction("Select", throwIfNotFound: true);
        m_Player_Place = m_Player.FindAction("Place", throwIfNotFound: true);
        m_Player_Cancel = m_Player.FindAction("Cancel", throwIfNotFound: true);
        m_Player_SelectionMinus = m_Player.FindAction("SelectionMinus", throwIfNotFound: true);
        m_Player_SelectionPlus = m_Player.FindAction("SelectionPlus", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Select;
    private readonly InputAction m_Player_Place;
    private readonly InputAction m_Player_Cancel;
    private readonly InputAction m_Player_SelectionMinus;
    private readonly InputAction m_Player_SelectionPlus;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_Player_Select;
        public InputAction @Place => m_Wrapper.m_Player_Place;
        public InputAction @Cancel => m_Wrapper.m_Player_Cancel;
        public InputAction @SelectionMinus => m_Wrapper.m_Player_SelectionMinus;
        public InputAction @SelectionPlus => m_Wrapper.m_Player_SelectionPlus;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Place.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlace;
                @Place.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlace;
                @Place.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlace;
                @Cancel.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancel;
                @SelectionMinus.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionMinus;
                @SelectionMinus.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionMinus;
                @SelectionMinus.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionMinus;
                @SelectionPlus.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionPlus;
                @SelectionPlus.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionPlus;
                @SelectionPlus.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionPlus;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Place.started += instance.OnPlace;
                @Place.performed += instance.OnPlace;
                @Place.canceled += instance.OnPlace;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @SelectionMinus.started += instance.OnSelectionMinus;
                @SelectionMinus.performed += instance.OnSelectionMinus;
                @SelectionMinus.canceled += instance.OnSelectionMinus;
                @SelectionPlus.started += instance.OnSelectionPlus;
                @SelectionPlus.performed += instance.OnSelectionPlus;
                @SelectionPlus.canceled += instance.OnSelectionPlus;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_XboxOneControlSchemeSchemeIndex = -1;
    public InputControlScheme XboxOneControlSchemeScheme
    {
        get
        {
            if (m_XboxOneControlSchemeSchemeIndex == -1) m_XboxOneControlSchemeSchemeIndex = asset.FindControlSchemeIndex("Xbox One Control Scheme");
            return asset.controlSchemes[m_XboxOneControlSchemeSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnPlace(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnSelectionMinus(InputAction.CallbackContext context);
        void OnSelectionPlus(InputAction.CallbackContext context);
    }
}
