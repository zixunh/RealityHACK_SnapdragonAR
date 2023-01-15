using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Proyecto26;
using Models;
using Newtonsoft.Json.Linq;

public class HTTPRequester_PlaneUpdate : MonoBehaviour
{
    string baseURL = "http://localhost:8888/notification/";
    float timeCnt = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Post();
    }

    // Update is called once per frame
    void Update()
    {
        timeCnt -= Time.deltaTime;
        if (timeCnt < 0)
        {
            Post();
            timeCnt = 2.0f;
        }
    }

    public void Post()
    {
        RestClient.Post<ReturnCheckIfNewMail>(baseURL + "check", new Post { }).Then(response => {
            Debug.Log(JObject.Parse(response.ToString())["has_new"]);
            if (JObject.Parse(response.ToString())["has_new"].ToString() == "True")
            {
                Debug.Log("has_new");
                // If has new, store latest unread mail
                RestClient.Post<ReturnReadUnreadMail>(baseURL + "unread", new ReadUnreadMail
                {
                    readmode = "noread"
                }).Then(res=> {
                    Debug.Log(
                        JObject.Parse(res.ToString())["latest_preview"]
                    ); });
            }
        });
    }
}