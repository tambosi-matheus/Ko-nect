using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float _rotationY;
    private float _rotationX;
    private Vector2 screenCenter =
        new Vector2(Screen.width / 2, Screen.height / 2);
    public Vector2 deltaPos, deltaPosition;

    [SerializeField] private Vector3 _target = Vector3.zero;
    [SerializeField] private float _distanceFromTarget = 3.0f;

    private Vector3 rotation;
    private Vector3 velocity = Vector3.zero;

    public float mouseX, mouseY;
    public Vector3 pastPos, newPos;

    private void Awake()
    {
        _distanceFromTarget = -transform.position.z;
    }

    void Update()
    {
        Vector3 deltaPos;
        if (Input.touchCount == 0)
        {
            deltaPos = Vector3.up * 7;
        }
        else
        {
            // Get mouse position relative to center of screen
            var maxMap = 200;
            var touch = Input.GetTouch(0);
            deltaPosition = touch.position - screenCenter;
            deltaPos = new Vector3(
                MapValue(deltaPosition.y, 0, Screen.height, 0, maxMap),
                MapValue(deltaPosition.x, 0, Screen.width, 0, maxMap));           
        }
        var targetRotation = rotation + deltaPos;
        rotation = Vector3.SmoothDamp(rotation, targetRotation, ref velocity, 1f);
        transform.localEulerAngles = rotation;
        transform.position = -transform.forward * _distanceFromTarget;

    }
    private float MapValue(float value, float min1, float max1, float min2, float max2)
        => (value - min1) / (max1 - min1) * (max2 - min2) + min2;
}