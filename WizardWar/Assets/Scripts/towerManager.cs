using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerManager : MonoBehaviour
{
    private HealthPoints healthPoints;
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = GetComponent<HealthPoints>();   
    }

    // Update is called once per frame
    void Update()
    {
        manageDeath();
    }

    private void manageDeath() 
    {
        if (healthPoints.hp <= 0) 
        {
            healthPoints.die();
        }
    }
}
