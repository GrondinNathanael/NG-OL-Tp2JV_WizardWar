using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private float maxHp = 50;


    private float hp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        hp = maxHp;
    }

    public void getHealed(int points)
    {
        if (hp < maxHp)
        {
            hp += points;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
    }

    public void getDamaged(float points)
    {
        hp -= points;
    }

    public void die()
    {
        gameObject.SetActive(false);
    }

    public float getHp()
    {
        return hp;
    }
}
