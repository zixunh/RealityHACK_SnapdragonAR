using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperPlane : MonoBehaviour
{
    public Text LGesture;
    public Text RGesture;
    public GameObject DesktopPlane;
    public GameObject TextBG;
    public GameObject TextBoard;
    public GameObject radio;
    public enum FlightPlaneState {Flying, Parking, Expanding};
    FlightPlaneState curState;

    public Animator FlyingToParkingLotAnimator;
    public Animator FlyingToDesktopAnimator;

    private void Start()
    {
        curState = FlightPlaneState.Flying;
    }

    private void Update()
    {
        if(LGesture.text == "POINT" || RGesture.text == "POINT"){
            FlyToLaptop();
        }
    }

    public void FlyToParkingLot()
    {
        Debug.Log("flying");
        FlyingToParkingLotAnimator.SetTrigger("OnNewMail");
        curState = FlightPlaneState.Parking;
    }

    public void ExpandToPaper()
    {
        TextBoard.GetComponent<Text>().text = Functions.latestMail;
        TextBG.SetActive(true);
        curState = FlightPlaneState.Expanding;
    }

    public void FlyToLaptop()
    {
        gameObject.SetActive(false);
        TextBG.SetActive(false);
        DesktopPlane.SetActive(true);
        FlyingToDesktopAnimator.SetTrigger("OnTouch");
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
; 