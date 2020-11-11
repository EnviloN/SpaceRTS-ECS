using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[AlwaysUpdateSystem]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public class CameraInputSystem: SystemBase, UserInputs.IGameControlsActions
{
    private UserInputs userInputs;

    private Vector2 cameraMovement;
    private Vector2 cameraZoom;
    private float cameraMovementSpeedUp;

    protected override void OnCreate()
    {
        userInputs = new UserInputs();
        userInputs.GameControls.SetCallbacks(this);
    }
    protected override void OnStartRunning() => userInputs.Enable();

    protected override void OnStopRunning() => userInputs.Disable();

    void UserInputs.IGameControlsActions.OnCameraMovement(InputAction.CallbackContext context) => cameraMovement = context.ReadValue<Vector2>();
    void UserInputs.IGameControlsActions.OnCameraZoom(InputAction.CallbackContext context) => cameraZoom = context.ReadValue<Vector2>();
    void UserInputs.IGameControlsActions.OnCameraMovementSpeedUp(InputAction.CallbackContext context) => cameraMovementSpeedUp = context.ReadValue<float>();

    protected override void OnUpdate()
    {
        Entities.ForEach((ref CameraMoveComponent cameraMoveComponent) =>
        {
            cameraMoveComponent.direction.x = cameraMovement.x;
            cameraMoveComponent.direction.y = cameraMovement.y;

            cameraMoveComponent.useFastSpeed = cameraMovementSpeedUp > 0f;

            cameraMoveComponent.zoom = cameraZoom.y; // Mouse scroll is on y ax
        }).WithoutBurst().Run();
    }
}
