using UnityEngine;

public class WizardStateNormal : WizardState
{


    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        wizardRange = 2f;
        isInBattle = false;
        wizardRateOfFire = 2f;
        wizardDamage = Random.Range(1, 10);
        wizardHealthRegenNumber = 1;
        wizardHealthRegenRate = 0f;
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
            if (wizardRateOfFire >= 3f)
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
        if (healthPoints.hp <= 0)
        {
            healthPoints.die();
            GameManager.instance.decreaseWizardNb(color);
        }
    }

    public override void ManageHealthRegen()
    {
        if (!isInBattle)
        {
            if (wizardHealthRegenRate >= 1)
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
}