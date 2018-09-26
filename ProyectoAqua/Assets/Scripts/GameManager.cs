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

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {

        if (numLevelsRamdom > scenesRamdom.Length)
        {
            numLevelsRamdom = scenesRamdom.Length; 
        }

        scenesProcedurals = new Scene[numLevelsRamdom];
        SetRandomScenes();

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

}
