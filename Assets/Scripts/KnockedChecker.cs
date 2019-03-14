using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedChecker : MonoBehaviour {

    private float rayDistance = 0.4f;
    private bool knockedDown;

	void Update () {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Pin Detector");
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, rayDistance, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            knockedDown = false;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * rayDistance, Color.red);
            knockedDown = true;
        }
	}

    public bool getKnockedDown()
    {
        return knockedDown;
    }
}
