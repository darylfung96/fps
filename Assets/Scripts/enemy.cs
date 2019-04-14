using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public int health = 100;

	// Use this for initialization
	void Start () {
		
	}
	
    // items should contain health in first position, direction in second position
    void getShot(object[] items)
    {
        int decreaseHealth = (int)items[0];
        Vector3 direction = (Vector3)items[1];

        health -= decreaseHealth;
        direction.y = 0;

        transform.Translate((Vector3)direction * Time.deltaTime * 10);
    }

    // Update is called once per frame
    void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
