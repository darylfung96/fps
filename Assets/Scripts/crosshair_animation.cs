using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshair_animation : MonoBehaviour {
    public GameObject upCrosshair;
    public GameObject downCrosshair;
    public GameObject rightCrosshair;
    public GameObject leftCrosshair;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            upCrosshair.GetComponent<Animation>().Play();
            downCrosshair.GetComponent<Animation>().Play();
            leftCrosshair.GetComponent<Animation>().Play();
            rightCrosshair.GetComponent<Animation>().Play();
        }

    }

}
