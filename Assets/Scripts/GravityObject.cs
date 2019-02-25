using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class GravityObject : MonoBehaviour
    {
        public bool gravityActive = false;

        private float gravityModifier = 0.06674f; //0.00000000006674f;
        private Rigidbody cubeRB;

        private void Start()
        {
            cubeRB = GetComponent<Rigidbody>();
        }

        private void OnAttachedToHand(Hand hand)
        {
            gravityActive = true;
        }

        public bool IsGravityActive()
        {
            return gravityActive;
        }

        private void OnCollisionEnter(Collision collision)
        {
            gravityActive = true;
        }

        private void Update()
        {
            if (gravityActive)
            {
                GameObject[] gravityObjects = GameObject.FindGameObjectsWithTag("GravityObject");

                foreach (GameObject gravityObject in gravityObjects)
                {
                    if (gravityObject.GetComponent<GravityObject>().IsGravityActive())
                    {
                        float distance = Vector3.Distance(gravityObject.transform.position, transform.position);

                        if (distance >= 0.001f)
                        {
                            Vector3 gravityVector = (gravityObject.transform.position - transform.position).normalized;

                            float gravityMagnitude = (gravityModifier * cubeRB.mass * gravityObject.GetComponent<Rigidbody>().mass) / (distance * distance);

                            Vector3 gravityForce = new Vector3(gravityVector.x * gravityMagnitude, gravityVector.y * gravityMagnitude, gravityVector.z * gravityMagnitude);

                            cubeRB.AddForce(gravityForce);
                        }
                    }
                }
            }
        }
    }
}
