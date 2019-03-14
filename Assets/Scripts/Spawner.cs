using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class Spawner : MonoBehaviour
    {

        public SteamVR_ActionSet actionSetEnable;
        public SteamVR_Action_Boolean spawnSphere;
        public GameObject gravitySphere;
        public GameObject transparentGravitySphere;
        public float planetSizeMax = 0.1f;

        private Hand hand;
        
        private Vector3 radiusStartPoint;
        private Vector3 radiusEndPoint;
        private float scaleFactor = 20f;

        private GameObject transparentEarth;

        private void OnEnable()
        {
            actionSetEnable.ActivateSecondary(true);
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (spawnSphere == null)
            {
                Debug.LogError("No spawn sphere action assigned");
                return;
            }

            spawnSphere.AddOnChangeListener(OnSpawnSphereChange, hand.handType);
            spawnSphere.AddOnUpdateListener(OnSpawnSphereUpdate, hand.handType);
        }

        private void OnDisable()
        {
            if (spawnSphere != null)
            {
                spawnSphere.RemoveOnChangeListener(OnSpawnSphereChange, hand.handType);
                spawnSphere.RemoveOnUpdateListener(OnSpawnSphereUpdate, hand.handType);
            }
        }

        private void OnSpawnSphereChange(SteamVR_Action_In actionIn)
        {
            if (spawnSphere.GetStateDown(hand.handType))
            {
                radiusStartPoint = transform.position;
                transparentEarth = Instantiate(transparentGravitySphere, radiusStartPoint, new Quaternion());
            }
            else if (spawnSphere.GetStateUp(hand.handType))
            {
                radiusEndPoint = transform.position;
                GameObject newGravityObject = Instantiate(gravitySphere, radiusStartPoint, new Quaternion());
                float gravityObjectRadius = Vector3.Distance(radiusStartPoint, radiusEndPoint);
                if (gravityObjectRadius > planetSizeMax)
                {
                    gravityObjectRadius = planetSizeMax;
                }
                float gravityObjectScale = gravityObjectRadius / scaleFactor;
                newGravityObject.transform.localScale = new Vector3(gravityObjectScale, gravityObjectScale, gravityObjectScale);
                newGravityObject.GetComponent<Rigidbody>().mass = gravityObjectRadius * scaleFactor;
                if (transparentEarth)
                {
                    Destroy(transparentEarth);
                }
            }
        }

        private void OnSpawnSphereUpdate(SteamVR_Action_In actionIn)
        {
            if (transparentEarth)
            {
                float gravityObjectRadius = Vector3.Distance(transparentEarth.transform.position, transform.position);
                if (gravityObjectRadius > planetSizeMax)
                {
                    gravityObjectRadius = planetSizeMax;
                }
                float gravityObjectScale = gravityObjectRadius / scaleFactor;
                transparentEarth.transform.localScale = new Vector3(gravityObjectScale, gravityObjectScale, gravityObjectScale);
            }
        }
    }
}