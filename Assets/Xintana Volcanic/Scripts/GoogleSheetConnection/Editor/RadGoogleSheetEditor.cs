using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class RadGoogleSheetEditor : MonoBehaviour
{


    //[MenuItem("Radical Graphics/FetchEnemies")]
    //static void FetchEnemies()
    //{

    //    RadUnityConnector conn = GameObject.FindObjectOfType<RadUnityConnector>();


    //    string connectionString = conn.webServiceUrl + "?ssid=" + conn.spreadsheetId + "&sheet=" + conn.worksheetName + "&pass=" + conn.password + "&action=GetData";
    //    Debug.Log("Connecting to webservice on " + connectionString);

    //    WWW www = new WWW(connectionString);

    //    JsonData[] ssObjects;
    //    while (!www.isDone) ;
    //    if (!string.IsNullOrEmpty(www.error))
    //    {
    //        Debug.Log("WWW failed: " + www.error);

    //    }

    //    ssObjects = JsonMapper.ToObject<JsonData[]>(www.text);
    //    Debug.Log("Data Successfully Retrieved!" + www.text);

    //}

    [MenuItem("Radical Graphics/Connect")]
    static void ConnectaJoder()
    {
        RadUnityConnector conn = GameObject.FindObjectOfType<RadUnityConnector>();
        string connectionString = conn.webServiceUrl + "?ssid=" + conn.spreadsheetId + "&sheet=" + conn.worksheetName + "&pass=" + conn.password + "&action=GetData";

        conn.Connect();
    }


    //[MenuItem("Radical Graphics/FetchEnemies")]
    //static void FetchEnemies()
    //{
    //    RadUnityConnector conn = GameObject.FindObjectOfType<RadUnityConnector>();
    //    string connectionString = conn.webServiceUrl + "?ssid=" + conn.spreadsheetId + "&sheet=" + conn.worksheetName + "&pass=" + conn.password + "&action=GetData";
    //    Debug.Log("Connecting to webservice on " + connectionString);

    //    WWW www = new WWW(connectionString);

    //    launchy(www);
    //}

    //private void launchy(WWW www)
    //{
    //    StartCoroutine(WaitForRequest(www));
    //}


    //IEnumerator WaitForRequest(WWW www)
    //{
    //    yield return www;

    //    // check for errors
    //    if (www.error == null)
    //    {
    //        Debug.Log("WWW Ok!: " + www.text);
    //    } else
    //    {
    //        Debug.Log("WWW Error: " + www.error);
    //    }
    //}

}
