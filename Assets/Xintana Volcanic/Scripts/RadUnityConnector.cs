using System.Collections;
using UnityEngine;
using LitJson;

public class RadUnityConnector : MonoBehaviour
{
    public string webServiceUrl = "";
    public string spreadsheetId = "";
    public string worksheetName = "";
    public string worksheet2Name = "";
    public string password = "";
    public float maxWaitTime = 10f;
    //public GameObject dataDestinationObject;
    //public LevelChunkCreator levelChunkCreator;
    public string statisticsWorksheetName = "Statistics";
    public bool debugMode;

    bool updating;
    string currentStatus;
    JsonData[] ssObjects;
    JsonData[] ssObjects2;
    bool saveToGS;

    //Rect guiBoxRect;
    //Rect guiButtonRect;
    //Rect guiButtonRect2;
    //Rect guiButtonRect3;

    void Start()
    {
        updating = false;
        currentStatus = "Offline";
        saveToGS = false;
    }


    public void Connect()
    {
        if (updating)
            return;

        updating = true;
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        string connectionString = webServiceUrl + "?ssid=" + spreadsheetId + "&sheet=" + worksheetName + "&pass=" + password + "&action=GetData";
        if (debugMode)
            Debug.Log("Connecting to webservice on " + connectionString);

        WWW www = new WWW(connectionString);

        float elapsedTime = 0.0f;
        currentStatus = "Stablishing Connection... ";

        while (!www.isDone)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= maxWaitTime)
            {
                currentStatus = "Max wait time reached, connection aborted.";
                Debug.Log(currentStatus);
                updating = false;
                break;
            }

            yield return null;
        }

        if (!www.isDone || !string.IsNullOrEmpty(www.error))
        {
            currentStatus = "Connection error after" + elapsedTime.ToString() + "seconds: " + www.error;
            Debug.LogError(currentStatus);
            updating = false;
            yield break;
        }

        string response = www.text;
        Debug.Log(elapsedTime + " : " + response);
        currentStatus = "Connection stablished, parsing data...";

        if (response == "\"Incorrect Password.\"")
        {
            currentStatus = "Connection error: Incorrect Password.";
            Debug.LogError(currentStatus);
            updating = false;
            yield break;
        }

        try
        {
            ssObjects = JsonMapper.ToObject<JsonData[]>(response);
        } catch
        {
            currentStatus = "Data error: could not parse retrieved data as json.";
            Debug.LogError(currentStatus);
            updating = false;
            yield break;
        }

        currentStatus = "Data Successfully Retrieved!" + response;
        updating = false;


        //we call the second getdata to 
        //StartCoroutine(GetData2());


    }



    public void ManipulateData(JsonData[] ssObjects)
    {
        //We manipulate the data for the first Json object
        for (int i = 0; i < ssObjects.Length; i++)
        {

            //Key name marks the beginning of a new enemy
            if (ssObjects[i].Keys.Contains("name"))
            {
                if (string.Equals(ssObjects[i]["name"].ToString(), "Zazuc"))
                {

                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Black Knight"))
                {

                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Devil"))
                {

                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Explorer"))
                {

                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Fire Mage"))
                {

                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Ice Beast"))
                {

                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Lava Beast"))
                {

                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Alchemist"))
                {

                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Skeleton Mage"))
                {

                }
            }

            if (ssObjects[i].Keys.Contains("lifeBase"))
            {

            }

            if (ssObjects[i].Keys.Contains("lifeGrowth"))
            {

            }

            if (ssObjects[i].Keys.Contains("damageBase"))
            {

            }

            if (ssObjects[i].Keys.Contains("damageGrowth"))
            {

            }

            if (ssObjects[i].Keys.Contains("score"))
            {

            }

            if (ssObjects[i].Keys.Contains("world"))
            {

            }
        }
    }
}