using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float speed;
    [SerializeField]
    private Vector3 target;
    private Vector3 direction;
    
    private AudioSource shootSound;
    [SerializeField]
    private AudioClip shootClip;
    // Start is called before the first frame update
    void Start()
    {
        direction = (target - transform.position);
        direction.Normalize();
        speed = 10f;
        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void InitPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public void InitTarget(Vector3 pos)
    {
        target = new Vector3(pos.x,0.5f,pos.z);
        float angle = Vector3.Angle(target, transform.position); 
        transform.Rotate(0f,angle,0f,Space.World);
    }
    Vector3 Truncate(Vector3 v, float m)
    {
        if (v.sqrMagnitude == 0)return v;
        return (m * v) / v.sqrMagnitude;
    }
    private void Move()
    { 
        transform.position += direction *speed*Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag != "Player")
        {
            if(other.tag == "Ennemi")
            {
                Destroy(other.gameObject);
            }
            Debug.Log("Tir Détruit");
            Destroy(gameObject);
        }
       
    }
}
