using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    private const string GREEN = "green";
    private const string BLUE = "blue";
    [SerializeField] private GameManager gameManager;
    private List<GameObject> ennemyTowerList;
    private GameObject[] ennemyList;
    private List<GameObject> allyTowerList;

    public enum WizardStateToSwitch { Normal, Intrepid, Flee, Hiding, Safe, Inactive }
    private WizardState wizardState;

    private Transform forestInContact = null;
    private Transform towerInContact = null;

    // Start is called before the first frame update
    void Start()
    {
        wizardState = GetComponent<WizardState>();
        if (transform.tag == "WizardBlue")
        {
            allyTowerList = gameManager.getTowerList(BLUE);
            ennemyTowerList = gameManager.getTowerList(GREEN);
            ennemyList = gameManager.getEnnemyList(GREEN);
        }
        else
        {
            allyTowerList = gameManager.getTowerList(GREEN);
            ennemyTowerList = gameManager.getTowerList(BLUE);
            ennemyList = gameManager.getEnnemyList(BLUE);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeWizardState(WizardStateToSwitch nextState)
    {
        Destroy(wizardState);

        switch (nextState)
        {
            case WizardStateToSwitch.Normal:
                {
                    wizardState = gameObject.AddComponent<WizardStateNormal>() as WizardStateNormal;
                    break;
                }
            case WizardStateToSwitch.Intrepid:
                {
                    wizardState = gameObject.AddComponent<WizardStateIntrepid>() as WizardStateIntrepid;
                    break;
                }
            case WizardStateToSwitch.Flee:
                {
                    wizardState = gameObject.AddComponent<WizardStateFlee>() as WizardStateFlee;
                    break;
                }
            case WizardStateToSwitch.Hiding:
                {
                    wizardState = gameObject.AddComponent<WizardStateHiding>() as WizardStateHiding;
                    break;
                }
            case WizardStateToSwitch.Safe:
                {
                    wizardState = gameObject.AddComponent<WizardStateSafe>() as WizardStateSafe;
                    break;
                }
            case WizardStateToSwitch.Inactive:
                {
                    wizardState = gameObject.AddComponent<WizardStateInactive>() as WizardStateInactive;
                    break;
                }
        }
    }

    public Transform getForestInContact()
    {
        return forestInContact;
    }

    public void quitForest()
    {
        forestInContact = null;
    }

    public Transform getTowerInContact()
    {
        return towerInContact;
    }

    public void quitTower()
    {
        towerInContact = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bush")
            forestInContact = other.transform;
        if (other.gameObject.tag == "Tower")
            towerInContact = other.transform;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bush")
            forestInContact = null;
        if (other.gameObject.tag == "Tower")
            towerInContact = null;
    }
}
