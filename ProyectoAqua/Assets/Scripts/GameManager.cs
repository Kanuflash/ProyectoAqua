using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    [SerializeField]
    public Scene[] scenesRamdom;
    public static GameManager instance = null;
    public int numLevelsRamdom = 1;
    private int actualLevel = 0;
    private Scene[] scenesProcedurals;

    [SerializeField]
    GameObject[] players;

    [SerializeField]
    Transform startPosition;

    public bool pause = false;
    [SerializeField]
	public int maxCollectables = 0;
	[SerializeField]
	int currentCollectables = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        spawnCharacter();
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        
        /*if (numLevelsRamdom > scenesRamdom.Length)
        {
            numLevelsRamdom = scenesRamdom.Length; 
        }

        scenesProcedurals = new Scene[numLevelsRamdom];
        SetRandomScenes();*/

    }
	
    void SetRandomScenes()
    {
        int i = 0;

        while (numLevelsRamdom > 1)
        {
            int k = Random.Range(0, numLevelsRamdom);
            numLevelsRamdom--;
            maxCollectables++;
            Scene value = scenesRamdom[k];
            scenesRamdom[k] = scenesRamdom[numLevelsRamdom];
            scenesRamdom[numLevelsRamdom] = value;
            scenesProcedurals[i++] = value;
        }
    }	

    public void collect()
    {
        if(currentCollectables< maxCollectables)
        {
            currentCollectables++;
            MenuController.instance.refreshCollectablesHUD(currentCollectables,maxCollectables);
        }
    }

    void spawnCharacter(){
        GameObject player = Instantiate(players[PlayerPrefs.GetInt("SelectedCharacter", 0)], startPosition);
        player.transform.SetParent(null);
    }
    public void unlockCharacter(int character){
        PlayerPrefs.SetInt("Character_" + character, 1);
    }

    public void die(){
        pause = true;
        MenuController.instance.showDeathMenu();
    }
    public void winGame(){
        unlockCharacter(2);
        if(currentCollectables == maxCollectables) unlockCharacter(3);
    }
}
