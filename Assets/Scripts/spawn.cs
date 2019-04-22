using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

    public GameObject enemy;
    public GameObject objectiveComplete;
    public AudioSource spawnAudio;
    public GameObject playerCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        enemy.SetActive(true);

        if (objectiveComplete != null)
            objectiveComplete.SetActive(true);

        if (spawnAudio != null)
            spawnAudio.Play();

        if (playerCamera != null)
        {
            playerCamera.GetComponent<Animator>().enabled = true;
            StartCoroutine(disableAnimator());
        }
        StartCoroutine(destroySelf());
    }

    private IEnumerator disableAnimator()
    {
        yield return new WaitForSeconds(0.3f);
        playerCamera.GetComponent<Animator>().enabled = false;
    }

    IEnumerator destroySelf()
    {
        yield return new WaitForSeconds(1);
        playerCamera.GetComponent<Animator>().enabled = false;
        Destroy(gameObject);
    }
}
