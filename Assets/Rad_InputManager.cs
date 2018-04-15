using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            if (currentSwipe.x > 0.25f || currentSwipe.y > 0.25f || currentSwipe.x < -0.25f || currentSwipe.y < -0.25f)
            {
                Instantiate(UISwipePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 10f), Quaternion.identity);
            }
            else
            {
                Instantiate(UITouchPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 10f), Quaternion.identity);
            }

                
        }

        

	}
}
