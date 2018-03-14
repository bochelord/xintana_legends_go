using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DynamicGrid : MonoBehaviour {

    public int col;

    private RectTransform parent;
    private GridLayoutGroup grid;
	// Use this for initialization
	void Start () {

        parent = gameObject.GetComponent<RectTransform>();
        grid = gameObject.GetComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2(parent.rect.width / col, grid.cellSize.y);
    }

    //private void Update()
    //{
    //    grid.cellSize = new Vector2(parent.rect.width / col, grid.cellSize.y);
    //}

}
