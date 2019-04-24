using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public int health = 100;
    public GameObject enemyAnimation;
    public string idleAnimation;
    public string walkAnimation;
    public string attackAnimation;
    public string dyingAnimation;

    public int sightDistance = 20;
    public int attackDistance = 2;
    public bool isWalkAround = true;

    public bool hasSurprised = false;
    public AudioSource surprise;

    public int enemyDamage = 1;
    public float speed;

    private bool isAttacking = false;
    private bool gone = false;
    private bool isMoving = false;
    private bool playerInSight = false;

	// Use this for initialization
	void Start () {
		
	}
	
    // items should contain health in first position, direction in second position
    void getShot(object[] items)
    {
        int decreaseHealth = (int)items[0];

        health -= decreaseHealth;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "player")
        {
            playerInSight = true;
            transform.LookAt(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "player")
            playerInSight = false;
    }

    private void OnTriggerStay(Collider other)
    {
        RaycastHit shot;
        if (other.transform.tag == "player")
        {
            transform.LookAt(other.transform);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot)) {
                if (shot.distance > attackDistance && shot.distance <= sightDistance && shot.transform.tag == "player")
                {
                    isMoving = false;
                    transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed * Time.deltaTime);

                    if (walkAnimation != "")
                        enemyAnimation.GetComponent<Animation>().Play(walkAnimation);

                    if (!hasSurprised)
                    {
                        hasSurprised = true;
                        surprise.Play();
                    }
                }
                // attack
                else if (shot.distance < attackDistance && shot.transform.tag == "player")
                {
                    transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed * 0.5f * Time.deltaTime);
                    if (attackAnimation != "")
                        enemyAnimation.transform.GetComponent<Animation>().Play(attackAnimation);

                    if (!isAttacking)
                        StartCoroutine(attackPlayer(other));
                }
            }

        }
    }

    // Update is called once per frame
    void Update () {

        if (gone)
            return;

        if (health <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(disappear());
        }

        // if the previous animation is not done yet, we dont trigger anything 
        // if playerinsight we want the zombie to run in trigger functions
        if (playerInSight || isMoving) return;

        float idleProb = Random.value;
        if (idleProb > 0.5)
        {
            if (idleAnimation != "")
                enemyAnimation.transform.GetComponent<Animation>().Play(idleAnimation);
        }
        else
        {
            if (isWalkAround)
                StartCoroutine(moveToPosition());
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

    private IEnumerator attackPlayer(Collider other)
    {
        isAttacking = true;
        Debug.Log(other);
        player_network playerScript = (player_network)other.gameObject.GetComponent(typeof(player_network));
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
