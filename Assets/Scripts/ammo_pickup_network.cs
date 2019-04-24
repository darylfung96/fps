using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ammo_pickup_network : NetworkBehaviour {

    public AudioSource ammoSound;
    public GameObject currentPlayer;
    public ammo currentPlayerAmmo;
    public int ammo;

	// Use this for initialization
	void Start () {
        currentPlayerAmmo = (ammo)currentPlayer.GetComponent(typeof(ammo));
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!isLocalPlayer) return;

        currentPlayerAmmo.clipAmmo += ammo;
        this.gameObject.SetActive(false);
        ammoSound.Play();
    }

}
