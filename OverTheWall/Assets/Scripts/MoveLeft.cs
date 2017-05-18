using UnityEngine;
using System.Collections;

public class MoveLeft : MonoBehaviour {

    public RectTransform LeftButton;
    public RectTransform RightButton;
    public GameObject Castle;
    public Camera mainCamera;
    public GameObject groundSpawner;

    float castleMovementSpeed = 5.0f;
    Rect leftButtonRect;
    Rect rightButtonRect;
    RectTransform LB;
    RectTransform RB;

    // Use this for initialization
    void Start () {
        LeftButton = GetComponent<RectTransform>();
        RightButton = GetComponent<RectTransform>();
        Castle = GetComponent<GameObject>();

        //RectTransform LB = (RectTransform)LeftButton.transform;
        //RectTransform RB = (RectTransform)RightButton.transform;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                if (LB.rect.Contains(touch.position))
                {
                    MoveCastle(-1);
                }
                else if (RB.rect.Contains(touch.position))
                {
                    MoveCastle(1);
                }
            }
        }

    }

    void MoveCastle(int dir)
    {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + (castleMovementSpeed * dir) * Time.deltaTime, mainCamera.transform.position.y);
        transform.position = new Vector3(transform.position.x + (castleMovementSpeed * dir) * Time.deltaTime, transform.position.y);
        groundSpawner.transform.position = new Vector3(groundSpawner.transform.position.x + (castleMovementSpeed * dir) * Time.deltaTime, groundSpawner.transform.position.y);
    }
}
