// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/User Controls/UserInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @UserInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @UserInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UserInputs"",
    ""maps"": [
        {
            ""name"": ""GameControls"",
            ""id"": ""44275234-867e-47a6-98d7-35bdbe624765"",
            ""actions"": [
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Button"",
                    ""id"": ""d154ccde-cb05-4c04-ac1e-97d03cdd34e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMovementSpeedUp"",
                    ""type"": ""Button"",
                    ""id"": ""03c1e103-d745-4553-8ec0-28378be9f308"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoom"",
                    ""type"": ""Value"",
                    ""id"": ""18b0282c-e3ed-40d3-b326-b16d961a40dd"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpaceGrab"",
                    ""type"": ""Button"",
                    ""id"": ""c92fbd4a-ab82-4e72-a8cf-431d8d4c2438"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Value"",
                    ""id"": ""49afd832-5be8-496b-b568-3a7ea08d3984"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""ScaleVector2(x=2,y=2)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""9ae1e28f-f82a-4a3d-83d4-5fee76f01c5c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""adc32981-3e6d-4c34-ae31-01929c2b93e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASDCameraMovement"",
                    ""id"": ""e05b9881-3a8b-4977-bce3-6e7e6bae3b8f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2f7306b9-6d6d-4871-b064-62e27e2ded06"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a2c8db34-2626-4f52-a218-561985b60af9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4c9931e4-1944-4967-a104-5bc7c3affa00"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a38bcb6d-9c4a-4337-8c6e-f4347244c8f0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowsCameraMovement"",
                    ""id"": ""7a61ae62-c4d1-4edf-830c-4c39291ca249"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4f11caee-9101-426e-b194-739c78d65b12"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3306c4c6-2c8c-4736-a37c-1dd59addfcc4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""adefa38a-be53-44e8-bc0e-73a2197e0b47"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f234214c-1c56-4f3b-b597-6db2c766a244"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6488c55e-0af8-4e82-a7f4-2b8e19b6139d"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMovementSpeedUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""b751a6d5-e247-4e49-b3df-0e57891ab88f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3a3a7e7-61da-4089-96c8-bca9af059368"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""693db9e6-a9dc-440f-9b67-11c9c9eadd28"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SpaceGrab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb69b2b9-1d2b-4c8d-85ca-876221a37e1e"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19cdc89d-bfa3-47f3-b493-daba80aa4af8"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4af3dcf9-80e0-4a90-9e3c-2bbd7713457e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GameControls
        m_GameControls = asset.FindActionMap("GameControls", throwIfNotFound: true);
        m_GameControls_CameraMovement = m_GameControls.FindAction("CameraMovement", throwIfNotFound: true);
        m_GameControls_CameraMovementSpeedUp = m_GameControls.FindAction("CameraMovementSpeedUp", throwIfNotFound: true);
        m_GameControls_CameraZoom = m_GameControls.FindAction("CameraZoom", throwIfNotFound: true);
        m_GameControls_SpaceGrab = m_GameControls.FindAction("SpaceGrab", throwIfNotFound: true);
        m_GameControls_MouseDelta = m_GameControls.FindAction("MouseDelta", throwIfNotFound: true);
        m_GameControls_MousePosition = m_GameControls.FindAction("MousePosition", throwIfNotFound: true);
        m_GameControls_Select = m_GameControls.FindAction("Select", throwIfNotFound: true);
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

    // GameControls
    private readonly InputActionMap m_GameControls;
    private IGameControlsActions m_GameControlsActionsCallbackInterface;
    private readonly InputAction m_GameControls_CameraMovement;
    private readonly InputAction m_GameControls_CameraMovementSpeedUp;
    private readonly InputAction m_GameControls_CameraZoom;
    private readonly InputAction m_GameControls_SpaceGrab;
    private readonly InputAction m_GameControls_MouseDelta;
    private readonly InputAction m_GameControls_MousePosition;
    private readonly InputAction m_GameControls_Select;
    public struct GameControlsActions
    {
        private @UserInputs m_Wrapper;
        public GameControlsActions(@UserInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @CameraMovement => m_Wrapper.m_GameControls_CameraMovement;
        public InputAction @CameraMovementSpeedUp => m_Wrapper.m_GameControls_CameraMovementSpeedUp;
        public InputAction @CameraZoom => m_Wrapper.m_GameControls_CameraZoom;
        public InputAction @SpaceGrab => m_Wrapper.m_GameControls_SpaceGrab;
        public InputAction @MouseDelta => m_Wrapper.m_GameControls_MouseDelta;
        public InputAction @MousePosition => m_Wrapper.m_GameControls_MousePosition;
        public InputAction @Select => m_Wrapper.m_GameControls_Select;
        public InputActionMap Get() { return m_Wrapper.m_GameControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameControlsActions set) { return set.Get(); }
        public void SetCallbacks(IGameControlsActions instance)
        {
            if (m_Wrapper.m_GameControlsActionsCallbackInterface != null)
            {
                @CameraMovement.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCameraMovement;
                @CameraMovementSpeedUp.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCameraMovementSpeedUp;
                @CameraMovementSpeedUp.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCameraMovementSpeedUp;
                @CameraMovementSpeedUp.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCameraMovementSpeedUp;
                @CameraZoom.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCameraZoom;
                @SpaceGrab.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnSpaceGrab;
                @SpaceGrab.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnSpaceGrab;
                @SpaceGrab.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnSpaceGrab;
                @MouseDelta.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMouseDelta;
                @MousePosition.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMousePosition;
                @Select.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_GameControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CameraMovement.started += instance.OnCameraMovement;
                @CameraMovement.performed += instance.OnCameraMovement;
                @CameraMovement.canceled += instance.OnCameraMovement;
                @CameraMovementSpeedUp.started += instance.OnCameraMovementSpeedUp;
                @CameraMovementSpeedUp.performed += instance.OnCameraMovementSpeedUp;
                @CameraMovementSpeedUp.canceled += instance.OnCameraMovementSpeedUp;
                @CameraZoom.started += instance.OnCameraZoom;
                @CameraZoom.performed += instance.OnCameraZoom;
                @CameraZoom.canceled += instance.OnCameraZoom;
                @SpaceGrab.started += instance.OnSpaceGrab;
                @SpaceGrab.performed += instance.OnSpaceGrab;
                @SpaceGrab.canceled += instance.OnSpaceGrab;
                @MouseDelta.started += instance.OnMouseDelta;
                @MouseDelta.performed += instance.OnMouseDelta;
                @MouseDelta.canceled += instance.OnMouseDelta;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public GameControlsActions @GameControls => new GameControlsActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IGameControlsActions
    {
        void OnCameraMovement(InputAction.CallbackContext context);
        void OnCameraMovementSpeedUp(InputAction.CallbackContext context);
        void OnCameraZoom(InputAction.CallbackContext context);
        void OnSpaceGrab(InputAction.CallbackContext context);
        void OnMouseDelta(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
}
