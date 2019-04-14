using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo_pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        global_ammo.currentAmmo += 10;
        this.gameObject.SetActive(false);
    }
}
