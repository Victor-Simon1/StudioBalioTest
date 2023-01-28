using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.UI;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    private Transform transformPlayer;
    private Animator anim;
    private GameObject UIObj;
    private TextMeshProUGUI text;
    private int nbBullets;
    [SerializeField]
    private GameObject prefabBullet;
    [SerializeField]
    private float speed;
    float input;
    [SerializeField]
    GameObject shootStart;//place where shoot begin to move and apppears

    // Start is called before the first frame update
    void Start()
    {
        //GameObject
        transformPlayer = transform;
        UIObj = GameObject.Find("UI");
        text = UIObj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
        //Var
        nbBullets = 0;
        speed = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(nbBullets>0)
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    GameObject go = Instantiate(prefabBullet,this.transform.parent);
                    go.GetComponent<BulletBehavior>().InitPosition(shootStart.transform.position);
                    go.GetComponent<BulletBehavior>().InitTarget(hit.point);
        
                    nbBullets--;
                    text.text = "Bullets : " +nbBullets;
                    Debug.Log(hit.point);
                }
               
            }
        }
        
    }
    private void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        movement.Normalize();

        transform.Translate(movement*speed*Time.deltaTime,Space.World);
        if(movement != Vector3.zero)
        {
            anim.SetBool("IsRunning",true);
            Quaternion rotation = Quaternion.LookRotation(movement,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,180*Time.deltaTime );
        }
        else anim.SetBool("IsRunning",false);
        Shoot();
    }

    //properties
    public int NbBullets
    {
        get{return nbBullets;}
        set{nbBullets = value;}
    }
}
