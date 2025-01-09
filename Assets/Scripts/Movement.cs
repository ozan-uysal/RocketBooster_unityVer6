using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction input;

    private void OnEnable() 
    {
     input.Enable();    
    }

    private void Update() 
    {
       if (input.IsPressed())
       {
            Debug.Log(input.bindings);
      
       }
    
    }
}
