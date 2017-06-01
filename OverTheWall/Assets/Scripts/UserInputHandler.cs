using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInputHandler : MonoBehaviour
{
    private Camera camera;

    public List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;

    // Use this for initialization
    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject gameObject = hit.transform.gameObject;

                if (Input.GetMouseButtonDown(0))
                {
                    gameObject.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    gameObject.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                }
                if (Input.GetMouseButton(0))
                {
                    gameObject.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
#endif

        if (Input.touchCount > 0)
        {
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();

            foreach (var touch in Input.touches)
            {
                Ray ray = camera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject gameObject = hit.transform.gameObject;

                    touchList.Add(gameObject);

                    if (touch.phase == TouchPhase.Began)
                    {
                        gameObject.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        gameObject.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        gameObject.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Canceled)
                    {
                        gameObject.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

            foreach (GameObject g in touchesOld)
            {
                if (!touchList.Contains(g))
                {
                    g.SendMessage("OnTouchExit", Vector3.zero, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

    }
}
