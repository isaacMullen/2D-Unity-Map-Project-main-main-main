using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI textmeshpro;
    public int health;
    public string healthStatus;
   
    
    public string ShowHUD()
    {
        healthStatus = HealthStatus(health);
        return $"Health: {health}";
    }



    
    public string HealthStatus(int hp)
    {
        if (hp <= 100 && hp > 90)
        {
            return "Perfect Health";
        }
        else if (hp <= 90 && hp > 75)
        {
            return "Healthy";
        }
        else if (hp <= 75 && hp > 50)
        {
            return "Hurt";
        }
        else if (hp <= 50 && hp > 10)
        {
            return "Badly Hurt";
        }
        else if (hp <= 10 && hp >= 1)
        {
            return "Imminent Danger";
        }

        return "Out of Range";

    }

}
