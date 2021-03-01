using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTree : MonoBehaviour
{
    public GameObject treeStart;
    public GameObject treeFinish;
    //public Transform treeFinish;
    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //treeStart = new Vector3(-137f, 39.2f, 30.21f);
            treeStart.transform.position = new Vector3(-137f, 31f, 37f);
            Debug.Log("Player Entered arena");
            treeFinish.transform.position = new Vector3(-137f, 31f, 48f);
        }

    }
}
