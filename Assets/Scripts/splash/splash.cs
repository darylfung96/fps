using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(loadMenu());
	}

    IEnumerator loadMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
}
}
