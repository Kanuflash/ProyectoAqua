using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {


	public GameObject deathMenu;
	public GameObject pauseMenu;
	public static MenuController instance;

	 // Use this for initialization
    void Start () {
		if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
	}
	
	public void onClickRetry(){
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	public void onClickTitleScreen(){
		
		SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
	}

	public void showDeathMenu(){
		Time.timeScale = 0.0000001f;
		deathMenu.SetActive(true);
	}

	public void showPauseMenu(){
		Time.timeScale = 0.0000001f;
		pauseMenu.SetActive(true);
	}

	public void onClickResume(GameObject pauseMn){
		pauseMn.SetActive(false);
		
	}

	public void onClickExit(){
		Application.Quit();
	}
}
