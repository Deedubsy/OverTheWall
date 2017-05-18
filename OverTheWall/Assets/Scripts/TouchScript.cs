using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private void OnGUI()
    {
        foreach (var touch in Input.touches)
        {
            string message = "";

            message += "ID: " + touch.fingerId + "\n";
            message += "Phase: " + touch.phase.ToString() + "\n";
            message += "Pos X: " + touch.position.x + "\n";
            message += "Pos Y: " + touch.position.y + "\n";

            Debug.Log(message);
        }
    }
}
