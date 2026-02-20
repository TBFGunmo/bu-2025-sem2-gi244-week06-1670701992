using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam03 : MonoBehaviour
{
    public float speed;
    public float xRange = 10;
    public GameObject projectilePrefab;

    public bool enableAutoFireMode;
    public float autoFireInterval = 0.1f;

    public float nextFireTime = 0;

    private float horizontalInput;
    private InputAction moveAction;
    private InputAction shootAction;

    private InputAction switchAction;


    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");

        switchAction = InputSystem.actions.FindAction("SwitchCommand");
    }

    // Update is called once per frame
    void Update()
    {
        var t = Time.time;

        horizontalInput = moveAction.ReadValue<Vector2>().x;
        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (enableAutoFireMode && t >= nextFireTime)
        {
            nextFireTime = t + autoFireInterval;
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
        else if (shootAction.triggered)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }


        if (switchAction.triggered)
        {
            enableAutoFireMode = !enableAutoFireMode;
        }

        
    }
}
