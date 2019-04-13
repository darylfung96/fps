using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_fire : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
        {
            AudioSource gunShot = GetComponent<AudioSource>();
            gunShot.Play();
            GetComponent<Animation>().Play();
        }
    }
}
