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

    [Header("EnemyPrefabs")]
    public GameObject[] enemiesPrefabs;

    [Header("Enemy List")]
    
    public static List<EnemyStructure> EnemyList = new List<EnemyStructure>();
    //public int world;



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
        //bestiaryList = new XintanaEnemiesBestiary();
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
            //Debug.Log("Key lifeBase:" + ssObjects[i]["lifeBase"].GetJsonType().ToString());
            XintanaEnemiesBestiary.XintanaEnemy bestiary_enemy = new XintanaEnemiesBestiary.XintanaEnemy();
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
                    tempEnemy.lifeBase = (float)(double)ssObjects[i]["lifeBase"];
                }
                else if (ssObjects[i]["lifeBase"].GetJsonType() == JsonType.Int)
                {
                    tempEnemy.lifeBase = (float)(int)ssObjects[i]["lifeBase"];
                }
                else if (ssObjects[i]["lifeBase"].GetJsonType() == JsonType.String)
                {
                    tempEnemy.lifeBase = float.Parse(ssObjects[i]["lifeBase"].ToString());
                }

                //Debug.Log("Key lifeBase:"+ ssObjects[i]["lifeBase"].GetJsonType().ToString());
                //Debug.Log(tempEnemy.lifeBase);
                //tempEnemy.lifeBase = (float)(double)ssObjects[i]["lifeBase"];
            }

            if (ssObjects[i].Keys.Contains("lifeGrowth"))
            {

                if (ssObjects[i]["lifeGrowth"].GetJsonType() == JsonType.Double)
                {
                    tempEnemy.lifeGrowth = (float)(double)ssObjects[i]["lifeGrowth"];
                } 
                else if (ssObjects[i]["lifeGrowth"].GetJsonType() == JsonType.Int)
                {
                    tempEnemy.lifeGrowth = (float)(int)ssObjects[i]["lifeGrowth"];
                } 
                else if (ssObjects[i]["lifeGrowth"].GetJsonType() == JsonType.String)
                {
                    tempEnemy.lifeGrowth = float.Parse(ssObjects[i]["lifeGrowth"].ToString());
                }


                //tempEnemy.lifeGrowth = (float)(double)ssObjects[i]["lifeGrowth"];
            }

            if (ssObjects[i].Keys.Contains("damageBase"))
            {

                if (ssObjects[i]["damageBase"].GetJsonType() == JsonType.Double)
                {
                    tempEnemy.damageBase = (float)(double)ssObjects[i]["damageBase"];
                } 
                else if (ssObjects[i]["damageBase"].GetJsonType() == JsonType.Int)
                {
                    tempEnemy.damageBase = (float)(int)ssObjects[i]["damageBase"];
                } 
                else if (ssObjects[i]["damageBase"].GetJsonType() == JsonType.String)
                {
                    tempEnemy.damageBase = float.Parse(ssObjects[i]["damageBase"].ToString());
                }



                //tempEnemy.damageBase = (float)(double)ssObjects[i]["damageBase"];
            }

            if (ssObjects[i].Keys.Contains("damageGrowth"))
            {

                if (ssObjects[i]["damageGrowth"].GetJsonType() == JsonType.Double)
                {
                    tempEnemy.damageGrowth = (float)(double)ssObjects[i]["damageGrowth"];
                } 
                else if (ssObjects[i]["damageGrowth"].GetJsonType() == JsonType.Int)
                {
                    tempEnemy.damageGrowth = (float)(int)ssObjects[i]["damageGrowth"];
                } 
                else if (ssObjects[i]["damageGrowth"].GetJsonType() == JsonType.String)
                {
                    tempEnemy.damageGrowth = float.Parse(ssObjects[i]["damageGrowth"].ToString());
                }

                //tempEnemy.damageGrowth = (float)(double)ssObjects[i]["damageGrowth"];
            }

            if (ssObjects[i].Keys.Contains("score"))
            {

                if (ssObjects[i]["score"].GetJsonType() == JsonType.Double)
                {
                    tempEnemy.score = (int)(double)ssObjects[i]["score"];
                } else if (ssObjects[i]["score"].GetJsonType() == JsonType.Int)
                {
                    tempEnemy.score = (int)ssObjects[i]["score"];
                } else if (ssObjects[i]["score"].GetJsonType() == JsonType.String)
                {
                    tempEnemy.score = int.Parse(ssObjects[i]["score"].ToString());
                }



                //tempEnemy.score = (int)ssObjects[i]["score"];
            }

            if (ssObjects[i].Keys.Contains("world"))
            {

                if (ssObjects[i]["world"].GetJsonType() == JsonType.Double)
                {
                    //tempEnemy.world = (int)(double)ssObjects[i]["world"];
                    Debug.LogError("world is a double :( (it should be a string)");
                } 
                else if (ssObjects[i]["world"].GetJsonType() == JsonType.Int)
                {
                    //tempEnemy.world = (int)ssObjects[i]["world"];
                    Debug.LogError("world is a double :( (it should be a string)");
                } 
                else if (ssObjects[i]["world"].GetJsonType() == JsonType.String)
                {

                    string tempstring = ssObjects[i]["world"].ToString();
                     
                    //we check the string where the format comes as : 1,2,3
                    int counter = 0;
                    tempEnemy.world = new int[tempstring.Split(',').Length];
                    for (int j = 0; j < tempstring.Length; j++)
                    {
                        if (tempstring[j] != ',')
                        {
                            //Debug.Log("j: " + j);
                            //Debug.Log("counter: " + counter);

                            tempEnemy.world[counter] = int.Parse(tempstring[j].ToString());
                            counter++;
                            //Debug.Log("tempstring.Length: " + tempstring.Length);
                            
                            //print("tempEnemy.world[counter]: " + tempEnemy.world[counter]);
                            //Debug.Log("char.Parse(tempstring[j].ToString(): " + char.Parse(tempstring[j].ToString()));

                        }
                    }



                    //tempEnemy.world = int.Parse(ssObjects[i]["world"].ToString());
                }



                //world = (int)ssObjects[i]["world"];
            }

            //EnemyList.Add(tempEnemy);
            //Debug.Log("Added Enemy as:" + tempEnemy.type.ToString());



            //Adding to the Scriptable Object

            

            bestiary_enemy.nameId = tempEnemy.type.ToString();

            bestiary_enemy.type = tempEnemy.type;

            bestiary_enemy.damageBase = tempEnemy.damageBase;
            bestiary_enemy.damageGrowth = tempEnemy.damageGrowth;
            bestiary_enemy.damage = tempEnemy.damage;
            bestiary_enemy.lifeBase = tempEnemy.lifeBase;
            bestiary_enemy.lifeGrowth = tempEnemy.lifeGrowth;
            bestiary_enemy.appearsInWorld = tempEnemy.world;
            //bestiary_enemy.prefab = tempEnemy.
            //bestiary_enemy.appearsInWorld = tempEnemy.world;

            bestiary_enemy.score = tempEnemy.score;


            for (int x = 0; x < enemiesPrefabs.Length; x++)
            {
                if (enemiesPrefabs[x].GetComponentInChildren<EnemyController>().type == bestiary_enemy.type)
                {
                    bestiary_enemy.prefab = enemiesPrefabs[x];
                }
            }


            bestiaryList.xintanaEnemies.Add(bestiary_enemy);
            Debug.Log("Added to Bestiary:" + bestiary_enemy.type.ToString());
            //tempEnemy = new EnemyStructure();

        }

        //XintanaEnemiesBestiary.XintanaEnemy temp_enemy = new XintanaEnemiesBestiary.XintanaEnemy();

        //foreach (EnemyStructure item in EnemyList)
        //{

            

        //    temp_enemy.nameId = item.type.ToString();
        //    temp_enemy.type = item.type;
            
        //    temp_enemy.damageBase = item.damageBase;
        //    temp_enemy.damageGrowth = item.damageGrowth;
        //    temp_enemy.damage = item.damage;
        //    temp_enemy.lifeBase = item.lifeBase;
        //    temp_enemy.lifeGrowth = item.lifeGrowth;
        //    temp_enemy.appearsInWorld = item.world;

        //    temp_enemy.score = item.score;
            

        //    bestiaryList.xintanaEnemies.Add(temp_enemy);
        //    Debug.Log("Added to Bestiary:" + temp_enemy.type.ToString());

        //}

        //bestiaryList.xintanaEnemies = EnemyList;
    }
}