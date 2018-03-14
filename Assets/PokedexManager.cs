using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PokedexManager : MonoBehaviour {

    public GameObject[] _enemies;
    [Header("Movement values")]

    public Transform leftPosition;
    public Transform rightPosition;
    public Transform centerScreenPosition;

    public float timeForMoving;

    // Variables for swipe gestures
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private PokedexProfile _pokedex;
    public int pokedexIndex = 0;
    private PokedexStep _step;
    private Swipe _swipe;
    private enum Swipe
    {
        Left,
        Right
    }
    private enum PokedexStep
    {
        WaitingForInput,
        CheckAnimation,
        ShowEnemy
    }
    void Awake()
    {
        _pokedex = Rad_SaveManager.pokedex;
        _enemies[0].transform.DOMove(centerScreenPosition.position, timeForMoving, false);
        _step = PokedexStep.WaitingForInput;
    }

    void Update()
    {
        switch (_step)
        {
            case PokedexStep.CheckAnimation:
                MoveEnemies();
                break;
            case PokedexStep.ShowEnemy:
                //TODO SHOW STATS
                Debug.Log(_enemies[pokedexIndex]);
                _step = PokedexStep.WaitingForInput;
                break;
            case PokedexStep.WaitingForInput:
                checkInput();
                break;
        }
    }

    #region Input
    public void checkInput()
    {
        checkSwipeTouch();
        checkSwipeClick();
    }
    void checkSwipeTouch()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();


                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    _swipe = Swipe.Left;
                    if(pokedexIndex >= _enemies.Length)
                    {
                        pokedexIndex = 0;
                    }
                    else
                    {
                        pokedexIndex++;
                    }
                }

                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    _swipe = Swipe.Right;
                    if (pokedexIndex <= 0)
                    {
                        pokedexIndex = _enemies.Length;
                    }
                    else
                    {
                        pokedexIndex--;
                    }
                }
                _step = PokedexStep.CheckAnimation;
            }
        }
    }
    void checkSwipeClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                _swipe = Swipe.Left;
                if (pokedexIndex >= _enemies.Length - 1)
                {
                    pokedexIndex = 0;
                }
                else
                {
                    pokedexIndex++;
                }
            }

            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                _swipe = Swipe.Right;
                if (pokedexIndex <= 0)
                {
                    pokedexIndex = _enemies.Length - 1;
                }
                else
                {
                    pokedexIndex--;
                }
            }
            _step = PokedexStep.CheckAnimation;
        }


    }
    #endregion

    #region Animation

    private void MoveEnemies()
    {
        if(pokedexIndex == 0 && _swipe == Swipe.Left)
        {
            _enemies[_enemies.Length-1].transform.DOMove(leftPosition.position, timeForMoving, false);
            _enemies[0].transform.DOMove(rightPosition.position, 0, false); //set postion to right in case it's not
            _enemies[0].transform.DOMove(centerScreenPosition.position, timeForMoving, false);
        }
        else if(pokedexIndex == _enemies.Length-1 && _swipe == Swipe.Right)
        {
            _enemies[_enemies.Length-1].transform.DOMove(leftPosition.position, 0, false);
            _enemies[_enemies.Length-1].transform.DOMove(centerScreenPosition.position, timeForMoving, false); 
            _enemies[0].transform.DOMove(rightPosition.position, timeForMoving, false);
        }
        else if(_swipe == Swipe.Right)
        {
            _enemies[pokedexIndex + 1].transform.DOMove(rightPosition.position, timeForMoving, false);
            _enemies[pokedexIndex].transform.DOMove(leftPosition.position, 0, false);
            _enemies[pokedexIndex].transform.DOMove(centerScreenPosition.position, timeForMoving, false);
        }
        else if(_swipe == Swipe.Left)
        {
            _enemies[pokedexIndex - 1].transform.DOMove(leftPosition.position, timeForMoving, false);
            _enemies[pokedexIndex].transform.DOMove(rightPosition.position, 0, false);
            _enemies[pokedexIndex].transform.DOMove(centerScreenPosition.position, timeForMoving, false);
        }

        _step = PokedexStep.ShowEnemy;
    }

    #endregion
}
