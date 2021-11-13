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
    }

    // Update is called once per frame
    void Update()
    {
        ManageIsInBattle();
        MoveWizard();
        ManageBattle();
        ManageStateChange();
        ManageDeath();
        ManageHealthRegen();
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
            if (wizardRateOfFire >= WIZARD_BASE_RATE_OF_FIRE)
            {

                wizardTarget.GetComponent<HealthPoints>().getDamaged(wizardDamage);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bush")
        {
            decreaseWizardStatsInForest();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bush")
        {
            resetWizardStats();
        }
    }

    private void resetWizardStats()
    {
        speed = WIZARD_BASE_SPEED;
        wizardDamage = wizardBaseDamage;
    }

    private void decreaseWizardStatsInForest()
    {
        speed *= FOREST_SPEED_REDUCTION;
        wizardDamage *= WIZARD_DAMAGE_REDUCTION;
    }
}