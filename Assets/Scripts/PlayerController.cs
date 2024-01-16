using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 10.0f;
    private PlayerControls controls;
    private Vector2 move;
    private Rigidbody rb;

    private int score = 0;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();

        if(rb == null)
        {
            Debug.LogError("Rigidbody is NULL");
        }
    }

    private void FixedUpdate()
    {
        move = controls.movementActionMap.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(move.x, 0.0f, move.y);
        rb.velocity = movement * speed;
    }

    private void OnEnable()
    {
        controls.movementActionMap.Enable();
    }

    private void OnDisable()
    {
        controls.movementActionMap.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            score++;
            Debug.Log($"Score: {score}");
        }
    }
}
