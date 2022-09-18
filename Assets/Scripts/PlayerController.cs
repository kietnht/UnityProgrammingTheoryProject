using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform controllable;
    [SerializeField] Transform controllableParent;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] Camera cam;

    ColorAbsorber colorAbsorber;
    PlayerInput input;
    Vector3 moveDirection = Vector3.zero;

    // ENCAPSULATION
    public ColorAbsorber ColorAbsorber { get { return colorAbsorber; } }

    void Start()
    {
        input = GetComponent<PlayerInput>();
        ApplyNewControllable();
    }

    // ABSTRACTION
    private void ApplyNewControllable()
    {
        if (controllable != null)
        {
            controllableParent = controllable.parent;
            colorAbsorber = controllable.GetComponent<ColorAbsorber>();
            controllable.parent = transform;

            transform.position = new Vector3(controllable.position.x, transform.position.y, controllable.position.z);
            controllable.localPosition = new Vector3(0, controllable.position.y, 0);

        }
    }

    void Update()
    {
        Move();
        if (input.ConsumeFire())
        {
            var ray = cam.ScreenPointToRay(input.mouseLocation);
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 20f))
            {
                var hitObject = hit.transform;
                if(hitObject.CompareTag("Controllable"))
                {
                    SwitchControllable(hitObject);
                }
            }
        }
    }

    // ABSTRACTION
    private void SwitchControllable(Transform controlObject)
    {
        //unchild current
        controllable.parent = controllableParent;
        
        controllable = controlObject;
        ApplyNewControllable();
    }

    private void Move()
    {
        moveDirection.x = input.moveVector.x;
        moveDirection.z = input.moveVector.y;
        transform.position += moveSpeed * Time.deltaTime * moveDirection;
    }
}
