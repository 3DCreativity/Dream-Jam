using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawn : MonoBehaviour
{
    public GameObject Menu1;
    public GameObject Menu2;
    public GameObject Menu3;
    int num;
    bool spawned = false;
    void Awake()
    {
        num = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned == false)
        {
            if (num == 1)
            {
                Instantiate(Menu1,this.transform);
            }
            if (num == 2)
            {
                Instantiate(Menu2,this.transform);
            }
            if (num == 3)
            {
                Instantiate(Menu3,this.transform);
            }
            spawned = true;
        }
    }
}
