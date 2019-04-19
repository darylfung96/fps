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
    private bool isMoving = false;

	// Use this for initialization
	void Start () {
		
	}
	
    // items should contain health in first position, direction in second position
    void getShot(object[] items)
    {
        int decreaseHealth = (int)items[0];

        health -= decreaseHealth;

    }

    // Update is called once per frame
    void Update () {

        if (gone)
            return;

        RaycastHit shot;

        // when player get to a distance that the enemy can see or hear
        if (Vector3.Distance(transform.position, player.transform.position) < 15)
            transform.LookAt(player.transform);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
        {
            // chase players
            if (shot.distance > 2 && shot.distance <= 20 && shot.transform.tag == "player")
            {
                isMoving = false;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                if (walkAnimation != "")
                    enemyAnimation.GetComponent<Animation>().Play(walkAnimation);
            }
            // attack
            else if (shot.distance < 2 && shot.transform.tag == "player")
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*0.2f * Time.deltaTime);
                if (attackAnimation != "")
                    enemyAnimation.transform.GetComponent<Animation>().Play(attackAnimation);

                if (!isAttacking && shot.transform.name == "FPSController")
                    StartCoroutine(attackPlayer());
            }
            // idle or walk randomly
            else
            {
                // if the previous animation is not done yet, we dont trigger anything 
                if (isMoving) return;

                float idleProb = Random.value;
                if (idleProb > 0.5)
                {
                    if (idleAnimation != "")
                        enemyAnimation.transform.GetComponent<Animation>().Play(idleAnimation);
                }
                else
                {
                        StartCoroutine(moveToPosition());
                }

            }
        }



        if (health <= 0)
        {
            StartCoroutine(disappear());
        }
    }

    private Vector3 getRandomPosition()
    {
        Vector3 moveDelta = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
        Vector3 nextPosition = transform.position + moveDelta;
        return nextPosition;
    }

    private IEnumerator moveToPosition()
    {
        isMoving = true;
        Vector3 newPosition = getRandomPosition();
        if (walkAnimation != "")
            enemyAnimation.GetComponent<Animation>().Play(walkAnimation);
        transform.LookAt(newPosition);
        float elapsedTime = 0;
        // start moving
        while (transform.position != newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            yield return new WaitForSeconds(0.03f);

            elapsedTime += Time.deltaTime;
            if (elapsedTime > 3) break;
        }

        if (idleAnimation != "")
            enemyAnimation.GetComponent<Animation>().Play(idleAnimation);
        yield return new WaitForSeconds(2f);
        isMoving = false;
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
