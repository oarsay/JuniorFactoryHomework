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

    void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        Move();
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
                //transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * playerSpeed), transform.position.y, transform.position.z);
                
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
