using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text nbBlueWizardText;
    [SerializeField] private Text nbGreenWizardText;
    [SerializeField] private Text blueWizardsWinsText;
    [SerializeField] private Text greenWizardsWinsText;

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
    private enum ColorsWinSide{ Blue, Green, None };
    private ColorsWinSide winningTeam = ColorsWinSide.None;

    // Start is called before the first frame update
    void Start()
    {
        setRandomSpawnCooldown(currentBlueSpawnCooldown);
        setRandomSpawnCooldown(currentGreenSpawnCooldown);
        setWizardsList(blueWizardList, blueWizard);
        setWizardsList(greenWizardList, greenWizard);
    }

    // Update is called once per frame
    void Update()
    {
        removeTowerFromList(blueTowers);
        removeTowerFromList(greenTowers);

        areOneSideTowersDestroyed();
        if (winningTeam != ColorsWinSide.None)
        {
            endGame(winningTeam);
        }

        spawnWizard(blueTowers, blueWizardList);
        spawnWizard(greenTowers, greenWizardList);
    }

    private void setRandomSpawnCooldown(float cooldown)
    {
        cooldown = Random.Range(shortestSpawnCooldown, longestSpawnCooldown);
    }

    private void setWizardsList(GameObject[] list, GameObject wizardPrefab)
    {
        list = new GameObject[maxNbOfWizardPerSide];

        for (int i = 0; i < maxNbOfWizardPerSide; i++)
        {
            list[i] = Instantiate(wizardPrefab);
            list[i].SetActive(false);
        }
    }

    private void areOneSideTowersDestroyed()
    {
        if (blueTowers.Count == 0) winningTeam = ColorsWinSide.Green;
        if (greenTowers.Count == 0) winningTeam = ColorsWinSide.Blue;
    }

    private void endGame(ColorsWinSide winningTeam)
    {
        if(winningTeam == ColorsWinSide.Blue)
        {
            blueWizardsWinsText.gameObject.SetActive(true);
        }
        else if(winningTeam == ColorsWinSide.Green)
        {
            greenWizardsWinsText.gameObject.SetActive(true);
        }
    }

    private void removeTowerFromList(List<GameObject> towers)
    {
        for(int i = 0; i < towers.Count; i++)
        {
            if (!towers[i].gameObject.activeSelf) towers.RemoveAt(i);
        }
    }

    private void spawnWizard(List<GameObject> towers, GameObject[] wizards)
    {
        if(towers.Count > 0)
        {
            int nextWizard = getNextWizard(wizards);
            if (nextWizard >= 0)
            {
                int towerToSpawn = Random.Range(0, towers.Count);
                Vector2 spawnPos = towers[towerToSpawn].transform.position;

                wizards[nextWizard].transform.position = spawnPos;
                wizards[nextWizard].SetActive(true);
            }
        }
    }

    private int getNextWizard(GameObject[] wizards)
    {
        for (int i = 0; i < maxNbOfWizardPerSide; i++)
        {
            if (!wizards[i].activeSelf) return i;
        }
        return -1;
    }

    public void changeBlueWizardNb(int blueWizardsNb)
    {
        nbBlueWizardText.text = blueWizardsNb.ToString();
    }

    public void changeGreenWizardNb(int greenWizardsNb)
    {
        nbGreenWizardText.text = greenWizardsNb.ToString();
    }
}
