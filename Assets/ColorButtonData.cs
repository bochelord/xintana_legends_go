using UnityEngine;
using System.Collections;

public class ColorButtonData : MonoBehaviour {

    public string buttonColor;
    public int position;

    public string returnButtonColor()
    {
        return buttonColor;
    }

    public int returnButtonPosition()
    {
        return position;
    }
}
