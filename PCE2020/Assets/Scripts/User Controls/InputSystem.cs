using Assets.Scripts.Camera_Control;
using Assets.Scripts.Entity_Selection;
using Assets.Scripts.Entity_Selection.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.User_Controls {
    /// <summary>
    /// System handles input system callbacks and updates data in corresponding Components.
    /// </summary>
    [AlwaysUpdateSystem]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public class InputSystem : SystemBase, UserInputs.IGameControlsActions {
        private UserInputs _userInputs;

        private Vector2 _cameraMovement;
        private Vector2 _cameraZoom;
        private Vector2 _mouseDelta;
        private Vector2 _mousePosition;
        private float _cameraMovementSpeedUp;
        private float _spaceGrab;
        private bool _selectPerformed;
        private bool _selectCanceled;

        protected override void OnCreate() {
            _userInputs = new UserInputs();
            _userInputs.GameControls.SetCallbacks(this);
        }

        protected override void OnStartRunning() => _userInputs.Enable();

        protected override void OnStopRunning() => _userInputs.Disable();

        void UserInputs.IGameControlsActions.OnCameraMovement(InputAction.CallbackContext context) =>
            _cameraMovement = context.ReadValue<Vector2>();

        void UserInputs.IGameControlsActions.OnCameraZoom(InputAction.CallbackContext context) =>
            _cameraZoom = context.ReadValue<Vector2>();

        void UserInputs.IGameControlsActions.OnCameraMovementSpeedUp(InputAction.CallbackContext context) =>
            _cameraMovementSpeedUp = context.ReadValue<float>();

        void UserInputs.IGameControlsActions.OnSpaceGrab(InputAction.CallbackContext context) =>
            _spaceGrab = context.ReadValue<float>();

        void UserInputs.IGameControlsActions.OnMouseDelta(InputAction.CallbackContext context) =>
            _mouseDelta = context.ReadValue<Vector2>();

        void UserInputs.IGameControlsActions.OnMousePosition(InputAction.CallbackContext context) =>
            _mousePosition = context.ReadValue<Vector2>();

        void UserInputs.IGameControlsActions.OnSelect(InputAction.CallbackContext context) {
            _selectPerformed = context.performed;
            _selectCanceled = context.canceled;
        }

        /// <summary>
        /// Updates corresponding components with current input data.
        /// </summary>
        protected override void OnUpdate() {
            // Camera Movement Component
            Entities.ForEach((ref CameraMoveComponent cameraMoveComponent) => {
                cameraMoveComponent.Direction.x = _cameraMovement.x;
                cameraMoveComponent.Direction.y = _cameraMovement.y;

                cameraMoveComponent.UseFastSpeed = _cameraMovementSpeedUp > 0f; // Convert to bool

                cameraMoveComponent.Zoom = _cameraZoom.y; // Mouse scroll is only on y ax

                ProcessSpaceGrab(ref cameraMoveComponent);
            }).WithoutBurst().Run(); // Run on main thread without burst compilation

            Entities.ForEach((ref SelectionControlsComponent selectionComponent) => {
                selectionComponent.CursorPosition = GetCursorPositionOnXYPlane();
                selectionComponent.SelectPerformed = _selectPerformed;
                selectionComponent.SelectCanceled = _selectCanceled;
            }).WithoutBurst().Run(); // Run on main thread without burst compilation

            _selectPerformed = _selectCanceled = false;
        }

        /// <summary>
        /// Method checks if the player pushed the space grab button and adds mouse delta
        /// values to the component's direction property.
        /// </summary>
        /// <param name="cameraMoveComponent">Reference to the current camera move component</param>
        private void ProcessSpaceGrab(ref CameraMoveComponent cameraMoveComponent) {
            if (!(_spaceGrab > 0))
                return;

            // Add inverted values, because we want to grab and pull the view
            cameraMoveComponent.Direction.x -= _mouseDelta.x;
            cameraMoveComponent.Direction.y -= _mouseDelta.y;
        }

        /// <summary>
        /// Method computes the position of the cursor in world based on mouse and camera position.
        /// </summary>
        /// <returns>World position on XY plane.</returns>
        private float3 GetCursorPositionOnXYPlane() {
            Debug.Assert(Camera.main != null, "Camera.main != null");
            var mouse = new float3(_mousePosition.x, _mousePosition.y, -Camera.main.transform.position.z);
            var worldSpacePoint = Camera.main.ScreenToWorldPoint(mouse);

            return new float3(worldSpacePoint.x, worldSpacePoint.y, 0);
        }
    }
}
