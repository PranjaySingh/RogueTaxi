using UnityEngine;
using System.Collections;

public class Chaser : MonoBehaviour {
	
	public float speed = 20.0f;
	public float minDist = 1f;
	public Transform target;
    public ParticleSystem copParticleSystem;
	
	void Start () 
	{
		//if no target, assume the player
		if (target == null)
		{
			if (GameObject.FindWithTag ("Player")!=null)
			{
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
	}
	
	void Update () 
	{
		if (target == null)
			return;

		//look at target
		transform.LookAt(target);

		//distance between the chaser and the target
		float distance = Vector3.Distance(transform.position,target.position);

		//if(distance > minDist)	
			transform.position += transform.forward * speed * Time.deltaTime;	
	}

	// Set the target of the chaser
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag!= "Player")
        {
			Destroy(gameObject);
			if(copParticleSystem != null)
            {
				Vector3 particlePosition = new Vector3(transform.position.x, 1, transform.position.z);
				Instantiate(copParticleSystem, particlePosition, Quaternion.identity);
			}
			
		}
    }
}
