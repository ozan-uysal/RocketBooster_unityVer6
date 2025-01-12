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
    Vector3 RotatorVector3 = new Vector3(0,0,1);
    AudioSource audioSource;
    [SerializeField] AudioClip mainEngine;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() 
    {
     thrust.Enable(); 
     rotation.Enable();    
    }

    void FixedUpdate()
    {
        
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * forcepower * Time.fixedDeltaTime);

            if(!audioSource.isPlaying)
            {
            audioSource.PlayOneShot(mainEngine);
            }
        }  
        else
        {
            audioSource.Stop();
        }
       
    }
    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if(rotationInput < 0)
        {
            ApplyRotation(rotationPower);
        }
         else if(rotationInput > 0)
        {
            ApplyRotation(-rotationPower);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {  
        rb.freezeRotation =true;
        transform.Rotate(RotatorVector3 * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation =false;
    }
}
