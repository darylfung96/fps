using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class global_ammo : MonoBehaviour {

    public static int currentAmmo;
    public int internalAmmo;
    public GameObject ammoDisplay; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        internalAmmo = currentAmmo;
        ammoDisplay.GetComponent<Text>().text = "" + internalAmmo;
	}
}
