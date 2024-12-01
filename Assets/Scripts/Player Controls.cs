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
  

    private Vector3Int newTile;
    private Vector3 target;



    void Start()
    {
        
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
            
    }

    //Method for moving the player on the map.
    void HandleInput()
    {
        
        //Keycodes for the four different WASD directions.
        if(Input.GetKeyDown(KeyCode.W))
        {
            newTile = currentTile + new Vector3Int(0, 1, 0);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            newTile = currentTile + new Vector3Int(0, -1, 0);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            newTile = currentTile + new Vector3Int(1, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            newTile = currentTile + new Vector3Int(-1, 0, 0);
        }
        else 
        { 
            newTile = currentTile;
        }

        target = tilemap.CellToWorld(newTile);


    }

    //Method for moving the player if tile is 
    void MovePlayer()
    {
        if (IsTileWalkable(newTile))
        {
            transform.position = newTile;

            currentTile = newTile;
        }

      
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
