using UnityEngine;

public class WizardStateNormal : WizardState
{
    private const float WIZARD_BASE_SPEED = 2f;
    private const float WIZARD_BASE_RANGE = 2f;
    private const float WIZARD_BASE_RATE_OF_FIRE = 3f;
    private const int WIZARD_MAX_ATTACK = 5;
    private const int WIZARD_MIN_ATTACK = 1;
    private const int WIZRAD_BASE_HEALTH_REGEN = 1;
    private const float WIZARD_BASE_REGEN_RATE = 1f;
    private const float FOREST_SPEED_REDUCTION = 0.5f;
    private const float WIZARD_DAMAGE_REDUCTION = 0.80f;
    private const int NUMBER_OF_KILL_TO_INTREPID = 3;
    private const float FLEE_HEALTH_THRESHOLD = 0.25f;
    private float wizardBaseDamage;

    // Start is called before the first frame update
    void Start()
    {
        speed = WIZARD_BASE_SPEED;
        wizardRange = WIZARD_BASE_RANGE;
        isInBattle = false;
        wizardRateOfFire = WIZARD_BASE_RATE_OF_FIRE;
        wizardDamage = Random.Range(WIZARD_MIN_ATTACK, WIZARD_MAX_ATTACK);
        wizardHealthRegenNumber = WIZRAD_BASE_HEALTH_REGEN;
        wizardHealthRegenRate = 0f;
        wizardBaseDamage = wizardDamage;
        numberOfKills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (wizardManager.getForestInContact() != null && speed == WIZARD_BASE_SPEED)
            decreaseWizardStatsInForest();
        else if (wizardManager.getForestInContact() == null)
            resetWizardStats();
        ManageIsInBattle();
        MoveWizard();
        ManageBattle();
        ManageStateChange();
        ManageDeath();
        ManageHealthRegen();

        if (isStateShowInConsole) 
        {
            Debug.Log("État normal");
        }
    }

    public override void ManageStateChange()
    {
        if (numberOfKills >= NUMBER_OF_KILL_TO_INTREPID)
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Intrepid);
        }
        else if (healthPoints.getHp() < (healthPoints.getMaxHp() * FLEE_HEALTH_THRESHOLD)) 
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Flee);
        }
        else if (GameManager.instance.didSomeoneWin())
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Inactive);
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

    public override void ManageBattle()
    {
        if (isInBattle)
        {
            if (wizardRateOfFire >= WIZARD_BASE_RATE_OF_FIRE)
            {
                if (wizardTarget.tag != "Tower")
                {
                    if (wizardTarget.GetComponent<WizardManager>().getForestInContact() != null)
                    {
                        wizardTarget.GetComponent<HealthPoints>().getDamaged(wizardDamage);
                    }
                    else
                    {
                        wizardTarget.GetComponent<HealthPoints>().getDamaged(wizardDamage * WIZARD_DAMAGE_REDUCTION);
                    }
                }
                else
                {
                    wizardTarget.GetComponent<HealthPoints>().getDamaged(wizardDamage);
                }
                wizardRateOfFire = 0f;
            }
            else
            {
                wizardRateOfFire += Time.deltaTime;
            }

            if (!wizardTarget.activeSelf || Vector2.Distance(transform.position, wizardTarget.transform.position) > wizardRange)
            {
                isTargetKilled();
                isInBattle = false;
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
        if (!isInBattle)
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
    }

    private void resetWizardStats()
    {
        speed = WIZARD_BASE_SPEED;
    }

    private void decreaseWizardStatsInForest()
    {
        speed *= FOREST_SPEED_REDUCTION;
    }

    private void isTargetKilled()
    {
        if (!wizardTarget.activeSelf)
        {
            numberOfKills++;
        }
    }
}