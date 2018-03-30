//using ProgressBar;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class AsyncLoadLevel : MonoBehaviour {


    [Header("Binding")]
    public static string LoadingScreenSceneName = "LoadingScreen";


    //private ProgressBarBehaviour barBehaviour;
    private AsyncOperation _asyncOperation; // When assigned, load is in progress.
    protected static string _sceneToLoad = "";

    /// <summary>
    /// Call this static method to load a scene from anywhere
    /// </summary>
    /// <param name="sceneToLoad">Level name.</param>
    public static void LoadScene(string sceneToLoad)
    {
        _sceneToLoad = sceneToLoad;
        Application.backgroundLoadingPriority = ThreadPriority.High;
        if (LoadingScreenSceneName != null)
        {
            SceneManager.LoadScene(LoadingScreenSceneName);
        }
    }

    protected virtual void Start()
    {
        if (_sceneToLoad != "")
        {
            StartCoroutine(LoadAsynchronously());
        }
    }


    /// <summary>
    /// Loads the scene to load asynchronously.
    /// </summary>
    protected virtual IEnumerator LoadAsynchronously()
    {
        // we setup our various visual elements
        //LoadingSetup();

        // we start loading the scene
        _asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Single);
        _asyncOperation.allowSceneActivation = false;

        // while the scene loads, we assign its progress to a target that we'll use to fill the progress bar smoothly
        while (_asyncOperation.progress < 0.9f)
        {
            //_fillTarget = _asyncOperation.progress;
            yield return null;
        }
        //// when the load is close to the end (it'll never reach it), we set it to 100%
        //_fillTarget = 1f;

        //Debug.Log("filltarget esta lleno");

        // we wait for the bar to be visually filled to continue
        //while (LoadingProgressBar.GetComponent<Image>().fillAmount != _fillTarget)
        //{
        //    yield return null;
        //}

        // the load is now complete, we replace the bar with the complete animation
        //LoadingComplete();
        //yield return new WaitForSeconds(LoadCompleteDelay);

        // we fade to black
        //EduoLoadingGUIManager.Instance.FaderOn(true, ExitFadeDuration);
        //yield return new WaitForSeconds(ExitFadeDuration);

        // we switch to the new scene
        _asyncOperation.allowSceneActivation = true;
    }

}
