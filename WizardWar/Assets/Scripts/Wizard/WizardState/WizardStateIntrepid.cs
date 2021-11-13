using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateIntrepid : WizardState
{
    private const float INTREPID_BASE_SPEED = 2.5f;
    private const float FOREST_SPEED_REDUCTION = 0.5f;
    private const float WIZARD_BASE_RANGE = 2f;
    private const float WIZARD_BASE_RATE_OF_FIRE = 3f;
    private const float WIZARD_DAMAGE_REDUCTION = 0.80f;
    private const int WIZRAD_BASE_HEALTH_REGEN = 1;
    private const float WIZARD_BASE_REGEN_RATE = 1f;
    private const int WIZARD_MAX_ATTACK = 5;
    private const int WIZARD_MIN_ATTACK = 1;
    private float wizardBaseDamage;
    private bool isAttackedByEnnemy = false;
    private float tempHealth;

    void Start()
    {
        speed = INTREPID_BASE_SPEED;
        wizardRange = WIZARD_BASE_RANGE;
        isInBattle = false;
        wizardHealthRegenNumber = WIZRAD_BASE_HEALTH_REGEN;
        wizardRateOfFire = WIZARD_BASE_RATE_OF_FIRE;
        wizardDamage = Random.Range(WIZARD_MIN_ATTACK, WIZARD_MAX_ATTACK);
        wizardHealthRegenRate = 0f;
        wizardBaseDamage = wizardDamage;
        tempHealth = healthPoints.getHp();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("is in intrepid state");
        Debug.Log(isAttackedByEnnemy);

        if (wizardManager.getForestInContact() != null && speed == INTREPID_BASE_SPEED)
            decreaseWizardStatsInForest();
        else if (wizardManager.getForestInContact() == null)
            resetWizardStats();

        isAttacked();
        ManageIsInBattle();
        MoveWizard();
        ManageBattle();
        ManageStateChange();
        ManageDeath();
        ManageHealthRegen();
    }
    public override void ManageBattle()
    {
        if (isInBattle)
        {
            if (wizardRateOfFire >= WIZARD_BASE_RATE_OF_FIRE)
            {
                wizardTarget.GetComponent<HealthPoints>().getDamaged(wizardDamage);
                wizardRateOfFire = 0f;

            }
            else
            {
                wizardRateOfFire += Time.deltaTime;
            }

            if (!wizardTarget.activeSelf || Vector2.Distance(transform.position, wizardTarget.transform.position) > wizardRange)
            {
                isInBattle = false;
                isAttackedByEnnemy = false;
                tempHealth = healthPoints.getHp();
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
        if (wizardHealthRegenRate >= WIZARD_BASE_REGEN_RATE)
        {
            healthPoints.getHealed(wizardHealthRegenNumber);
            wizardHealthRegenRate = 0f;
        }
        else
        {
            wizardHealthRegenRate += Time.deltaTime;
        }
    }

    public override void ManageIsInBattle()
    {
        for (int i = 0; i < GameManager.instance.getTowerList(ennemyColor).Count; i++)
        {
            if (Vector2.Distance(transform.position, GameManager.instance.getTowerList(ennemyColor)[i].transform.position) < wizardRange)
            {
                isInBattle = true;
                wizardTarget = GameManager.instance.getTowerList(ennemyColor)[i];
                return;
            }
        }
        if (isAttackedByEnnemy) {
            if (wizardTarget == null || !wizardTarget.activeInHierarchy) {
                for (int i = 0; i < GameManager.instance.getEnnemyList(ennemyColor).Length; i++)
                {

                    if (Vector2.Distance(transform.position, GameManager.instance.getEnnemyList(ennemyColor)[i].transform.position) < wizardRange && GameManager.instance.getEnnemyList(ennemyColor)[i].activeSelf)
                    {
                        isInBattle = true;
                        wizardTarget = GameManager.instance.getEnnemyList(ennemyColor)[i];
                        return;
                    }
                }
            }
        }
    }

    public override void ManageStateChange()
    {
        if (GameManager.instance.didSomeoneWin())
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Inactive);
        }
        else if (healthPoints.getHp() <= 0) 
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Normal);
        }
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

    private void resetWizardStats()
    {
        speed = INTREPID_BASE_SPEED;
    }

    private void decreaseWizardStatsInForest()
    {
        speed *= FOREST_SPEED_REDUCTION;
    }
    private void isAttacked() 
    {
        if (tempHealth > healthPoints.getHp()) 
        {
            isAttackedByEnnemy = true;
            tempHealth = healthPoints.getHp();
        }
    }

}
