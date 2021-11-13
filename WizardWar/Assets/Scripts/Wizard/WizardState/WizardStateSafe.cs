using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateSafe : WizardState
{
    private const int WIZRAD_SAFE_HEALTH_REGEN = 3;
    private const float WIZARD_SAFE_REGEN_RATE = 1f;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        wizardRange = 0;
        isInBattle = false;
        wizardRateOfFire = 0;
        wizardDamage = 0;
        wizardHealthRegenNumber = WIZRAD_SAFE_HEALTH_REGEN;
        wizardHealthRegenRate = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStateShowInConsole)
        {
            Debug.Log("État en sureté");
        }
    }

    public override void MoveWizard()
    {
    }

    public override void ManageIsInBattle()
    {
    }

    public override void ManageBattle()
    {
    }

    public override void ManageDeath()
    {
    }

    public override void ManageHealthRegen()
    {
        if (!isInBattle)
        {
            if (wizardHealthRegenRate >= WIZARD_SAFE_REGEN_RATE)
            {
                healthPoints.getHealed(wizardHealthRegenNumber);
                wizardHealthRegenRate = 0f;
            }
            else
            {
                wizardHealthRegenRate += Time.deltaTime;
            }
        }
    }

    public override void ManageStateChange()
    {
        if (healthPoints.getHp() == healthPoints.getMaxHp())
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Normal);
        }
        else if (wizardManager.getTowerInContact() == null)
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Normal);
        }
        else if (GameManager.instance.didSomeoneWin())
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Inactive);
        }
    }
}
