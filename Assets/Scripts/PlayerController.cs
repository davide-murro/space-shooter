using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // move speed
    [SerializeField] float moveSpeed = 5f;

    // padding limit for the player
    [SerializeField] float paddingTop = 2f;
    [SerializeField] float paddingRight = 0.5f;
    [SerializeField] float paddingBottom = 0.5f;
    [SerializeField] float paddingLeft = 0.5f;

    // move input
    Vector2 moveInput;

    // bounds of the player for camera
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    // The Awake method is called when the script instance is being loaded
    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Update is called every fixed framerate frame
    void FixedUpdate()
    {
    }

    // on move event
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); // check if input value changes (button pressed 0-1)
        //Debug.Log(moveInput);
    }

    // on attack event
    void OnAttack(InputValue value)
    {
        if (shooter != null) // check if shooter is existing in the scene
        {
            shooter.isFiring = value.isPressed;
        }
    }

    // move the player
    void Move()
    {
        Vector3 delta = moveInput * moveSpeed * Time.deltaTime; // Calculates how much the object should move this frame based on input, speed, and time.

        Vector3 newPos = new Vector2(); // Create a new 2D vector to store the object's new position.
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight); // Set the new x-position, ensuring it stays within defined horizontal bounds.
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop); // Set the new y-position, ensuring it stays within defined vertical bounds.

        transform.position = newPos; // Update the object's position to the newly calculated position.
    }

    // init camera bounds, so even if we change view and camera it going to take the maximum value of screen
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0)); // this is the minimum value for camera
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1)); // this is the maximum value for camera
    }
}
