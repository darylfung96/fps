using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class gun_fire : MonoBehaviour {

    public GameObject upCrosshair;
    public GameObject downCrosshair;
    public GameObject rightCrosshair;
    public GameObject leftCrosshair;

    public GameObject flash;

    public AudioSource gunFireSound;
    public AudioSource reloadSound;
    public int maxClip = 7;
    public Boolean isReloading = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { 
		if (Input.GetButtonDown("Fire1") && global_ammo.currentAmmo > 0 && !isReloading)
        {
            gunFireSound.Play();
            GetComponent<Animation>().Play("gun_shot");
            global_ammo.currentAmmo -= 1;

            upCrosshair.GetComponent<Animation>().Play();
            downCrosshair.GetComponent<Animation>().Play();
            leftCrosshair.GetComponent<Animation>().Play();
            rightCrosshair.GetComponent<Animation>().Play();
            flash.SetActive(true);
            StartCoroutine(startFlash());
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (global_ammo.clipAmmo > 0 && global_ammo.currentAmmo < maxClip)
            {
                isReloading = true;
                GetComponent<Animation>().Play("gun_reload");
                reloadSound.Play();

                int reloadAmount = Math.Max(0, Math.Min(maxClip, global_ammo.clipAmmo));
                global_ammo.currentAmmo += reloadAmount;
                global_ammo.clipAmmo -= reloadAmount;
                StartCoroutine(reload());
            }
        }

    }

    IEnumerator startFlash()
    {
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(1);
        isReloading = false;
    }
}
