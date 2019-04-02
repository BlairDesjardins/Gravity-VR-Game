using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour
    {
        public SteamVR_Action_Boolean interactUI;
        public Hand hand;

        public LineRenderer laser;

        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (interactUI == null)
            {
                Debug.LogError("No interactUI action assigned");
                return;
            }

            interactUI.AddOnChangeListener(OnInteractUIActionChange, hand.handType);

            if (laser == null)
                laser = this.GetComponent<LineRenderer>();
        }

        private void OnDisable()
        {
            if (interactUI != null)
                interactUI.RemoveOnChangeListener(OnInteractUIActionChange, hand.handType);
        }

        private void OnInteractUIActionChange(SteamVR_Action_In actionIn)
        {
            Debug.Log("UI Click");
        }

        private void Update()
        {
            LayerMask mask = LayerMask.GetMask("UI");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
            {
                laser.enabled = true;
                laser.SetPosition(0, transform.position);
                laser.SetPosition(1, hit.point);
            }
            else
            {
                if (laser.enabled)
                    laser.enabled = false;
            }
            
        }
    }
}
