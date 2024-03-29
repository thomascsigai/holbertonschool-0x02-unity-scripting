using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 10.0f;
    private PlayerControls controls;
    private Vector2 move;
    private Rigidbody rb;

    private int score = 0;

    public int health = 5;

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

        if(other.gameObject.CompareTag("Trap"))
        {
            health--;
            Debug.Log($"Health: {health}");
        }

        if(other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }

        if(other.gameObject.CompareTag("Teleporter"))
        {
            transform.position = other.gameObject.GetComponent<TeleporterBehavior>().otherTeleporter.transform.position;
        }
    }

    private void Update()
    {
        if(health == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
