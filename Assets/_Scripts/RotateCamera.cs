using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;

    private float horitzontalInput;
    // Update is called once per frame
    void Update()
    {
        horitzontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horitzontalInput * rotationSpeed * Time.deltaTime);
    }
}
