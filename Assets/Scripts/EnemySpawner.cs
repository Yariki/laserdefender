using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
    public float spawnDelay = 0.5f;

    private bool movingRight = false;
	private float minX;
	private float maxX;
    
    // Use this for initialization
    void Start ()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        var leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        var rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        maxX = rightEdge.x;
        minX = leftEdge.x;

        SpawnUntilFull(); 
    }

    private void CreateNewFormation()
    {
        foreach (Transform child in transform)
        {
            var enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition)
        {
            var enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }

    }

    void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position,new Vector3(width,height));
	}
	
	
	
	// Update is called once per frame
	void Update () 
    {
		if(movingRight)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}	
		else
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		float rightEdgeOFFormation = transform.position.x + (width / 2);
		float leftEdgeOFFOrmation = transform.position.x - (width / 2);
		
		if(leftEdgeOFFOrmation < minX)
		{
			movingRight = true;
		}
		else if(rightEdgeOFFormation > maxX)
		{
			movingRight = false;
		}

        if (AllMembersDead())
        {
            SpawnUntilFull();
        }
	}

    private Transform NextFreePosition()
    {
        foreach (Transform pos in transform)
        {
            if (pos.childCount == 0)
            {
                return pos;
            }
        }
        return null;
    }

    private bool AllMembersDead()
    {
        
        foreach(Transform pos in transform)
        {
            if(pos.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}
