using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam05 : MonoBehaviour
{
    public float speed;
    public float xRange = 10;
    public GameObject projectilePrefab;


    // Exam 05 ...
    public int maxBulletCount = 10;
    public int currentBullet;

    public float bulletRegenerateCooldown = 1f;
    private bool onCooldown = false;
    private float cooldownEnd;

    // ...

    private float horizontalInput;
    private InputAction moveAction;
    private InputAction shootAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    private void Start()
    {
        currentBullet = maxBulletCount;
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

        if (shootAction.triggered)
        {
            if (currentBullet <= 0)
            {
                if (!onCooldown) 
                {
                    Debug.Log("bullet empty | restore in " + bulletRegenerateCooldown + " sec");
                    onCooldown = true;
                    cooldownEnd = t + bulletRegenerateCooldown;
                }
                else if (onCooldown && t >= cooldownEnd) 
                {
                    Debug.Log("restore bullet");
                    currentBullet = maxBulletCount;
                    onCooldown = false;
                }
            }
            else 
            {
                Instantiate(projectilePrefab, transform.position, transform.rotation);
                currentBullet--;
            }
        }
    }
}
