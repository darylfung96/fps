using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo_pickup : MonoBehaviour {

    public AudioSource ammoSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        global_ammo.clipAmmo += 10;
        this.gameObject.SetActive(false);
        ammoSound.Play();
    }

}
