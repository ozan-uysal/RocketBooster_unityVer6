using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    Rigidbody rb;
    [SerializeField] int forcepower = 5;
    [SerializeField] int rotationPower = 5;
    private Vector3 RotatorVector3 = new Vector3(0,0,1);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() 
    {
     thrust.Enable(); 
     rotation.Enable();    
    }

    private void FixedUpdate()
    {
        
        ProcessThrust();
        ProcessRotation();

    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * forcepower * Time.fixedDeltaTime);
        }
    }
    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        Debug.Log(rotationInput);

        if(rotationInput < 0)
        {
           ApplyRotation(rotationPower);
        }
         else if(rotationInput > 0)
        {
            ApplyRotation(-rotationPower);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(RotatorVector3 * rotationThisFrame * Time.fixedDeltaTime);
    }
}
