using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleter : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GravityObject")
        {
            Destroy(other.gameObject);
        }
    }
}
