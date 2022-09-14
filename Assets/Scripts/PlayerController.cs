using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;

    PlayerInput input;
    Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        moveDirection.x = input.moveVector.x;
        moveDirection.z = input.moveVector.y;
        transform.position += moveSpeed * Time.deltaTime * moveDirection;
    }
}
