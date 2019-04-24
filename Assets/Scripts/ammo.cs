using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammo : MonoBehaviour {

    public int currentAmmo;
    public GameObject ammoDisplay;

    public int clipAmmo;
    public GameObject loadedAmmoDisplay;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ammoDisplay.GetComponent<Text>().text = "" + currentAmmo;
        loadedAmmoDisplay.GetComponent<Text>().text = "" + clipAmmo;
    }
}
