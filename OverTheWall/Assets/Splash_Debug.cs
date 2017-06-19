using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash_Debug : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(SpashTime());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator SpashTime()
    {
        yield return new WaitForSeconds(3);
        
        SceneManager.LoadScene("MenuScene");
    }
}
