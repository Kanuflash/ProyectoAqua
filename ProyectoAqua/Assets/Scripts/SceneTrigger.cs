using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Invoke("goPlay", 33);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)){
            goPlay();
        }
	}
	
	public void goPlay(){
		SceneManager.LoadScene(2, LoadSceneMode.Single);
	}
}
