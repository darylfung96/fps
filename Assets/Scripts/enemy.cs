using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public int health = 100;
    public GameObject player;
    public GameObject enemyAnimation;
    public string idleAnimation;
    public string walkAnimation;
    public string attackAnimation;
    public string dyingAnimation;
    public int enemyDamage = 1;
    public float speed;

    private bool isAttacking = false;
    private bool gone = false;

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

        if (gone)
            return;

        RaycastHit shot;

        //Vector3 playerDirection = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(player.transform);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
        {

            if (shot.distance > 2 && shot.distance <= 10)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);

                if (walkAnimation != "")
                    enemyAnimation.GetComponent<Animation>().Play(walkAnimation);
            }
            else if (shot.distance < 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*0.2f);
                if (attackAnimation != "")
                    enemyAnimation.transform.GetComponent<Animation>().Play(attackAnimation);

                if (!isAttacking && shot.transform.name == "FPSController")
                    StartCoroutine(attackPlayer());
            }
            else
            {
                if (idleAnimation != "")
                    enemyAnimation.transform.GetComponent<Animation>().Play(idleAnimation);
            }
        }



        if (health <= 0)
        {
            StartCoroutine(disappear());
        }
    }

    private IEnumerator attackPlayer()
    {
        isAttacking = true;
        player playerScript = (player)player.GetComponent(typeof(player));
        yield return new WaitForSeconds(1.6f);
        playerScript.decreaseHealth(enemyDamage);
        isAttacking = false;
    }

    private IEnumerator disappear()
    {
        gone = true;
        if (dyingAnimation != "")
            enemyAnimation.transform.GetComponent<Animation>().Play(dyingAnimation);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
