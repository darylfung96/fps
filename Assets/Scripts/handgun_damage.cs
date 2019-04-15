using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handgun_damage : MonoBehaviour {
    public int damage = 5;
    public float targetDistance;
    public float allowedRange = 15;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1") && global_ammo.currentAmmo > 0)
        {
            RaycastHit shot;
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
            {
                targetDistance = shot.distance;
                if (shot.distance < allowedRange)
                {
                    object[] items = {damage, transform.TransformDirection(Vector3.forward)};
                    shot.transform.SendMessage("getShot", items, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
