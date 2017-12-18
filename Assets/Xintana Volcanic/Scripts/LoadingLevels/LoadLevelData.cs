using UnityEngine;
using System.Collections;

public class LoadLevelData : MonoBehaviour {


    public int selectedLevel; // 0 is the default level - IntroScreen_PressStart.
    private string levelName;

    public string[] levelNames;
	// Use this for initialization

    private static LoadLevelData _instance;
    public static LoadLevelData instance 
    { 
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LoadLevelData>();

                //Tell Unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
            
    }

    void Awake()
    {
        if (_instance == null)
        {
            // If I am the first instance, make me Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            // If a Singleton already exists and you find
            // another reference in scene, destroy it!
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

	void Start () {
        Debug.Log("LoadLevelData - Level Name: " + levelName);
        Debug.Log("LoadLevelData - Selected Level: " + selectedLevel);

        //if (selectedLevel > 2 || selectedLevel < 1)
        //{
        //    levelName = "IntroScreen_PressStart";
        //}

        //if (selectedLevel == 1)
        //{
        //    levelName = "level_01-space_ship";
        //}
        //else if (selectedLevel == 2)
        //{
        //    levelName = "planet_gogh-01";
        //}
        //else 
        //{
        //    levelName = "IntroScreen_PressStart";
        //}
	}

    public void SetSelectedLevel(int level)
    {
        selectedLevel = level;
        //Debug.Log("New Selected Level: " + selectedLevel);
    }

    public void SetLevelName(string name)
    {
        levelName = name;
    }

    public int GetSelectedLevel()
    {
        return selectedLevel;
    }

    public string GetSelectedLevelName()
    {
        return levelNames[selectedLevel];
    }
       

}
