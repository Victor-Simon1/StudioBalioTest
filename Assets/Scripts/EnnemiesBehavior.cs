using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesBehavior : MonoBehaviour
{

    private Transform transformEnnemi;
    private float speed;
    private Vector3 velocity;
    private float force;
    private float mass;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        transformEnnemi = transform;
        transformEnnemi.position = new Vector3(Random.Range(-10f,10f),5f,Random.Range(-10f,10f));
        speed = 3f;
        force = 2f;
        mass = GetComponent<Rigidbody>().mass;
        velocity = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        if(transformEnnemi.position.y < -10f)
        {
            Respawn();
        }
        Move();
    }
    Vector3 Truncate(Vector3 v, float m)
    {
        if (v.sqrMagnitude == 0)return v;
        return (m * v) / v.sqrMagnitude;
    }
    private void Move()
    {
        if( Vector3.Distance(target,transform.position)<5f)
        {
            target = new Vector3(Random.Range(-15f,15f),2f,Random.Range(-15f,15f));
        }
        Vector3 temp = target - transform.position;

	    Vector3 desired_velocity = temp * speed;
	    Vector3 steering = desired_velocity - velocity;
        Vector3 steering_force = Truncate(steering,force);
	    Vector3 acceleration = steering_force / mass;

	    velocity = Truncate(velocity+acceleration, speed);
        transformEnnemi.position += velocity *Time.deltaTime;

    }
    private void Respawn()
    {
        transformEnnemi.position = new Vector3(0f,0f,0f);
    }
}
