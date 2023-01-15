using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlane : MonoBehaviour
{
    enum FlightPlaneState {Flying, Parking, Expanding};
    FlightPlaneState curState;

    private void Start()
    {
        curState = FlightPlaneState.Flying;
        FlyToParkingLot();
    }

    private void Update()
    {

    }

    void FlyToParkingLot()
    {
        
    }

    void ExpandToPaper()
    {


        // timeline --> enum set to expanding
    }

    void FlyToLaptop()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("joint") == true)
        {
            if(curState == FlightPlaneState.Parking)
            {
                curState = FlightPlaneState.Flying;
                ExpandToPaper();
            }

            if(curState == FlightPlaneState.Expanding)
            {
                curState = FlightPlaneState.Flying;
                FlyToLaptop();
            }
        }
    }

}
