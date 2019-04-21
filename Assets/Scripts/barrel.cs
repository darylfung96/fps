using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour {

    public GameObject realBarrel;
    public GameObject fakeBarrel;
    public AudioSource explosionSound;
    public int health = 1;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
        {
            realBarrel.SetActive(false);
            fakeBarrel.SetActive(true);
            //Destroy(realBarrel);
        }
    }

    // items should contain health in first position, direction in second position
    void getShot(object[] items)
    {
        int decreaseHealth = (int)items[0];

        health -= decreaseHealth;

    }

}
