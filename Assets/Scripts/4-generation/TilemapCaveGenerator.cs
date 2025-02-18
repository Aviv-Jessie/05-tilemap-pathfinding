using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using UnityEngine.Events;


/**
 * This class demonstrates the CaveGenerator on a Tilemap.
 * 
 * By: Erel Segal-Halevi
 * Since: 2020-12
 * 
 * 
 */

public class TilemapCaveGenerator: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;

    [Tooltip("The tile that represents a wall (an impassable block)")]
    [SerializeField]public TileBase wallTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField]public TileBase floorTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField] public TileBase grassTile = null;


    [Tooltip("The percent of walls in the initial random map")]
    [Range(0, 1)]
    [SerializeField] float randomFillPercent = 0.5f;


    [Tooltip("How many steps do we want to simulate?")]
    [SerializeField] int simulationSteps = 20;

    [Tooltip("For how long will we pause between each simulation step so we can look at the result?")]
    [SerializeField] float pauseTime = 1f;

    [Tooltip("Length and height of the grid")]
    public static int gridSize = 20;

    private CaveGenerator caveGenerator;

    // Random variable
    private System.Random rand = new System.Random();

    // Array list that hold the legal floors that players can walk on
    private ArrayList CacheFloorList = null;


    // we save this points for the game objects
    // variable that represent the left bottom point
    private Vector3Int CacheLeftBottom = Vector3Int.down;

    // variable that represent the right top bottom
    private Vector3Int CacheRightTop = Vector3Int.down;


    //event when SimulationCompleted.
    private static UnityEvent onSimulationCompletedEvent;

    // Unity event call when we finish to create to map, after all the smothoes
    public static UnityEvent getSimulationCompletedEvent(){
        if (onSimulationCompletedEvent == null)
            onSimulationCompletedEvent = new UnityEvent();
        return onSimulationCompletedEvent;
    }


    void Start()  {
        //To get the same random numbers each time we run the script
        Random.InitState(100);

        caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
        caveGenerator.RandomizeMap();
                
        //For testing that init is working
        GenerateAndDisplayTexture(caveGenerator.GetMap());
            
        //Start the simulation
        StartCoroutine(SimulateCavePattern());

        // init the floors variables
        if(CacheFloorList != null)
            CacheFloorList.Clear();
        CacheFloorList = null;
        CacheLeftBottom = Vector3Int.down;
        CacheRightTop = Vector3Int.down;
    }


    //Do the simulation in a coroutine so we can pause and see what's going on
    private IEnumerator SimulateCavePattern()  {
        for (int i = 0; i < simulationSteps; i++)   {
            yield return new WaitForSeconds(pauseTime);

            //Calculate the new values
            caveGenerator.SmoothMap();

            //Generate texture and display it on the plane
            GenerateAndDisplayTexture(caveGenerator.GetMap());
        }

        // Invoke my event
        if (onSimulationCompletedEvent == null)
            onSimulationCompletedEvent = new UnityEvent();
        onSimulationCompletedEvent.Invoke();
        Debug.Log("Simulation completed!");
    }


    //Generate a floor types - wall, grass, floor texture. depending on the pixel position
    //Display the texture on a plane
    private void GenerateAndDisplayTexture(int[,] data) {
        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                var position = new Vector3Int(x, y, 0);
                var tile = grassTile;
                var dataXY = data[x, y];


                // 1 = wall = blue
                // 2 = grass = green
                // 0 = floor = brown
                if(dataXY == 1){
                    tile = wallTile;
                }else if(dataXY == 0){
                    tile = floorTile;
                }else{
                    tile = grassTile;
                }
                tilemap.SetTile(position, tile);
            }
        }
    }

    //find closer floor to checkCell.
    // Vector3Int.down represents a "-1" variable. that we dont calculeted it yet. 
    // Vector3Int.left represents a "-1" variable. we dont found the floor on the map
    private Vector3Int findeCloserFloor(Vector3Int checkCell){
         int[,] data = caveGenerator.GetMap();
        Vector3Int oldCell = Vector3Int.left;
        int oldMinDistance = System.Int32.MaxValue;

         for(int i=0; i<gridSize;i++)
                for(int j=0; j<gridSize ;j++)
                    if(isFloor(data[i, j])){
                        Vector3Int newCell = new Vector3Int(i,j,0);
                        int newMinDistance = distance(newCell,checkCell);
                        if(newMinDistance < oldMinDistance){
                            oldMinDistance = newMinDistance;
                            oldCell = newCell;
                        }
                    }

        return oldCell; 
    }


    // Method to check the distance
    private int distance(Vector3Int newCell,Vector3Int checkCell){
        return System.Math.Abs(newCell.x - checkCell.x) + System.Math.Abs(newCell.y - checkCell.y);
    }


    // find left bottom tile that player can walk on.
    // Vector3Int.down represents a "-1" variable. that we dont calculeted it yet. 
    // Vector3Int.left represents a "-1" variable. we dont found the floor on the map
    public Vector3Int findeLeftBottomFloor(){
        if(CacheLeftBottom == Vector3Int.down){
            CacheLeftBottom = findeCloserFloor(Vector3Int.zero);
        }    
        return CacheLeftBottom;                                
    }

    // same same like "findeLeftBottomFloor" but at another position.
    public Vector3Int findeRightTopFloor(){
        Vector3Int RightTopCell = new Vector3Int(gridSize-1,gridSize-1,0);
        if(CacheRightTop == Vector3Int.down){
            CacheRightTop = findeCloserFloor(RightTopCell);
        }    
        return CacheRightTop;                     
    }


    // pull random legal tile that player can walk on.
    // return Vector3Int.left represents a "-1" variable. we dont found the floor on the map
    public Vector3Int pullRandomFloor(){
        if(CacheFloorList == null){
            CacheFloorList = new ArrayList();
            int[,] data = caveGenerator.GetMap();
            for(int i=0; i<gridSize;i++)
                for(int j=0; j<gridSize ;j++)
                    if(isFloor(data[i,j]))
                        CacheFloorList.Add(new Vector3Int(i,j,0));
        }        
        
        int index = rand.Next(CacheFloorList.Count);
        Vector3Int v = (Vector3Int)CacheFloorList[index];
        CacheFloorList.Remove(index);

        if(CacheFloorList.Count > 0)
            return v;
        else
            return Vector3Int.left;
    }


    // Method to check if this is legal floor that i can walk on.
    private bool isFloor(int typeFloor){
        if(typeFloor == 2 || typeFloor == 0){
            return true;
        }
        return false;
    }

}
