using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected WizardManager wizardManager;
    protected float speed;
    protected bool isInBattle;
    protected int numberOfKills;
    protected float wizardRange;
    protected enum wizardColors { GREEN, BLUE};
    protected string color;
    protected string ennemyColor;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void MoveWizard();

    public abstract void ManageStateChange();

    public abstract void ManageBattle();
}