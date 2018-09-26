using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {


	public GameObject deathMenu;
	public GameObject pauseMenu;

	

	public Text collectableHUDText;

	public static MenuController instance;

	 // Use this for initialization
    void Start () {
		if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

		refreshCollectablesHUD(0, GameManager.instance.maxCollectables);
	}
	
	public void onClickRetry(){
		GameManager.instance.pause = false;
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	public void onClickTitleScreen(){
		GameManager.instance.pause = false;
		SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
	}

	public void showDeathMenu(){
		
		GameManager.instance.pause = true;
		deathMenu.SetActive(true);
	}

	public void showPauseMenu(){
		GameManager.instance.pause = true;
		pauseMenu.SetActive(true);
	}

	public void onClickResume(GameObject pauseMn){
		pauseMn.SetActive(false);
		GameManager.instance.pause = false;
	}

	public void onClickExit(){
		Application.Quit();
	}

	
	public void refreshCollectablesHUD(int currentCollectables, int maxCollectables){
		collectableHUDText.text = currentCollectables.ToString("00")+ "/" + maxCollectables.ToString("00");
	}
}
