using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cut_scene_one : MonoBehaviour {

    public GameObject player;
    public GameObject ui;

	// Use this for initialization
	void Start () {
        StartCoroutine(startCutScene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator startCutScene()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        player.SetActive(true);
        ui.SetActive(true);
    }
}
