using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerControls : MonoBehaviour
{
    public Tilemap tilemap;
    
    public Vector3Int currentTile;
    public float moveSpeed = 1.0f;
    public GameObject PlayerTile;
    public bool IsplayersTurn = true;
    public bool HasMoved = false;
    public EnemyScript enemyscript;

    private Vector3Int newTile;
    private Vector3 target;

    Health healthSystem = new Health();

    void Start()
    {
        enemyscript = GameObject.FindWithTag("Enemy").GetComponent<EnemyScript>();
        currentTile = tilemap.WorldToCell(transform.position);
        target = transform.position;

    }

    void Update()
    {
        HandleInput();
        if (IsTileWalkable(newTile))
        {
            MovePlayer();
            
        }
        if (HasMoved)
        {
            StartEnemyTurn();
        }
            
    }

    //Method for moving the player on the map.
    void HandleInput()
    {
        
        //Keycodes for the four different WASD directions.
        if(Input.GetKeyDown(KeyCode.W))
        {
            newTile = currentTile + new Vector3Int(0, 1, 0);
            HasMoved = true;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            newTile = currentTile + new Vector3Int(0, -1, 0);
            HasMoved = true;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            newTile = currentTile + new Vector3Int(1, 0, 0);
            HasMoved = true;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            newTile = currentTile + new Vector3Int(-1, 0, 0);
            HasMoved = true;
        }
        else 
        { 
            newTile = currentTile;
        }

        target = tilemap.CellToWorld(newTile);

        
    }

    //Method for moving the player if tile is walkable.
    void MovePlayer()
    {
        //If statement that allows the player
        if (IsTileWalkable(newTile) && IsplayersTurn)
        {
            transform.position = newTile;

            currentTile = newTile; 
        
        }
 
    }

    void StartEnemyTurn()
    {
        IsplayersTurn = false;

        enemyscript.HasMoved = false;
    }

    //Method for calling the attack from the player.
    void PlayerAttack(Vector3Int tilePosition)
    {
        
    }

    //Bool that returns if a tile can be walked on or not with a true or false.
    bool IsTileWalkable(Vector3Int tilePosition)
    {
        TileBase tile = tilemap.GetTile(tilePosition);

        //Debug.Log(tile);
        if(tile != null)
        {
            //Combat Check
            //if (tile.tileposition == enemyposition)
            //{
            //    PlayerAttack(tilePosition);
            //    return false;
            //}

            string tileName = tile.name;
           //If statement for tiles that I don't want the player to walk on.
            if(tileName == "Wall Tile" || tileName == "Chest Tile" || tileName == "DoorTile" || tileName == "Enemy Tile")
            {
                return false;
            }
            else
            {
                return true;
            }
           
        }
        else
        {
            return false;
        }
    
    
    
    }


}
