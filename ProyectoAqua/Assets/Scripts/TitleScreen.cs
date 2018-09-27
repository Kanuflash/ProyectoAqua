using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
	int numberCharacters = 3;


	[SerializeField]
	GameObject[] characterSelection;

	// Use this for initialization
	void Start () {
		characterSelection[0].SetActive(true);
		showLevels();
        GameObject gb = GameObject.Find("GameManager");
        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        if (gb) { 
            Destroy(gb);
            Destroy(pl);
        }
	}

	void showLevels(){
		for(int i = 1; i< numberCharacters; i++){
			characterSelection[i].SetActive(PlayerPrefs.GetInt("Character_" + i, 0) == 1 || PlayerPrefs.GetInt("EverythingUnlocked", 0) == 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}



	public void onClickSelectGame(int character){
		PlayerPrefs.SetInt("SelectedCharacter", character);
		SceneManager.LoadScene("Intro", LoadSceneMode.Single);
	
	}


	public void onClickUnlockEverything()
	{
		if(PlayerPrefs.GetInt("EverythingUnlocked",0) == 1)
		{
			PlayerPrefs.SetInt("EverythingUnlocked", 0);
			showLevels();
		}
		else{
			PlayerPrefs.SetInt("EverythingUnlocked", 1);
			showLevels();
		}
	}

}
