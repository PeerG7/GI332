using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;

    private InputAction moveAction;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        float h = moveAction.ReadValue<Vector2>().x;
        transform.Rotate(Vector3.up, h * rotationSpeed * Time.deltaTime);
    }
}
