using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Proyecto26;
using Models;
using Newtonsoft.Json.Linq;

public class HTTPRequester_CloudClick : MonoBehaviour
{
    string baseURL = Functions.baseURL;
    // float timeCnt = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // timeCnt -= Time.deltaTime;
        // if (timeCnt < 0)
        // {
        //     Post();
        //     timeCnt = 2.0f;
        // }
    }

    void OnEnable(){
        Post();
    }

    public void Post()
    {
        RestClient.Post<Post>(baseURL, new OpenNotificationBoard
        {
            deep = false
        }).Then(response => {
            Debug.Log("Latest Message Renew");////////////////// PESUDO-CODE HERE ///
                                              //////////////////////////////////////////////
            RestClient.Post<ReturnReadUnreadMail>(baseURL + "unread", new ReadUnreadMail
            { readmode = "readall" }
            );
        }
                );
    }
}