using UnityEngine;
using System.Collections;

public class CastleController : MonoBehaviour {

    public Camera mainCamera;
    public GameObject groundSpawner;
    float castleMovementSpeed = 5.0f;

    private enum Movement
    {
        Left, 
        Right,
        Stationary
    }

    Movement movement = Movement.Stationary;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update () {
        CheckMovement();
    }

    void CheckMovement()
    {
        switch (movement)
        {
            case Movement.Left:
                Move(-1);
                break;
            case Movement.Right:
                Move(1);
                break;
            case Movement.Stationary:
                break;
            default:
                break;
        }
    }

    private void Move(int dir)
    {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + (castleMovementSpeed * dir) * Time.deltaTime, mainCamera.transform.position.y);
        transform.position = new Vector3(transform.position.x + (castleMovementSpeed * dir) * Time.deltaTime, transform.position.y);
        groundSpawner.transform.position = new Vector3(groundSpawner.transform.position.x + (castleMovementSpeed * dir) * Time.deltaTime, groundSpawner.transform.position.y);
    }

    void OnLeftButtonTouchDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 5f * Time.deltaTime);
    }

    void OnLeftButtonTouchUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y);
    }

    void OnLeftButtonTouchStay()
    {

    }

    void OnLeftButtonTouchExit()
    {

    }

    public void OnLeftClickDown()
    {
        movement = Movement.Left;
    }

    public void OnLeftClickUp()
    {
        movement = Movement.Stationary;
    }

    public void OnRightClickDown()
    {
        movement = Movement.Right;
    }

    public void OnRightClickUp()
    {
        movement = Movement.Stationary;
    }

}
