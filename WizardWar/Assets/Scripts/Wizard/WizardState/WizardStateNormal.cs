using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{


    // Start is called before the first frame update
    void Start()
    {
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

        for (int i = 0; i < GameManager.instance.getTowerList(ennemyColor).Count; i++)
        {

            transform.position = Vector3.MoveTowards(transform.position, GameManager.instance.getTowerList(ennemyColor)[i].transform.position, speed * Time.deltaTime);
            return;

        }
    }

    private void ManageIsInBattle()
    {
        for (int i = 0; i < GameManager.instance.getEnnemyList(ennemyColor).Length; i++)
        {
            if (Vector2.Distance(transform.position, GameManager.instance.getEnnemyList(ennemyColor)[i].transform.position) < wizardRange && GameManager.instance.getEnnemyList(ennemyColor)[i].activeSelf)
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