using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform camera;

    void Start()
    {
        camera = Camera.main.transform;
        transform.LookAt(camera);
        transform.rotation = Quaternion.LookRotation(transform.position - camera.position);
    }
}
