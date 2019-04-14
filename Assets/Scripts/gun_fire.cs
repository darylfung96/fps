using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_fire : MonoBehaviour {

    public AudioSource gunFireSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
        {
            gunFireSound.Play();
            GetComponent<Animation>().Play();
            global_ammo.currentAmmo -= 1;
        }
    }
}
