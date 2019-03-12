using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : GravityObject {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GravityObject")
        {
            Destroy(collision.gameObject);
        }
    }
}
