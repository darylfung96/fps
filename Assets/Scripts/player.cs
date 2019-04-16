using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {

    public int health = 10;
    public GameObject healthDisplay;

    public GameObject showHurt;
    public List<AudioSource> hurtSounds;

    private int hurtSoundsCount;

	// Use this for initialization
	void Start () {
        hurtSoundsCount = hurtSounds.Count;
	}
	
	// Update is called once per frame
	void Update () {
        healthDisplay.GetComponent<Text>().text = "health: " + health;

		if (health <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void decreaseHealth(int damage)
    {
        health -= damage;
        StartCoroutine(flashScreen());

        // play hurt sound
        int index = Random.Range(1, hurtSoundsCount);
        hurtSounds[index].Play();
    }

    IEnumerator flashScreen()
    {
        showHurt.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        showHurt.SetActive(false);
    }
}
