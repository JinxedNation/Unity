using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public Transform holdPos;
    //public Color startcolor;

    public void PickUpItem()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = holdPos.position;
        this.transform.parent = GameObject.Find("HoldPosition").transform;
    }

    // Update is called once per frame
    public void DropItem()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;

    }
}
