using UnityEngine;
using System.Collections;
using TouchControlsKit;
using OverTheWall.Enums;
using OverTheWall.Projectile;
using OverTheWall.SharedFunctions;

public class CastleController : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject groundSpawner;
    public GameObject arrow;
    private bool moveLeftBtnState;
    private bool moveRightBtnState;
    public RectTransform healthBar;

    private float totalHealth = 1000;
    private float currentHealth = 1000;
    private float rightSideCastle = 0;
    private float castleMovementSpeed = 5.0f;

    private Vector2 rightSideCastleVector2;
    public RectTransform castleBounds;
    bool canAttack = true;
    float AttackCooldown = 0.5f;

    Movement movement = Movement.Stationary;

    private Vector2 startDragPosition = Vector2.zero;
    private Vector2 endDragPosition = Vector2.zero;
    private float currentTimeBetweenDrag = 0;
    private float totalTimeBetweenDrag = 0;
    private float angle = 0;
    private bool dragging = false;
    private float distanceBetweenDrags;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        castleBounds = GetComponent<RectTransform>();

        rightSideCastle = transform.position.x + (castleBounds.sizeDelta.x / 2);

        rightSideCastleVector2 = new Vector2(rightSideCastle , transform.position.y);
    }

    void Update()
    {
        
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


    //======================================NEW===============================

    public void OnHit(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            currentHealth = 0;

        //UpdateHealthBar();
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / totalHealth;
        float newSizeDelta = healthPercentage * healthBar.sizeDelta.x;

        healthBar.sizeDelta = new Vector2(newSizeDelta, healthBar.sizeDelta.y);
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(AttackCooldown);

        canAttack = true;
    }
}

//float horizontal = TCKInput.GetAxis("MovementDPad", EAxisType.Horizontal);

//if (horizontal > 0)
//{
//    Move(1);
//}
//else if (horizontal < 0)
//{
//    Move(-1);
//}

//if(TCKInput.GetAction("Move_Right", EActionEvent.Down))
//{
//    Move(1);
//}
//else if (TCKInput.GetAction("Move_Left", EActionEvent.Down))
//{
//    Move(-1);
//}


//void OnLeftButtonTouchDown()
//{
//    transform.position = new Vector3(transform.position.x, transform.position.y + 5f * Time.deltaTime);
//}

//void OnLeftButtonTouchUp()
//{
//    transform.position = new Vector3(transform.position.x, transform.position.y);
//}

//void OnLeftButtonTouchStay()
//{

//}

//void OnLeftButtonTouchExit()
//{

//}

//public void OnLeftClickDown()
//{
//    movement = Movement.Left;
//}

//public void OnLeftClickUp()
//{
//    movement = Movement.Stationary;
//}

//public void OnRightClickDown()
//{
//    movement = Movement.Right;
//}

//public void OnRightClickUp()
//{
//    movement = Movement.Stationary;
//}