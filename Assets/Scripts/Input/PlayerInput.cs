using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Input Values")]
    public Vector2 moveVector;
    public Vector2 lookVector;
    public bool isFire;

    public Vector2 mouseLocation
    {
        get { return Mouse.current.position.ReadValue(); }
    }

    InputMap inputMap;

    // Start is called before the first frame update
    void Awake()
    {
        inputMap = new InputMap();
        
        inputMap.Player.Move.performed += OnMove;
        inputMap.Player.Move.canceled += _ => OnMoveCanceled();
        inputMap.Player.Look.performed += OnLook;
        inputMap.Player.Look.canceled += _ => OnLookCanceled();
        inputMap.Player.Fire.performed += OnFire;
        inputMap.Player.Fire.canceled += _ => OnFireCanceled();
    }

    public bool ConsumeFire()
    {
        if(isFire)
        {
            isFire = false;
            return true;
        }
        return false;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        isFire = true;
    }

    private void OnFireCanceled()
    {
        isFire = false;
    }

    void OnEnable()
    {
        inputMap.Enable();    
    }

    void OnDisable()
    {
        inputMap.Disable();    
    }

    void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
    void OnMoveCanceled()
    {
        moveVector = Vector2.zero;
    }

    void OnLook(InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
    }
    void OnLookCanceled()
    {
        lookVector = Vector2.zero;
    }
}
