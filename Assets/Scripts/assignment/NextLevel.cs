using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class NextLevel : MonoBehaviour{
    [SerializeField] TilemapCaveGenerator tilemapCaveGenerator = null;
    [SerializeField] TileBase[] floorTiles = null;
    [SerializeField] TileBase[] wallTiles = null;
    [SerializeField] TileBase[] grassTiles = null;

    static int i = 0;



    void Start(){
        TileBase floor = floorTiles[i % floorTiles.Length];
        TileBase wall = wallTiles[i % wallTiles.Length];
        TileBase grass = grassTiles[i % grassTiles.Length];

        tilemapCaveGenerator.wallTile = wall;
        tilemapCaveGenerator.floorTile = floor;
        tilemapCaveGenerator.grassTile = grass;

        Debug.Log("I ENABLED !!!");

        tilemapCaveGenerator.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        i++;
        
        if(other.tag == "Player"){
            // increase the number of grid size by 10%.
            TilemapCaveGenerator.gridSize = (int)(TilemapCaveGenerator.gridSize*1.1f);
            SceneManager.LoadScene("assignment");
        }    
    }
}
