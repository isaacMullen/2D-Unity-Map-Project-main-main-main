using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    //Public textmeshpro for displaying the health, health status and lives.
    public TextMeshProUGUI textmeshpro;
    //Public variables for setting the values of the health and lives, which is declared on the variable.
    public int health = 100;
    public int lives = 3;
    //Public string for writing the health status of the player.
    public string healthStatus;

    

    public void Awake()
    {
        textmeshpro = GetComponent<TextMeshProUGUI>();
        
        textmeshpro.text = ShowHUD();
        
    }

    //Method that prints the health, health status and number of lives the player has into the game.
    public string ShowHUD()
    {
        healthStatus = HealthStatus(health);
        textmeshpro.text = $"Health: {health} " + $"Lives: {lives} " + $"Health Status: {healthStatus}";
        
        return textmeshpro.text;
        
    }

    //Method for player taking damage.
    public void TakeDamage(int damage)
    {
        health = health - damage;
        //If statement for player health reaching zero, makes sure health doesn't go into the negatives
        //and resets their stats with the revive method.
        if(health <= 0)
        {
            health = 0;
            Revive();
        }
    }

    //Method for the health status that lets the player know how badly they are injured depending on the range of their health.
    public string HealthStatus(int hp)
    {
        //If statements that return different health statuses depending on the range of the player's health.
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

        //Returns out of range incase the health goes outside the 0 - 100 range for whatever reason.
        return "Out of Range";

    }

    //Method for reviving the player after they lose a life, resets health to 100.
    public void Revive()
    {
        lives = lives - 1;
        health = 100;

        //Resets game if player has no more lives.
        if(lives <= 0)
        {
            ResetGame();
        }
    }

    //Resets the health and lives values after the player dies.
    public void ResetGame()
    {
        health = 100;
        lives = 3;
    }



}
