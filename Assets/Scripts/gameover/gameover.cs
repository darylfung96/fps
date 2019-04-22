using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour {

    private bool loaded = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!loaded)
        {
            loaded = true;
            StartCoroutine(respawn());
        }
	}

    private IEnumerator respawn()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);

    }
}
