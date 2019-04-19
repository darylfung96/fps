using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel_bang : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 1)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
