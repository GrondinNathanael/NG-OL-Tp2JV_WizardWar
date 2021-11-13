using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateHiding : WizardState
{
    private const float hpPercentToReturnToFlee = 0.25f;
    private const float hpPercentToReturnToNormal = 0.5f;

    private const float WIZARD_HIDING_RANGE = 2F;
    private const float WIZARD_HIDING_RATE_OF_FIRE = 3f;
    private const int WIZARD_MAX_ATTACK = 5;
    private const int WIZARD_MIN_ATTACK = 1;
    private const int WIZRAD_HIDING_HEALTH_REGEN = 3;
    private const float WIZARD_HIDING_REGEN_RATE = 1f;
    private const float WIZARD_DAMAGE_REDUCTION = 0.80f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        wizardRange = WIZARD_HIDING_RANGE;
        isInBattle = false;
        wizardRateOfFire = WIZARD_HIDING_RATE_OF_FIRE;
        wizardDamage = Random.Range(WIZARD_MIN_ATTACK, WIZARD_MAX_ATTACK);
        wizardHealthRegenNumber = WIZRAD_HIDING_HEALTH_REGEN;
        wizardHealthRegenRate = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ManageIsInBattle();
        ManageBattle();
        ManageDeath();
        ManageHealthRegen();
        ManageStateChange();
    }

    public override void MoveWizard()
    {
    }

    public override void ManageIsInBattle()
    {
        for (int i = 0; i < GameManager.instance.getEnnemyList(ennemyColor).Length; i++)
        {

            if (Vector2.Distance(transform.position, GameManager.instance.getEnnemyList(ennemyColor)[i].transform.position) < wizardRange && GameManager.instance.getEnnemyList(ennemyColor)[i].activeSelf)
            {
                isInBattle = true;
                wizardTarget = GameManager.instance.getEnnemyList(ennemyColor)[i];
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
            if (wizardRateOfFire >= WIZARD_HIDING_RATE_OF_FIRE)
            {
                if(wizardTarget.GetComponent<WizardManager>().getForestInContact() != null)
                    wizardTarget.GetComponent<HealthPoints>().getDamaged(wizardDamage);
                else
                    wizardTarget.GetComponent<HealthPoints>().getDamaged(wizardDamage * WIZARD_DAMAGE_REDUCTION);
                wizardRateOfFire = 0f;
            }
            else
            {
                wizardRateOfFire += Time.deltaTime;
            }
        }
        else if (wizardTarget == null)
        {
            isInBattle = false;
        }
    }

    public override void ManageDeath()
    {
        if (healthPoints.getHp() <= 0)
        {
            healthPoints.die();
            GameManager.instance.decreaseWizardNb(color);
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Normal);
        }
    }

    public override void ManageHealthRegen()
    {
        if (!isInBattle)
        {
            if (wizardHealthRegenRate >= WIZARD_HIDING_REGEN_RATE)
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
        if(healthPoints.getHp() == healthPoints.getMaxHp())
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Normal);
        }
        else if(healthPoints.getHp() >= healthPoints.getMaxHp() * hpPercentToReturnToNormal && isInBattle)
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Normal);
        }
        else if(healthPoints.getHp() <= healthPoints.getMaxHp() * hpPercentToReturnToFlee)
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Flee);
        }
        else if(wizardManager.getForestInContact() == null)
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Normal);
        }
        else if (GameManager.instance.didSomeoneWin())
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Inactive);
        }
    }
}
