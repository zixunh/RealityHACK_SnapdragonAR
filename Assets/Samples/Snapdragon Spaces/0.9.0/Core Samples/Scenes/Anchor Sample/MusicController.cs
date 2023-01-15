using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public PaperPlane plane;
    public GameObject myLeftJoint;
    public GameObject myRightJoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isEnter = false;
        // isEnter |= gameObject.GetComponent<Collider>().bounds.Contains(myLeftJoint.transform.position);
        // isEnter |= gameObject.GetComponent<Collider>().bounds.Contains(myRightJoint.transform.position);
        // if(isEnter == true && plane.curState == plane.FlightPlaneState.Parking){
        //     plane.ExpandToPaper();
        // }
        // else if(isEnter == true && plane.FlightPlaneState.Expanding){
        //     plane.FlyToLaptop();
        // }
        isEnter |= gameObject.GetComponent<Collider>().bounds.Contains(myLeftJoint.transform.position);
        isEnter |= gameObject.GetComponent<Collider>().bounds.Contains(myRightJoint.transform.position);
        if(isEnter == true){
            gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("joint") == true)
        {
            gameObject.SetActive(false);
        }
    }
}
