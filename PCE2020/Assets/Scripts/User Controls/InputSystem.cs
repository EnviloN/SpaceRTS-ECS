﻿using Assets.Scripts.Camera_Control;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

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
        private float _cameraMovementSpeedUp;

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
            }).WithoutBurst().Run(); // Run on main thread without burst compilation
        }
    }
}
