using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject blueWizard;
    [SerializeField] private GameObject greenWizard;
    [SerializeField] private int maxNbOfWizardPerSide = 30;

    [SerializeField] private List<GameObject> blueTowers;
    [SerializeField] private List<GameObject> greenTowers;
    [SerializeField] private float shortestSpawnCooldown = 2f;
    [SerializeField] private float longestSpawnCooldown = 8f;

    private GameObject[] blueWizardList;
    private float currentBlueSpawnCooldown;
    private GameObject[] greenWizardList;
    private float currentGreenSpawnCooldown;
    

    // Start is called before the first frame update
    void Start()
    {
        setRandomSpawnCooldown(currentBlueSpawnCooldown);
        setRandomSpawnCooldown(currentGreenSpawnCooldown);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setRandomSpawnCooldown(float cooldown)
    {
        cooldown = Random.Range(shortestSpawnCooldown, longestSpawnCooldown);
    }
}
