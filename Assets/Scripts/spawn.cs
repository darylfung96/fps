using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

    public GameObject enemy;
    public GameObject objectiveComplete;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        enemy.SetActive(true);
        objectiveComplete.SetActive(true);
    }
}
