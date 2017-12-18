//using ProgressBar;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class AsyncLoadLevel : MonoBehaviour {

    //private ProgressBarBehaviour barBehaviour;
    private AsyncOperation async; // When assigned, load is in progress.
    //public int selectedLevel = 0; // 0 is the default level - Spacecraft Fortress.
    //private string levelName;
    //private int levelNumber;
    //private LoadLevelData loadLevelData;
    //float UpdateDelay = 2f;
    //void Awake()
    //{
    //    DontDestroyOnLoad(this.gameObject);
    //}
    IEnumerator Start()
    {
        //loadLevelData = GameObject.Find("PermanentData").GetComponent<LoadLevelData>();
        //levelName = loadLevelData.GetSelectedLevelName();
        //levelNumber = loadLevelData.GetSelectedLevel();
        //Debug.Log("CallProgressBar level name: " + levelName);
        //async = Application.LoadLevelAsync("combinationDisplay_safe_portrait");

        async = SceneManager.LoadSceneAsync("combinationDisplay_safe_portrait");
        //if (async.isDone)
        //{
        //    loadLevelData.SetSelectedLevel(levelNumber + 1);
        //}

        yield return 0;
        //barBehaviour = GetComponent<ProgressBarBehaviour>();
        
        
        //while (!async.isDone)
        //{
        //    barBehaviour.Value = async.progress * 100;
        //    yield return (0);
        //}
    }

    //public void SetSelectedLevel(int level)
    //{
    //    selectedLevel = level;
    //}
}
