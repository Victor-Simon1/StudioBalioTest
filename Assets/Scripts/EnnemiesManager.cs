using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesManager : MonoBehaviour
{

    [SerializeField]
    private int nbEnnemiesMax;
    [SerializeField]
    private int nbEnnemies;
    [SerializeField]
    private GameObject prefabEnnemi;
    private bool isRunning;
    // Start is called before the first frame update
    void Start()
    {
        nbEnnemiesMax = 10;
        StartCoroutine(InitEnnemies());

    }

    // Update is called once per frame
    void Update()
    {
        if(!isRunning)
        {
            nbEnnemies = transform.childCount;
            if(nbEnnemies<2)
            {
                createEnnemies();
            }
            nbEnnemies = transform.childCount;
            if(nbEnnemies < nbEnnemiesMax)
            {
                float randSpawn = Random.Range(0f,100f);
                if(randSpawn<5f)
                {
                    createEnnemies();
                }
            }
        }
    }

    private IEnumerator InitEnnemies()
    {
        isRunning = true;
        for(int i= 0;i<nbEnnemiesMax;i++)
        {
            createEnnemies();
            yield return new WaitForSeconds(1f);
        }
        isRunning = false;
    }

    private void createEnnemies()
    {
        Instantiate(prefabEnnemi,this.transform);
    }
}
