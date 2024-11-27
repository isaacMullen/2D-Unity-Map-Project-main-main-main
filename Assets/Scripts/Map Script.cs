using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;


public class MapScript : MonoBehaviour
{
    //creating the tilemap and tilebases for the player, chests, walls and doors.
    public Tilemap Tilemap;
    public TileBase Wall;
    public TileBase Ground;
    public TileBase Player;
    public TileBase Chest;
    public TileBase Door;
    string pathToMyFile = $"{Application.dataPath}/TextFiles/PreMadeMap.txt";
    Vector3Int Cellposition;

    //declaring a character array and passing parameters into it.
    char walls = '%';
    char ground = '@';
    char player = 'O';
    char chest = 'C';
    char door = 'D';
   
    //Creating the x and y sizes of the map
    int width;
    int height;
    char[,] Map;

    private void Start()
    {
        width = 15;
        height = 10;
        Map = new char[width, height];
        ConvertToTileMap(LoadPremadeMap(pathToMyFile));
       
        
    }

    //The method for generating the string of the map.
    void GenerateMap(int width, int height)
    {
       
        
        //Integer of a random Y position for the placement of the doors on the height.
        int randomYposition = Random.Range(1, height - 3);
        //Integer of a random X position for the placement of the doors on the width.
        int randomXposition = Random.Range(1, width - 3);
       
        //Runs for every iteration of inside width loop and draws the width.
        for (int y = 0; y < height; y++)
        {
            //runs for every iteration of the outer height loop.
            for (int x = 0; x < width; x++)
            {
                //Assigns and draws the ground with its character in the console.
                Map[x, y] = ground;

                //conditional statement/rule that generates walls around the border of the play area.
                if (x == width - 1 || y == height - 1 || y == 0 || x == 0)
                {
                    //Assigns and draws the wall characters around the borders of the ground.
                    Map[x, y] = walls;
                }
                //Another conditional statement/rule that generates doors around the inside of the walls.
                else if (x == width - 2 && y == randomYposition || y == height - 2 && x == randomXposition)
                {
                    //Assigns the door character to the space it gets generated to.
                    Map[x, y] = door;
                }
                //Conditional Statement/rule for placing the Chest on the map.
                else if(x == width - 3 && y == height - 3)
                {
                    Map[x, y] = chest;
                }
                //Conditional statement that always draws the character to a specific point on the map. 
               

            }

               
        
        }


        //An instance of stringbuilder to build the map.
        StringBuilder BuiltString = new StringBuilder();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //Appends each character of the map to a string.
                BuiltString.Append(Map[x, y]);

            }

            //Appended each character to the map builder.
            BuiltString.AppendLine();

        }

        //Returns the Map string to the return value of the method.
       string MapString = BuiltString.ToString();
        ConvertToTileMap(MapString); 
        

    }
    
    //The method for drawing the Tilemap.
   void ConvertToTileMap(string mapData)
    {

        
        //Two for loops that check where tiles should be placed on the map.
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Cellposition = new Vector3Int(x, y, 0);
               //If statement that draws the wall tile to all the positions on the map that they are placed.
                if (Map[x,y] == walls)
                {
                    Tilemap.SetTile(Cellposition, Wall);
                }

                //Group of if statements that draw all the tiles to the grid in Unity.
                if(Map[x,y] ==  door)
                {
                    Tilemap.SetTile(Cellposition, Door);
                }
                if (Map[x,y] == player)
                {
                    Tilemap.SetTile(Cellposition, Player);
                }
                if (Map[x, y] == ground)
                {
                    Tilemap.SetTile(Cellposition, Ground);
                }
                if (Map[x, y] == chest)
                {
                    Tilemap.SetTile(Cellposition, Chest);
                }

            }
        }
    }

    //Assigning a character to the multidimensional array's width and height.
    public char[,] multidimensionalArray = new char[15, 10];
    
    //Method that loads a premade map from a text file.
    string LoadPremadeMap(string MyPath)
    {
        
       
        //Allows the text file to be read in Unity.
        string[] myLines = File.ReadAllLines(MyPath);
       
        //An instance of stringbuilder to build the map.
        StringBuilder BuiltString = new StringBuilder();
       
        //Looping through an array giving the length and width of the map to the text file.
        for (int y = 0; y < myLines.Length; y++)
        {
            for (int x = 0; x < myLines[y].Length; x++)
            {
                //Appends each character of the map to a string.
                Map[x, y] = myLines[y][x];
                //Assigns myLines x and y to the string.
                BuiltString.Append(myLines[y][x]);


            }

            //Creates a new line after every row.
            BuiltString.AppendLine();
            
        }
        
        //Returns the Map string to the return value of the method.
        string MapString = BuiltString.ToString();
        
       
        return MapString;


    }


}
