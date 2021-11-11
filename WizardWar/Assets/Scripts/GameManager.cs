using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const string BLUE_COLOR = "blue";
    private const string GREEN_COLOR = "green";
    public static GameManager instance;

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
    private float currentBlueSpawnCooldown = 0;
    private GameObject[] greenWizardList;
    private float currentGreenSpawnCooldown = 0;
    private enum ColorsWinSide { Blue, Green, None };
    private ColorsWinSide winningTeam = ColorsWinSide.None;

    private int blueWizardNb = 0;
    private int greenWizardNb = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentBlueSpawnCooldown = setRandomSpawnCooldown(currentBlueSpawnCooldown);
        currentGreenSpawnCooldown = setRandomSpawnCooldown(currentGreenSpawnCooldown);
        setWizardsList(blueWizardList, blueWizard);
        setWizardsList(greenWizardList, greenWizard);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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

        if (winningTeam == ColorsWinSide.None)
        {
            currentBlueSpawnCooldown -= Time.deltaTime;
            currentGreenSpawnCooldown -= Time.deltaTime;

            if (currentBlueSpawnCooldown <= 0)
            {
                spawnWizard(blueTowers, blueWizardList);
                currentBlueSpawnCooldown = setRandomSpawnCooldown(currentBlueSpawnCooldown);
                increaseWizardNb(BLUE_COLOR);
            }
            if (currentGreenSpawnCooldown <= 0)
            {
                spawnWizard(greenTowers, greenWizardList);
                currentGreenSpawnCooldown = setRandomSpawnCooldown(currentGreenSpawnCooldown);
                increaseWizardNb(GREEN_COLOR);
            }
        }
    }

    private float setRandomSpawnCooldown(float cooldown)
    {
        cooldown = Random.Range(shortestSpawnCooldown, longestSpawnCooldown);
        return cooldown;
    }

    private void setWizardsList(GameObject[] list, GameObject wizardPrefab)
    {
        if (wizardPrefab.tag == "WizardBlue")
        {
            blueWizardList = new GameObject[maxNbOfWizardPerSide];

            for (int i = 0; i < maxNbOfWizardPerSide; i++)
            {
                blueWizardList[i] = Instantiate(wizardPrefab);
                blueWizardList[i].SetActive(false);
            }

        }
        else if (wizardPrefab.tag == "WizardGreen")
        {
            greenWizardList = new GameObject[maxNbOfWizardPerSide];

            for (int i = 0; i < maxNbOfWizardPerSide; i++)
            {
                greenWizardList[i] = Instantiate(wizardPrefab);
                greenWizardList[i].SetActive(false);

            }

        }


    }



    private void areOneSideTowersDestroyed()
    {
        if (blueTowers.Count == 0) winningTeam = ColorsWinSide.Green;
        if (greenTowers.Count == 0) winningTeam = ColorsWinSide.Blue;
    }

    private void endGame(ColorsWinSide winningTeam)
    {
        if (winningTeam == ColorsWinSide.Blue)
        {
            blueWizardsWinsText.gameObject.SetActive(true);
        }
        else if (winningTeam == ColorsWinSide.Green)
        {
            greenWizardsWinsText.gameObject.SetActive(true);
        }
    }

    private void removeTowerFromList(List<GameObject> towers)
    {
        for (int i = 0; i < towers.Count; i++)
        {
            if (!towers[i].gameObject.activeSelf) towers.RemoveAt(i);
        }
    }

    private void spawnWizard(List<GameObject> towers, GameObject[] wizards)
    {
        if (towers.Count > 0)
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

    public List<GameObject> getTowerList(string color)
    {
        if (color == "blue")
        {
            return blueTowers;
        }
        else if (color == "green")
        {
            return greenTowers;
        }
        return null;

    }

    public GameObject[] getEnnemyList(string color)
    {
        if (color == "blue")
        {
            return blueWizardList;
        }
        else if (color == "green")
        {
            return greenWizardList;
        }
        return null;
    }

    public void increaseWizardNb(string color)
    {
        if (color == "blue")
        {
            blueWizardNb++;
            changeBlueWizardNb(blueWizardNb);
        }
        else if (color == "green")
        {
            greenWizardNb++;
            changeGreenWizardNb(greenWizardNb);
        }
    }

    public void decreaseWizardNb(string color)
    {
        if (color == "blue")
        {
            blueWizardNb--;
            changeBlueWizardNb(blueWizardNb);
        }
        else if (color == "green")
        {
            greenWizardNb--;
            changeGreenWizardNb(greenWizardNb);
        }
    }
}
