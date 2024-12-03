using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public PlayerControls playerControls;
    //Tilemap variable for enemy,
    public Tilemap tilemap;      
    //Game objects for the enemy tile and the win text after defeating the enemy.
    public GameObject Enemy;       
    public GameObject WinTextObject;
    //TextMeshProUGUI for displaying Enemy health and health statuses.
    public TextMeshProUGUI healthText;
    //Variable for setting enemy health to 100.
    public int Health = 100;
    //Public string for the health status of the enemy depending on how much health it has.
    public string healthStatus;
    //Variable for the Player Tile.
    public Transform PlayerTile;
    //Damage amount Enemy can do to the player and a check for combat
    private int damage = 20;
    bool inCombat = false;
    bool turnIsOver = false;
    

    //Variable to store Enemy Position
    public Vector3Int enemyPos;
    bool moving;
    

    //Public bool for checking if the Enemy has moved.
    public bool HasMoved = false;
    
    // Start is called before the first frame update
    void Start()
    {                
        Debug.Log(PlayerTile);

        healthText.SetText(playerControls.health.ToString());
    }

    public string ShowHUD()
    {
        string healthStatus = HealthStatus(playerControls.health);
        
        if (healthText != null)
        {
            healthText.SetText($"Health: {playerControls.health}| Health Status: {healthStatus}");
        }
        else
        {
            Debug.LogError("Component not found");
        }
        
        return $"Health: {playerControls.health} " + $"Health Status: {healthStatus}";

    }

    //Method for player taking damage.
    public void TakeDamage()
    {
        if (damage < 0)
        {
            Debug.LogWarning("Damage can't be negative");
            return;
        }

        if (Health <= 0)
        {
            Debug.LogWarning("Player is dead, no more damage can be taken");
            return;
        }

        //If statement for player health reaching zero.
        if (Health == 0)
        {
            Enemy.SetActive(false);
            WinTextObject.SetActive(true);
        }

        //Changes health to health minus damage taken.
        Health -= damage;
    }

    void HandleTurns()
    {
        // A boolean to track whose turn it is; false = enemy's turn, true = player's turn.
        bool playerTurn = false;

        // Check if the current turn is still ongoing.
        if (!turnIsOver)
        {
            if (!playerTurn) // If it's the enemy's turn
            {
                // The enemy attacks by reducing the player's health.
                playerControls.health -= damage;
                Debug.Log("Enemy attacks! Player health: " + playerControls.health);

                // Update the enemy's health status (if relevant, e.g., self-damage logic).
                TakeDamage();

                // Switch to the player's turn after the enemy finishes attacking.
                playerTurn = true;
            }
            else // If it's the player's turn
            {
                // Wait for any key press to simulate the player taking their action.
                if (Input.anyKeyDown)
                {
                    Debug.Log("Player attacks!");

                    // Switch back to the enemy's turn after the player finishes their action.
                    playerTurn = false;
                }
            }

            // Mark the current turn as completed.
            turnIsOver = true;
        }
        else
        {
            // Reset the turn status to allow the next turn to begin.
            turnIsOver = false;
        }
    }
    //Method for the health status that lets the player know how badly they are injured depending on the range of their health.
    public string HealthStatus(int hp)
    {
        //If statements that return different health statuses depending on how high or low the player's health is.
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

    // Update is called once per frame
    void Update()
    {                    
        if(!inCombat && !turnIsOver)
        {
            StartCoroutine(FollowPlayer());
        }

        else if(inCombat && !turnIsOver)
        {
            HandleTurns();            
        }

        ShowHUD();

    }

    //Method for the Enemy to follow the player with.
    public IEnumerator FollowPlayer()
    {                
        //Stopping from being called while it's already running
        if(moving) yield break;

        moving = true;
                        
        //CALCULATE THE DIRECTION TO PLAYER
        
        Debug.Log($"New Position: {CheckDirectionToMove()}");
        //
        yield return new WaitForSeconds(.6f);

        if(IsTileWalkable(CheckDirectionToMove()))
        {
            transform.position = CheckDirectionToMove();
        }        
        //Resetting ability to play
        moving = false;
    }

    ////Method for Enemy to attack the player.
    //void EnemyAttack(Vector3Int tilePosition)
    //{

    //}
    
    Vector3Int CheckDirectionToMove()
    {        
        Vector3Int direction = new();

        Vector3Int CellPosition = tilemap.WorldToCell(transform.position);
        //Gives the direction of the player with normalizing the transform position of the player.
        
        //
        Vector3Int PlayerPosition = tilemap.WorldToCell(PlayerTile.position);
        
        //
        Vector3Int DifferenceFromEnemyToPlayer = (PlayerPosition - CellPosition);
        
        //If the x is larger. The direction is set to the left (-1, 0, 0)
        if (DifferenceFromEnemyToPlayer.x > 0) 
        {
            //Comparing the x and the y to determine which to prioritize
            if (Mathf.Abs(DifferenceFromEnemyToPlayer.y) <= DifferenceFromEnemyToPlayer.x)
            {
                direction = Vector3Int.left;
            }
        }
        //If the x is smaller. The direction is set to the right (1, 0, 0)
        else if (DifferenceFromEnemyToPlayer.x < 0) 
        {
            //Comparing the x and the y to determine which to prioritize
            if (Mathf.Abs(DifferenceFromEnemyToPlayer.y) <= Mathf.Abs(DifferenceFromEnemyToPlayer.x))
            {
                direction = Vector3Int.right;
            }
        }
        //If the y is larger. The direction is set to down (0, -1, 0)
        if (DifferenceFromEnemyToPlayer.y > 0) 
        {
            //Comparing the x and the y to determine which to prioritize
            if (Mathf.Abs(DifferenceFromEnemyToPlayer.x) <= DifferenceFromEnemyToPlayer.y)
            {
                direction = Vector3Int.down;
            }
        }
        //If the y is smaller. The direction is set to up (0, 1, 0)
        else if (DifferenceFromEnemyToPlayer.y < 0) 
        {
            //Comparing the x and the y to determine which to prioritize
            if (Mathf.Abs(DifferenceFromEnemyToPlayer.x) <= Mathf.Abs(DifferenceFromEnemyToPlayer.y))
            {
                direction = Vector3Int.up;
            }
        }
        
        Vector3Int NewPosition = CellPosition - direction;
        return NewPosition;
    }
    
    //bool for checking if certain tiles are walkable for the Enemy.
    bool IsTileWalkable(Vector3Int tilePosition)
    {
        TileBase tile = tilemap.GetTile(tilePosition);

        Vector3Int newPlayerPosition = tilemap.WorldToCell(new Vector3(PlayerTile.position.x, PlayerTile.position.y, PlayerTile.position.z));


        if (tile != null)
        {
            //Debug.Log($"Tile: {tilePosition} | Player Position: {newPlayerPosition}");
            
            //Combat Check
            //if (tile.tileposition == playerposition)
            //{
            //    EnemyAttack(tilePosition);
            //    return false;
            //}

            //A string for making tileName equal the name of the tiles.            
            //If statement for tiles that I don't want the player to walk on.
            if(tilePosition == newPlayerPosition)
            {
                Debug.Log($"Collided with Player");

                inCombat = true;

                return false;
            }
            
            else if (tile.name == "Wall Tile" || tile.name == "Chest Tile" || tile.name == "DoorTile" || tile.name == "PlayerTile")
            {                
                return false;
            }
            
            else
            {
                return true;
            }
        }        
        return false;        
    }
}
