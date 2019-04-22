using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_anim : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("space"))
        {
            StartCoroutine(jump());
        }

        if (Input.GetKey("w") || Input.GetKey("s")){
            anim.SetBool("isWalking", true);
            transform.localPosition = new Vector3(0, -1.7f, -1.8f);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

    }

    private IEnumerator jump()
    {
        anim.SetBool("OnGround", false);
        yield return new WaitForSeconds(1);
        anim.SetBool("OnGround", true);
    }
}
