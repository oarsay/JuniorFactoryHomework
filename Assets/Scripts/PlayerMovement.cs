using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool moveByTouch, gameState;
    private Vector3 mouseStartPos, playerStartPos;
    public float playerSpeed;
    private Camera camera;
    private float leftBoundary = -5f, rightBoundary = 5f;
    private float rotationSpeed = 5f;
    [SerializeField] private float playerSpeedOnAttack;

    private PlayerManager playerManager;
    [SerializeField] private Transform enemy;

    void Start()
    {
        camera = Camera.main;
        playerManager = transform.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if(playerManager.isFight && enemy.childCount > 0)
        {
            Rotate();
            Attack();
        }
        else
        {
            Move();
        }
        
    }

    void Rotate()
    {
        var enemyDirection = new Vector3(playerManager.enemy.position.x, transform.position.y, playerManager.enemy.position.z) - transform.position;

        // Rotate parent object, it will rotate all children objects, including the Count Label
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemyDirection, Vector3.up), Time.deltaTime * rotationSpeed);
    }

    void Attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.position, playerSpeedOnAttack * Time.deltaTime);
    }

    void Move()
    {
        if(Input.GetMouseButtonDown(0) && gameState)
        {
            moveByTouch = true;
            var plane = new Plane(Vector3.up, 0f);
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if(plane.Raycast(ray, out var distance))
            {
                mouseStartPos = ray.GetPoint(distance + 1f);
                playerStartPos = transform.position;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            moveByTouch = false;
        }

        if(moveByTouch)
        {
            var plane = new Plane(Vector3.up, 0f);
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if(plane.Raycast(ray, out var distance))
            {
                var mousePos = ray.GetPoint(distance + 1f);
                var move = mousePos - mouseStartPos;
                var control = playerStartPos + move;
                control.x = Mathf.Clamp(control.x, leftBoundary, rightBoundary);

                // instant movement
                transform.position = new Vector3(control.x, transform.position.y, transform.position.z);
            }
        }

        if(gameState)
        {
            transform.Translate(transform.forward * Time.deltaTime * playerSpeed);
        }
    }
}
