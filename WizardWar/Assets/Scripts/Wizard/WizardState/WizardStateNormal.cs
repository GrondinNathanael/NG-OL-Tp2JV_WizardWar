using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{


    // Start is called before the first frame update
    void Start()
    {
        if (transform.tag == "WizardBlue")
        {
            towerList = greenTowerList;
            wizardColor = WizardColors.BLUE;
            ennemyList = greenWizardList;
        }
        else
        {
            towerList = blueTowerList;
            wizardColor = WizardColors.GREEN;
            ennemyList = blueWizardList;
        }
        speed = 2f;
        wizardRange = 2f;
        isInBattle = false;
    }

    // Update is called once per frame
    void Update()
    {
        ManageIsInBattle();
        MoveWizard();
        ManageBattle();
        ManageStateChange();
    }

    public override void ManageStateChange()
    {

    }

    public override void MoveWizard()
    {


        if (isInBattle)
        {
            return;
        }

        for (int i = 0; i < towerList.Length; i++)
        {
            if (towerList[i].activeSelf)
            {
                transform.position = Vector3.MoveTowards(transform.position, towerList[i].transform.position, speed * Time.deltaTime);
                return;
            }
        }
    }

    private void ManageIsInBattle()
    {
        for (int i = 0; i < ennemyList.Length; i++)
        {
            if (Vector2.Distance(transform.position, ennemyList[i].transform.position) < wizardRange && ennemyList[i].activeSelf)
            {
                isInBattle = true;
                return;
            }
            else
            {
                isInBattle = false;
            }
        }
    }

    public override void ManageBattle()
    {
        if (isInBattle)
        {

        }
    }
}