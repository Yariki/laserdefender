using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed = 15f;
    public float padding = 1f;
	public float projectialeSpeed;
	public float firingRate = 0.2f;
    public float health = 1000f;
    public AudioClip fireSound;
    
    public GameObject laser;

    private float xmin;
    private float xmax;

	// Use this for initialization
	void Start ()
	{
	    float distance = transform.position.z - Camera.main.transform.position.z;
	    Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
	    xmin = leftMost.x + padding;
	    xmax = rightMost.x - padding;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire",0.00001f,firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}
    
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX,transform.position.y, transform.position.z);
    }
    
    void Fire()
    {
        Vector3 offset = transform.position + new Vector3(0, 1f, 0);

		GameObject beam = Instantiate(laser,offset,Quaternion.identity) as GameObject;			
		beam.rigidbody2D.velocity = new Vector3(0,projectialeSpeed);
        if (fireSound != null)
        {
            AudioSource.PlayClipAtPoint(fireSound, transform.position,1.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        LaserController laserController = coll.gameObject.GetComponent<LaserController>();
        if (laserController != null)
        {
            health -= laserController.GetDamage();
            if (health <= 0)
            {
                LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                man.LoadLevel("Win Screen");
                Destroy(gameObject);
            }
            laserController.Hit();
        }
    }

}
