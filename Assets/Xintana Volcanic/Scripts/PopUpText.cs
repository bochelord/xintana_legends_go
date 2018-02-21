using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpText : MonoBehaviour {

	//public GameObject prefab_popupmoney;
    //public GameObject prefab_messagelong;
    
    //private Vector3 position;
	private Vector3 screenPointPosition;
	private Camera Camerahold;
	private string text;
	private RectTransform rectTrans;
    private float timetodie;
    private Canvas canvas;
    private float text_speed;
    public float minSpeed;
    public float maxSpeed;
	// Use this for initialization
	void Start () {
		Camerahold = Camera.main;
		//screenPointPosition = Camerahold.WorldToScreenPoint (position);
		rectTrans = this.GetComponent<Text>().rectTransform;
        text_speed = Random.Range(minSpeed, maxSpeed);
        
        Killme(timetodie);
	}
	
	// Update is called once per frame
	void Update () {
		//rectTrans.position.y -= 1;
        
		this.transform.position += Vector3.up/text_speed;
	}

    public void Killme(float timetodie)
    {
        Destroy(this.gameObject, timetodie);
    }




	public static void ShowMoneyPopup(string texto, float width, Vector3 position, float timetodie){
		//var newInstance = new GameObject("Popup Text");
		GameObject newInstance = (GameObject)Instantiate(Resources.Load("Popup Text"));
        //GameObject newInstance = Instantiate(prefab_popupmoney) as GameObject;
		//var damagePopup = newInstance.AddComponent<PopUpText> ();
		//var damageUIPopup = newInstance.AddComponent<Text> ();

		var damagePopup = newInstance.GetComponent<PopUpText>();
		var damageUIPopup = newInstance.GetComponent<Text>();
        var damageImagePopup = newInstance.GetComponentInChildren<Image>();
		//Font ZagNormalFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Zag_Normal.otf") as Font;
		//Font Arialfont = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
		//Font papor = Resources.Load("Zag_Normal.otf") as Font;
		newInstance.transform.SetParent(FindObjectOfType<Canvas>().transform);

		//damagePopup.position = position;
		//damagePopup.text = texto;
        damagePopup.timetodie = timetodie;
		damageUIPopup.rectTransform.position= new Vector2(position.x,position.y + 50.0f);
		damageUIPopup.text = texto;

        damageUIPopup.DOFade(0.0f, timetodie);
        damageImagePopup.DOFade(0.0f, timetodie);
        //damageUIPopup.rectTransform.rect.Set(50f, 50f, width, 40.0f);
		//damageUIPopup.font = Arialfont;
		//damageUIPopup.rectTransform.wid
		//damageUIPopup.font = papor;
		//damageUIPopup.fontSize = 20;

	}

    public static void ShowMessageLong(string texto, float timetodie)
    {
        GameObject newInstance = (GameObject)Instantiate(Resources.Load("Popup Message"));

        newInstance.transform.SetParent(FindObjectOfType<Canvas>().transform);

        var messageSettings = newInstance.GetComponent<PopUpText>();
        var textSettings = newInstance.GetComponent<Text>();

        messageSettings.timetodie = timetodie;
        textSettings.text = texto;
        textSettings.rectTransform.position = new Vector3(-250f, 25f, 0);
        textSettings.rectTransform.offsetMin = new Vector2(100,50);
        textSettings.rectTransform.offsetMax = new Vector2(500,100);
        

    }
    

}
