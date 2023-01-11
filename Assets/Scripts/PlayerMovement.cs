using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool moveByTouch, gameState;
    private Vector3 mouseStartPos, playerStartPos;
    public float playerSpeed;
    private Camera camera;
    private float leftBoundary = -5f, rightBoundary = 5f;
    private float rotationSpeed = 1f;

    private PlayerManager playerManager;

    void Start()
    {
        camera = Camera.main;
        playerManager = transform.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if(playerManager.fight)
        {
            var enemyDirection = new Vector3(playerManager.enemy.position.x, transform.position.y, playerManager.enemy.position.z) - transform.position;

            // Rotate parent object, it will rotate all children objects, including the Count Label
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemyDirection, Vector3.up), Time.deltaTime * rotationSpeed);

            //for(int i = 1; i < transform.childCount; i++)
            //    transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(enemyDirection, Vector3.up), Time.deltaTime * 3f);
            

        }
        else
        {
            Move();
        }
        
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

                // smoother movement with lerp
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * playerSpeed), transform.position.y, transform.position.z);
                
                // instant movement
                //transform.position = new Vector3(control.x, transform.position.y, transform.position.z);
            }
        }

        if(gameState)
        {
            transform.Translate(transform.forward * Time.deltaTime * playerSpeed);
        }
    }
}
