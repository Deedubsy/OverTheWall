using UnityEngine;
using System.Collections;

public class CastleController : MonoBehaviour {

    private enum Movement
    {
        Left, 
        Right,
        Stationary
    }

    Movement movement = Movement.Stationary;

    // Update is called once per frame
    void Update () {

        switch (movement)
        {
            case Movement.Left:
                transform.position = new Vector3(transform.position.x - 5f * Time.deltaTime, transform.position.y);
                break;
            case Movement.Right:
                transform.position = new Vector3(transform.position.x + 5f * Time.deltaTime, transform.position.y);
                break;
            case Movement.Stationary:
                break;
            default:
                break;
        }

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
