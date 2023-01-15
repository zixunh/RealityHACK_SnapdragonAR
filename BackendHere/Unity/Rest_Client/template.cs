using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Proyecto26;
using Models;


//public class HTTPRequester : MonoBehaviour
//{
//    // string basePath = "http://172.20.10.4:8888";
//    // string basePath = "http://127.0.0.1:8080";
//    string basePath = "http://172.20.10.6:8080";

//    float timeCnt = 2.0f;

//    // Start is called before the first frame update
//    void Start()
//    {
//        Post();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        timeCnt -= Time.deltaTime;
//        if (timeCnt < 0)
//        {
//            Post();
//            timeCnt = 2.0f;
//        }
//    }

//    public void Post()
//    {
//        RestClient.Post<Post>(basePath, new Post
//        {
//            title = "My first title",
//            body = "My first message",
//            userId = 26
//        })
//        .Then(res => Debug.Log("Success"))
//        .Catch(err => Debug.Log("Error"));
//    }
//}


public class HTTPRequester : MonoBehaviour
{
    public static string baseURL = "http://localhost:8888/notification";
    string apiURL = baseURL;
    //float timeCnt = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Post();
    }

    // Update is called once per frame
    void Update()
    {
        // timeCnt -= Time.deltaTime;
        // if(timeCnt < 0)
        // {
        //     Post();
        //     timeCnt = 2.0f;
        // }
    }

    public void Post()
    {
        RestClient.Post<Post>(apiURL, new Post
        {
            deep = false
        })
        .Then(res => Debug.Log("Success"))
        .Catch(err => Debug.Log("Error"));
    }
}



