using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public AudioClip burbujaAlga;
    public static GameManager instance;
    public int numLevelsRamdom = 1;
    private int actualLevel = 0;
    private Scene[] scenesProcedurals;
    private AudioSource audioSource;

    [SerializeField]
    GameObject[] players;

    [SerializeField]
    Transform startPosition;

    public bool pause = false;
    [SerializeField]
	public int maxCollectables = 0;
	[SerializeField]
	int currentCollectables = 0;
    GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            spawnCharacter();
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += SceneWasLoaded;
            audioSource = GetComponent<AudioSource>();
        }            
        else if (instance != this)
            Destroy(gameObject);

        maxCollectables = SceneManager.sceneCountInBuildSettings;
    }

    private void SceneWasLoaded(Scene arg0, LoadSceneMode arg1)
    {
        GameObject spawnPlayer = GameObject.FindGameObjectWithTag("SpawnPlayer");
        player.transform.position = spawnPlayer.transform.position;
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
	
    /*void SetRandomScenes()
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
    */

    public void collect()
    {
        if(currentCollectables< maxCollectables)
        {
            currentCollectables++;
            MenuController.instance.refreshCollectablesHUD(currentCollectables,maxCollectables);
        }
    }

    void spawnCharacter(){
        player = Instantiate(players[PlayerPrefs.GetInt("SelectedCharacter", 0)], startPosition);
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

    public void nextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            audioSource.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("you Win!!");
        }
    }

    
}
