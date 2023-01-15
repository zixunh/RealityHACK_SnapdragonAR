using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions
{
    public GameObject paperPlanePrefab;

    GameObject InstantiatePaperPlane(Vector3 oriPos, string msg)
    {
        // spawn a paper plane at a given pos.
        var instance = Object.Instantiate(paperPlanePrefab);
        instance.transform.position = oriPos;
        instance.transform.rotation = Quaternion.identity;
        return instance;
    }
}