using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingCamera : MonoBehaviour
{
    private GameObject _camera;
    void Awake()
    {
        _camera = Camera.main.gameObject;
    }
    void LateUpdate()
    {
        transform.position = PositionCameara();
    }
    private Vector3 PositionCameara() => new Vector3(_camera.transform.position.x, transform.position.y, _camera.transform.position.z);
}
