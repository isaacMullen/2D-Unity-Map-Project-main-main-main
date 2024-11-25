using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerControls : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase player;
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
        MovePlayer();
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
        

   
        
        
    }

    void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        
        if(transform.position == target)
        {
            Vector3 difference = currentTile - newTile;
        }
    
    }
    //Bool that returns if a tile can be walked on or not with a true or false.
    bool IsTileWalkable(Vector3Int tilePosition)
    {
        TileBase tile = tilemap.GetTile(tilePosition);

        if(tile != null)
        {

            string tileName = tile.name;
           //If statement for tiles that I don't want the player to walk on.
            if(tileName == "Wall" || tileName == "Chest" || tileName == "Door")
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
            return true;
        }
    
    
    
    }


}
