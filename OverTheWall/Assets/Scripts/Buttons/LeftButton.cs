using UnityEngine;
using System.Collections;

public class LeftButton : MonoBehaviour {

    public GameObject castle;

	// Use this for initialization
	void Start () {
        castle = GetComponent<GameObject>();
	}

    void OnTouchDown()
    {
        castle.SendMessage("OnLeftButtonTouchDown");
    }

    void OnTouchUp()
    {
        castle.SendMessage("OnLeftButtonTouchUp");
    }

    void OnTouchStay()
    {
        castle.SendMessage("OnLeftButtonTouchStay");
    }

    void OnTouchExit()
    {
        castle.SendMessage("OnLeftButtonTouchExit");
    }
}
