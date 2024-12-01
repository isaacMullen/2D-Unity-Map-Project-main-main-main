using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyScript : MonoBehaviour
{
    //Health system instance that gives the Enemy its own health system.
    Health healthSystem = new Health();
    public Tilemap tilemap;
    //Variable for the Player Tile.
    public Transform PlayerTile;
    //Public bool for checking if the Enemy has moved.
    public bool HasMoved = true;
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (HasMoved)
        {
            return;
        }
        else
        {
            FollowPlayer();
        }
        


    }

    //Method for the Enemy to follow the player with.
    public void FollowPlayer()
    {
        if(HasMoved)
        {
            return;
        }
        else
        {
            //Gives the direction of the player with normalizing the transform position of the player.
            Vector3Int CellPosition = tilemap.WorldToCell(transform.position);
            //
            Vector3Int PlayerPosition = tilemap.WorldToCell(PlayerTile.position);
            //
            Vector3Int DirectionToPlayer = PlayerPosition - CellPosition;
            //
            Vector3Int NewPosition = CellPosition + new Vector3Int(-1, 0, 0);
            //

            transform.position = tilemap.CellToWorld(NewPosition);
        
            HasMoved = true;
            return;
        }
        
    }

    ////Method for Enemy to attack the player.
    //void EnemyAttack(Vector3Int tilePosition)
    //{

    //}
    
    //bool for checking if certain tiles are walkable for the Enemy.
    bool IsTileWalkable(Vector3Int tilePosition)
    {
        TileBase tile = tilemap.GetTile(tilePosition);


        if (tile != null)
        {
            //Combat Check
            //if (tile.tileposition == playerposition)
            //{
            //    EnemyAttack(tilePosition);
            //    return false;
            //}

            string tileName = tile.name;
            //If statement for tiles that I don't want the player to walk on.
            if (tileName == "Wall Tile" || tileName == "Chest Tile" || tileName == "DoorTile" || tileName == "PlayerTile")
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
