using UnityEngine;
using System.Collections;

public class ResolutionManager : MonoBehaviour 
{
    [Header("Array of Elements to Scale")]
    public Transform[] toScale;                     //Elements to scale
    [Header("Array of Elements to Reposition")]
    public Transform[] toReposition;                //Elements to reposition

    [Header("Renderers for floors and roofs")]
    public Renderer floor;                          //The renderer of the floor
    //public Renderer floor2;
    public Renderer roof;                           //The renderer of the roof
    private float scaleFactor;                      //The current scale factor
    //private float repositionFactor;               // The Reposition Factor will be used to reposition any elements in the toReposition Transform array.      
    void Start()
    {
		scaleFactor = Camera.main.aspect / 1.28f;
        //repositionFactor = Camera.main.aspect; // First value test to relocate elements.
        //Rescale elements
        foreach (Transform item in toScale)
            item.localScale = new Vector3(item.localScale.x * scaleFactor, item.localScale.y * scaleFactor, item.localScale.z);

        //Reposition Elements
        foreach (Transform item in toReposition)
            item.position = new Vector3(item.position.x * scaleFactor, item.position.y, item.position.z);
            //item.position = new Vector3(item.position.x * repositionFactor, item.position.y, item.position.z); 

        //Rescale floor if it's not null/empty/exists 
        if (floor)
        {
            floor.material.mainTextureScale = new Vector2(scaleFactor, 1);
        }
            
        //floor2.material.mainTextureScale = new Vector2(scaleFactor, 1);
        //Rescale roof if it's not null/empty/exists
        if (roof)
        {
            roof.material.mainTextureScale = new Vector2(scaleFactor, 1);
        }
            
    }
}
