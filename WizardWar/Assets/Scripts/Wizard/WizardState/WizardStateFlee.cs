using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateFlee : WizardState
{
    private const float WIZARD_FLEE_SPEED = 3f;
    private const int WIZRAD_FLEE_HEALTH_REGEN = 1;
    private const float WIZARD_FLEE_REGEN_RATE = 4f;
    private GameObject placeToGo;
    // Start is called before the first frame update
    void Start()
    {
        speed = WIZARD_FLEE_SPEED;
        wizardRange = 0;
        isInBattle = false;
        wizardRateOfFire = 0;
        wizardDamage = 0;
        wizardHealthRegenNumber = WIZRAD_FLEE_HEALTH_REGEN;
        wizardHealthRegenRate = 0f;
        isInForest = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void MoveWizard()
    {
        DecideWhereToGo();
        transform.position = Vector3.MoveTowards(transform.position, placeToGo.transform.position, speed * Time.deltaTime);
    }

    private void DecideWhereToGo()
    {
        float shortestDistance = 0;
        foreach (GameObject tower in GameManager.instance.getTowerList(color))
        {
            float distance = Vector2.Distance(transform.position, tower.transform.position);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                placeToGo = tower;
            }
        }

        foreach (GameObject forest in GameManager.instance.getForestList(color))
        {
            float distance = Vector2.Distance(transform.position, forest.transform.position);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                placeToGo = forest;
            }
        }
    }

    public override void ManageDeath()
    {
        if (healthPoints.getHp() <= 0)
        {
            healthPoints.die();
            GameManager.instance.decreaseWizardNb(color);
        }
    }

    public override void ManageHealthRegen()
    {
        if (wizardHealthRegenRate >= WIZARD_FLEE_REGEN_RATE)
        {
            healthPoints.getHealed(wizardHealthRegenNumber);
            wizardHealthRegenRate = 0f;
        }
        else
        {
            wizardHealthRegenRate += Time.deltaTime;
        }
    }

    public override void ManageStateChange()
    {
        if(wizardManager.getForestInContact() != null)
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Hiding);
        }
        else if (wizardManager.getTowerInContact() != null)
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Safe);
        }
        else if(GameManager.instance.didSomeoneWin())
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Inactive);
        }
    }

    public override void ManageBattle()
    {
    }

    public override void ManageIsInBattle()
    {
    }
}
