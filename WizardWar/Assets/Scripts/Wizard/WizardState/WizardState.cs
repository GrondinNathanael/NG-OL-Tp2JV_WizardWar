using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected WizardManager wizardManager;
    protected GameObject[] towerList;
    protected GameObject[] greenTowerList;
    protected GameObject[] blueTowerList;
    protected GameObject[] ennemyList;
    protected GameObject[] greenWizardList;
    protected GameObject[] blueWizardList;
    protected float speed;
    protected bool isInBattle;
    protected int numberOfKills;
    protected float wizardRange;
    protected enum WizardColors { GREEN, BLUE }
    protected WizardColors wizardColor;

    // Start is called before the first frame update
    void Awake()
    {
        wizardManager = GetComponent<WizardManager>();
        greenTowerList = GameObject.FindGameObjectsWithTag("TowerGreen");
        blueTowerList = GameObject.FindGameObjectsWithTag("TowerBlue");
        greenWizardList = GameObject.FindGameObjectsWithTag("WizardGreen");
        blueWizardList = GameObject.FindGameObjectsWithTag("WizardBlue");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void MoveWizard();

    public abstract void ManageStateChange();

    public abstract void ManageBattle();
}