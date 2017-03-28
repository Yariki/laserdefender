using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{

    public float health = 150f;
    public float speedLaser = 10f;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;
    public AudioClip fireSound;
    public AudioClip deathSound;

    public GameObject laser;

    private ScoreKeeperController scoreKeeper;

	// Use this for initialization
	void Start ()
    {
        scoreKeeper =  GameObject.Find("Score").GetComponent<ScoreKeeperController>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        float probability = shotsPerSecond * Time.deltaTime;
        if(Random.value < probability)
        {
            Fire();
        }
    }

    private void Fire()
    {
        Vector3 startPos = transform.position + new Vector3(0, -1f, 0);
        GameObject enemyLaser = Instantiate(laser, startPos, Quaternion.identity) as GameObject;
        enemyLaser.rigidbody2D.velocity = new Vector2(0f, -speedLaser);
        if (fireSound != null)
        {
            AudioSource.PlayClipAtPoint(fireSound,transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        LaserController laserController = coll.gameObject.GetComponent<LaserController>();
        if(laserController != null)
        {
            health -= laserController.GetDamage();
            if(health <= 0)
            {
                if (deathSound != null)
                {
                    AudioSource.PlayClipAtPoint(deathSound,transform.position);
                }
                
                Destroy(gameObject);
                if (scoreKeeper != null)
                {
                    scoreKeeper.ScorePoints(scoreValue);
                }
            }
            laserController.Hit();
        }
    }
}
