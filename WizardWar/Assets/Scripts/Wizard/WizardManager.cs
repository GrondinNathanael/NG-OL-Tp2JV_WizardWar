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
    // Start is called before the first frame update
    void Start()
    {
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
}
