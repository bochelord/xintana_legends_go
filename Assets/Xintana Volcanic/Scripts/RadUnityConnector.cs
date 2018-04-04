using System.Collections.Generic;
using System;
using UnityEngine;
using LitJson;
using System.Collections;

public class RadUnityConnector : MonoBehaviour
{
    public string webServiceUrl = "";
    public string spreadsheetId = "";
    public string worksheetName = "";
    //public string worksheet2Name = "";
    public string password = "";
    public float maxWaitTime = 10f;

    [Header("Enemies Scriptable Object")]
    public XintanaEnemiesBestiary bestiaryList;

    [Header("Enemy List")]
    [SerializeField]
    public List<EnemyStructure> EnemyList;
    public int world;



    //public GameObject dataDestinationObject;
    //public LevelChunkCreator levelChunkCreator;
    //public string statisticsWorksheetName = "Statistics";
    public bool debugMode;

    bool updating;
    string currentStatus;
    JsonData[] ssObjects;
    JsonData[] ssObjects2;
    bool saveToGS;


    void Start()
    {
        updating = false;
        currentStatus = "Offline";
        saveToGS = false;
    }

    #region Connection
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

        ManipulateData(ssObjects);
    }

    #endregion


    public void ManipulateData(JsonData[] ssObjects)
    {

        EnemyStructure tempEnemy = new EnemyStructure();

        //XintanaEnemiesBestiary.XintanaEnemy tempEnemy = new XintanaEnemiesBestiary.XintanaEnemy(); 
        //We manipulate the data for the first Json object
        for (int i = 0; i < ssObjects.Length; i++)
        {

            //Key name marks the beginning of a new enemy
            if (ssObjects[i].Keys.Contains("name"))
            {
                if (string.Equals(ssObjects[i]["name"].ToString(), "Zazuc"))
                {
                    tempEnemy.type = EnemyType.zazuc;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Black Knight"))
                {
                    tempEnemy.type = EnemyType.blackKnight;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Devil"))
                {
                    tempEnemy.type = EnemyType.devil;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Explorer"))
                {
                    tempEnemy.type = EnemyType.explorer;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Fire Mage"))
                {
                    tempEnemy.type = EnemyType.fireMage;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Ice Beast"))
                {
                    tempEnemy.type = EnemyType.iceBeast;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Lava Beast"))
                {
                    tempEnemy.type = EnemyType.lavabeast;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Alchemist"))
                {
                    tempEnemy.type = EnemyType.alchemist;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Skeleton Mage"))
                {
                    tempEnemy.type = EnemyType.skeletonMage;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Smile Zombie"))
                {
                    tempEnemy.type = EnemyType.smileZombie;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Parasite"))
                {
                    tempEnemy.type = EnemyType.parasite;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Mutant"))
                {
                    tempEnemy.type = EnemyType.mutant;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Blood Mage"))
                {
                    tempEnemy.type = EnemyType.bloodMage;
                }
                else if (string.Equals(ssObjects[i]["name"].ToString(), "Berserker Male"))
                {
                    tempEnemy.type = EnemyType.bersekerMale;
                }
            }

            if (ssObjects[i].Keys.Contains("lifeBase"))
            {

                if (ssObjects[i]["lifeBase"].GetJsonType() == JsonType.Double)
                {
                    Debug.Log("KEY: lifeBase is: " + JsonType.Double.ToString() );
                }
                //else 

                Debug.Log("Key lifeBase:"+ ssObjects[i]["lifeBase"].GetJsonType().ToString());
                tempEnemy.lifeBase = (float)(double)ssObjects[i]["lifeBase"];
            }

            if (ssObjects[i].Keys.Contains("lifeGrowth"))
            {
                tempEnemy.lifeGrowth = (float)(double)ssObjects[i]["lifeGrowth"];
            }

            if (ssObjects[i].Keys.Contains("damageBase"))
            {
                tempEnemy.damageBase = (float)(double)ssObjects[i]["damageBase"];
            }

            if (ssObjects[i].Keys.Contains("damageGrowth"))
            {
                tempEnemy.damageGrowth = (float)(double)ssObjects[i]["damageGrowth"];
            }

            if (ssObjects[i].Keys.Contains("score"))
            {
                tempEnemy.score = (int)ssObjects[i]["score"];
            }

            if (ssObjects[i].Keys.Contains("world"))
            {
                world = (int)ssObjects[i]["world"];
            }

            EnemyList.Add(tempEnemy);
            //bestiaryList.xintanaEnemies.Add()
        }
    }
}