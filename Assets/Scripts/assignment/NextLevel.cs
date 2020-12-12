using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class NextLevel : MonoBehaviour{
    [SerializeField] TilemapCaveGenerator tilemapCaveGenerator = null;
    [SerializeField] TileBase[] floorTiles = null;
    [SerializeField] TileBase[] wallTiles = null;
    static int i = 0;



    void Start(){
        TileBase floor = floorTiles[i % floorTiles.Length];
        TileBase wall = wallTiles[i % wallTiles.Length];
        
        tilemapCaveGenerator.wallTile = wall;
        tilemapCaveGenerator.floorTile = floor;

        tilemapCaveGenerator.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        i++;
        
        if(other.tag == "Player"){
            TilemapCaveGenerator.gridSize = (int)(TilemapCaveGenerator.gridSize*1.1f);
            SceneManager.LoadScene("assignment");
        }    
    }
}
