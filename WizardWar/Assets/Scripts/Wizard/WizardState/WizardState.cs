using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected WizardManager wizardManager;
    protected HealthPoints healthPoints;
    protected float speed;
    protected bool isInBattle;
    protected int numberOfKills;
    protected float wizardRange;
    protected float wizardRateOfFire;
    protected float wizardDamage;
    protected float wizardHealthRegenRate;
    protected int wizardHealthRegenNumber;
    protected bool isInForest;
    protected enum wizardColors { GREEN, BLUE };
    protected string color;
    protected string ennemyColor;
    protected GameObject wizardTarget;

    // Start is called before the first frame update
    void Awake()
    {
        wizardManager = GetComponent<WizardManager>();
        if (gameObject.tag == "WizardBlue")
        {
            color = "blue";
            ennemyColor = "green";
        }
        else if (gameObject.tag == "WizardGreen")
        {
            color = "green";
            ennemyColor = "blue";
        }

        healthPoints = GetComponent<HealthPoints>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void MoveWizard();

    public abstract void ManageStateChange();

    public abstract void ManageBattle();
    public abstract void ManageIsInBattle();

    public abstract void ManageDeath();

    public abstract void ManageHealthRegen();
}