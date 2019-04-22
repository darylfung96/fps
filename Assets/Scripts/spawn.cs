using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

    public GameObject enemy;
    public GameObject objectiveComplete;
    public GameObject showNextObjective;
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

        if (showNextObjective != null)
            showNextObjective.SetActive(true);

        if (spawnAudio != null)
            spawnAudio.Play();

        if (playerCamera != null)
        {
            playerCamera.GetComponent<Animation>().Play("look_up");
        }
        StartCoroutine(destroySelf());
    }

    IEnumerator destroySelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
