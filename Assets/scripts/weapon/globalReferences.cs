using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalReferences : MonoBehaviour
{
    // Start is called before the first frame update


    public static globalReferences instance { get; set;}
    public GameObject bulletImpactEffectPrefab;
    private void Awake()
    {
        if(instance!=null&& instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
