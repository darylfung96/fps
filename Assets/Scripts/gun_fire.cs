using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class gun_fire : MonoBehaviour {

    public Camera camera;
    public GameObject currentPlayer;
    public ammo currentPlayerAmmo;

    public GameObject upCrosshair;
    public GameObject downCrosshair;
    public GameObject rightCrosshair;
    public GameObject leftCrosshair;

    public GameObject flash;
    public int maxClip;

    public AudioSource gunFireSound;
    public AudioSource reloadSound;

    public Boolean isReloading = false;
    public Boolean isRapidFire = false;
    public Boolean isFiring = false;

    // use to damage
    public int damage = 5;
    public float targetDistance;
    public float allowedRange = 15;

    // bullet hole
    public GameObject bulletHole;

    // blood
    public GameObject blood;
    public GameObject spiderBlood;

    // Use this for initialization
    void Start () {
        currentPlayerAmmo = (ammo)currentPlayerAmmo.GetComponent (typeof(ammo));
	}
	
	// Update is called once per frame
	void Update () {

        if (isRapidFire)
        {
            rapidFireShot();
        }
        else
        {
            singleFireShot();
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (currentPlayerAmmo.clipAmmo > 0 && currentPlayerAmmo.currentAmmo < maxClip)
            {
                isReloading = true;
                GetComponent<Animation>().Play("gun_reload");
                reloadSound.Play();

                int reloadAmount = Math.Max(0, Math.Min(maxClip, currentPlayerAmmo.clipAmmo));
                currentPlayerAmmo.currentAmmo += reloadAmount;
                currentPlayerAmmo.clipAmmo -= reloadAmount;
                StartCoroutine(reload());
            }
        }
    }

    private void singleFireShot()
    {
        if (Input.GetButtonDown("Fire1") && currentPlayerAmmo.currentAmmo > 0 && !isReloading)
        {
            gunFireSound.Play();
            GetComponent<Animation>().Play("gun_shot");
            currentPlayerAmmo.currentAmmo -= 1;

            upCrosshair.GetComponent<Animation>().Play();
            downCrosshair.GetComponent<Animation>().Play();
            leftCrosshair.GetComponent<Animation>().Play();
            rightCrosshair.GetComponent<Animation>().Play();
            flash.SetActive(true);

            callShot();

            StartCoroutine(startFlash(0.1f));
        }
    }

    private void rapidFireShot()
    {
        if (Input.GetButton("Fire1") && currentPlayerAmmo.currentAmmo > 0 && !isReloading && !isFiring)
        {
            isFiring = true;
            gunFireSound.Play();
            GetComponent<Animation>().Play("gun_shot");
            currentPlayerAmmo.currentAmmo -= 1;

            upCrosshair.GetComponent<Animation>().Play();
            downCrosshair.GetComponent<Animation>().Play();
            leftCrosshair.GetComponent<Animation>().Play();
            rightCrosshair.GetComponent<Animation>().Play();
            flash.SetActive(true);

            callShot();

            StartCoroutine(startFlash(0.1f));
        }
    }


    IEnumerator startFlash(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        flash.SetActive(false);
        isFiring = false;
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(1);
        isReloading = false;
    }

    private void callShot()
    {
        RaycastHit shot;

        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out shot))
        {
            targetDistance = shot.distance;
            if (shot.distance < allowedRange)
            {
                Vector3 fwd = transform.TransformDirection(Vector3.forward);
                object[] items = { damage, transform.TransformDirection(Vector3.forward) };
                shot.transform.SendMessage("getShot", items, SendMessageOptions.DontRequireReceiver);

                if (shot.transform.tag == "zombie" || shot.transform.tag == "wolf")
                {
                    Instantiate(blood, shot.point, Quaternion.FromToRotation(Vector3.up, shot.normal));
                }
                else if (shot.transform.tag == "spider")
                {
                    Instantiate(spiderBlood, shot.point, Quaternion.FromToRotation(Vector3.up, shot.normal));
                }
                else
                {
                    Instantiate(bulletHole, shot.point, Quaternion.FromToRotation(Vector3.up, shot.normal));
                }
            }
        }
    }
}
