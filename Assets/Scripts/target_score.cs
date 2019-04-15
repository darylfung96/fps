using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class target_score : MonoBehaviour {

    public GameObject scoreDisplay;
    public int score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void getShot(object[] items)
    {
        scoreDisplay.GetComponent<TextMesh>().text = "" + score;
        StartCoroutine(wait());

    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        scoreDisplay.GetComponent<TextMesh>().text = "";
    }

}
