using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateInactive : WizardState
{
    public override void ManageBattle()
    {
        
    }

    public override void ManageDeath()
    {
        
    }

    public override void ManageHealthRegen()
    {
        
    }

    public override void ManageIsInBattle()
    {
        
    }

    public override void ManageStateChange()
    {
        
    }

    public override void MoveWizard()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStateShowInConsole)
        {
            Debug.Log("État inactif");
        }
    }
}
