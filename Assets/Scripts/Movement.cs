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
    [SerializeField] ParticleSystem leftThrustFX;
    [SerializeField] ParticleSystem rightThrustFX;
    [SerializeField] ParticleSystem mainThrustFX;

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    private void StopThrusting()
    {
        mainThrustFX.Stop();
        audioSource.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * forcepower * Time.fixedDeltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            mainThrustFX.Play();
        }
    }

    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        RotatingRight(rotationInput);
        RotatingLeft(rotationInput);
    }

    private void RotatingLeft(float rotationInput)
    {
        if (rotationInput > 0)
        {
            ApplyRotation(-rotationPower);
            leftThrustFX.Play();
        }
        else
        {
            leftThrustFX.Stop();
        }
    }

    private void RotatingRight(float rotationInput)
    {
        if (rotationInput < 0)
        {
            ApplyRotation(rotationPower);
            rightThrustFX.Play();
        }
        else
        {
            rightThrustFX.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {  
        rb.freezeRotation =true;
        transform.Rotate(RotatorVector3 * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation =false;
    }
}
