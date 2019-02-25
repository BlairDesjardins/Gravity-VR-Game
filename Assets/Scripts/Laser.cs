using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem
{
    public class Laser : MonoBehaviour
    {

        public SteamVR_Action_Boolean laserAction;

        public Hand hand;

        public LineRenderer laser;

        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (laserAction == null)
            {
                Debug.LogError("No laser action assigned");
                return;
            }

            laserAction.AddOnChangeListener(OnLaserActionChange, hand.handType);

            if (laser == null)
                laser = this.GetComponent<LineRenderer>();
        }

        private void OnDisable()
        {
            if (laserAction != null)
                laserAction.RemoveOnChangeListener(OnLaserActionChange, hand.handType);
        }

        private void OnLaserActionChange(SteamVR_Action_In actionIn)
        {
            if (laserAction.GetStateDown(hand.handType))
            {
                laser.enabled = true;
            }
            else if (laserAction.GetStateUp(hand.handType))
            {
                laser.enabled = false;
            }
        }

        private void Update()
        {
            if (laser.enabled)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    laser.SetPosition(0, transform.position);
                    laser.SetPosition(1, hit.point);
                }
                else
                {
                    laser.SetPosition(0, transform.position);
                    laser.SetPosition(1, transform.TransformDirection(Vector3.forward) * 1000);
                }
            }
            
        }
    }
}
