using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rad_InputManager : MonoBehaviour {

    public GameObject UITouchPrefab;
    public GameObject UISwipePrefab;
    private enum Swipe
    {
        Left,
        Right
    }
    private Swipe _swipe;

    private Vector2 firstPos, secondPos, firstTouch, secondTouch, currentSwipe;

    // Update is called once per frame
    void Update() {

        if (Input.GetButtonDown("Fire1"))
        {
            firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }


        if (Input.GetButtonUp("Fire1"))
        {


            // save ended touch 2d point
            secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPos.x - firstPos.x, secondPos.y - firstPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                _swipe = Swipe.Left;
            }

            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                _swipe = Swipe.Right;

            }

            if (currentSwipe.x > 0.5f || currentSwipe.y > 0.5f || currentSwipe.x < -0.5f || currentSwipe.y < -0.5f)
            {
                //Instantiate(UISwipePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 10f), Quaternion.identity);
            }
            else
            {
                Instantiate(UITouchPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 10f), Quaternion.identity);
            }

                
        }


        //Back button quit -- Apparently the back button from Android is mapped in Unity to the "Escape" key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name.Contains("Menu"))
            {
                //Application.Quit();
                MainMenuManager mainMenuManager = FindObjectOfType<MainMenuManager>();
                mainMenuManager.Show_QuitPanel();
            }
            else if (currentScene.name.Contains("Shop"))
            {
                AsyncLoadLevel.LoadScene("XintanaLegendsGo_Menu");
            }
            else if (currentScene.name.Contains("Settings"))
            {
                AsyncLoadLevel.LoadScene("XintanaLegendsGo_Menu");
            }
            else if (currentScene.name.Contains("credits"))
            {
                AsyncLoadLevel.LoadScene("SettingsScreen");
            }
            else if (currentScene.name.Contains("Pokedex"))
            {
                AsyncLoadLevel.LoadScene("XintanaLegendsGo_Menu");
            }
        }
        

	}
}
