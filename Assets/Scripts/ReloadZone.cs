using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ReloadZone : MonoBehaviour
{
    bool isFull;
    float reloadTime;
    float reloadTimeMax;
    int nbBulletsReload;
    TextMeshProUGUI text;
    [SerializeField]
    TextMeshProUGUI state;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("UI").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        isFull = true;
        reloadTime = 0f;
        reloadTimeMax = 18f;
        nbBulletsReload = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(reloadTime >0f)
        {
            reloadTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" && isFull)
        {
            Debug.Log("Reload");
            other.GetComponent<PlayerMovement>().NbBullets += nbBulletsReload;
            text.text = "Bullets : " +other.GetComponent<PlayerMovement>().NbBullets;
            state.text = gameObject.name + ": Vide";
            isFull = false;
            StartCoroutine(LoadZone(reloadTimeMax));
        }
    }

    IEnumerator LoadZone(float waitTime)
    {
        //Material[] materials = GetComponent<MeshRenderer>().materials;
      //  materials[0] = redMat;
        //materials[0].color = Color.red;
        //GetComponent<MeshRenderer>().materials = redMat;
        yield return new WaitForSeconds(waitTime);
        //GetComponent<Renderer>().materials[0] = redMat;
        Debug.Log("isReload");
        isFull = true;
        state.text =  gameObject.name + " : Full";
    }
}
