using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Qualcomm.Snapdragon.Spaces.Samples
{
    public class MyHandTrackingController : SampleController
    {
        public Text LeftHandGestureName;
        public Text LeftHandGestureRatio;
        public Text LeftHandFlipRatio;
        public Text RightHandGestureName;
        public Text RightHandGestureRatio;
        public Text RightHandFlipRatio;

        public GameObject Mirror;
        public GameObject MirroredPlayer;
        public GameObject MirroredPlayerHead;
        public GameObject MirroredJointObject;


        private GameObject[] _leftMirroredHandJoints;
        private GameObject[] _rightMirroredHandJoints;
        private Transform _mainCameraTransform;
        private SpacesHandManager _spacesHandManager;

        public override void Start()
        {
            base.Start();
            _mainCameraTransform = Camera.main.transform;
            _spacesHandManager = FindObjectOfType<SpacesHandManager>();
            _spacesHandManager.handsChanged += UpdateGesturesUI;

            _leftMirroredHandJoints = CreateHandJoints();
            _rightMirroredHandJoints = CreateHandJoints();
        }

        private GameObject[] CreateHandJoints()
        {
            var handJointCount = Enum.GetNames(typeof(SpacesHand.JointType)).Length;
            var joints = new GameObject[handJointCount];

            for (var i = 0; i < joints.Length; i++)
            {
                joints[i] = Instantiate(MirroredJointObject);
                joints[i].hideFlags = HideFlags.HideInHierarchy;
                joints[i].GetComponent<Renderer>().material.color = NormalizedColorForJoint(i);
            }
            joints[10].transform.localScale *= 2;

            return joints;
        }

        private static Color NormalizedColorForJoint(int jointId)
        {
            return Enum.GetName(typeof(SpacesHand.JointType), jointId).Split('_')[0] switch
            {
                "PALM" => Color.white,
                "WRIST" => new Color(200f / 255f, 200f / 255f, 200f / 255f),
                "THUMB" => new Color(255f / 255f, 196f / 255f, 0f / 255f),
                "INDEX" => new Color(26f / 255f, 201f / 255f, 56f / 255f),
                "MIDDLE" => new Color(0f / 255f, 215f / 255f, 255f / 255f),
                "RING" => new Color(139f / 255f, 0f / 255f, 226f / 255f),
                "LITTLE" => new Color(200f / 255f, 0f / 255f, 200f / 255f),
                _ => Color.black
            };
        }

        public override void Update()
        {
            base.Update();
            UpdateHand(true);
            UpdateHand(false);
        }

        private void UpdateGesturesUI(SpacesHandsChangedEventArgs args)
        { // subscribe to a gesture change event
            foreach (var hand in args.updated)
            {
                var gestureNameTextField = hand.IsLeft ? LeftHandGestureName : RightHandGestureName;
                var gestureRatioTextField = hand.IsLeft ? LeftHandGestureRatio : RightHandGestureRatio;
                var flipRatioTextField = hand.IsLeft ? LeftHandFlipRatio : RightHandFlipRatio;

                gestureNameTextField.text = Enum.GetName(typeof(SpacesHand.GestureType), hand.CurrentGesture.Type);
                gestureRatioTextField.text = (int)(hand.CurrentGesture.GestureRatio * 100f) + " %";
                flipRatioTextField.text = hand.CurrentGesture.FlipRatio.ToString("0.00");
            }

            foreach (var hand in args.removed)
            {
                var gestureNameTextField = hand.IsLeft ? LeftHandGestureName : RightHandGestureName;
                var gestureRatioTextField = hand.IsLeft ? LeftHandGestureRatio : RightHandGestureRatio;
                var flipRatioTextField = hand.IsLeft ? LeftHandFlipRatio : RightHandFlipRatio;

                gestureNameTextField.text = "-";
                gestureRatioTextField.text = "-";
                flipRatioTextField.text = "-";
            }
        }

        private void UpdateHand(bool leftHand)
        {
            var joints = leftHand ? _leftMirroredHandJoints : _rightMirroredHandJoints;
            for (var i = 0; i < _leftMirroredHandJoints.Length; i++)
            {
                var hand = leftHand ? _spacesHandManager.LeftHand : _spacesHandManager.RightHand;
                if (hand == null)
                {
                    joints[i].SetActive(false);
                    continue;
                }
                joints[i].SetActive(true);
                joints[i].transform.position = hand.Joints[i].Pose.position;
            }
        }

    }
}