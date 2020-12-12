using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using UnityEngine.Events;


/**
 * This class demonstrates the CaveGenerator on a Tilemap.
 * 
 * By: Erel Segal-Halevi
 * Since: 2020-12
 */

public class TilemapCaveGenerator: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;

    [Tooltip("The tile that represents a wall (an impassable block)")]
    [SerializeField]public TileBase wallTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField]public TileBase floorTile = null;

    [Tooltip("The percent of walls in the initial random map")]
    [Range(0, 1)]
    [SerializeField] float randomFillPercent = 0.5f;

    
  

    [Tooltip("How many steps do we want to simulate?")]
    [SerializeField] int simulationSteps = 20;

    [Tooltip("For how long will we pause between each simulation step so we can look at the result?")]
    [SerializeField] float pauseTime = 1f;

    [Tooltip("Length and height of the grid")]
    public static int gridSize = 20;
    //event when SimulationCompleted.
    private static UnityEvent onSimulationCompletedEvent;

    public static UnityEvent getSimulationCompletedEvent(){
        if (onSimulationCompletedEvent == null)
            onSimulationCompletedEvent = new UnityEvent();
        return onSimulationCompletedEvent;
    }

    private CaveGenerator caveGenerator;

    void Start()  {
        //To get the same random numbers each time we run the script
        Random.InitState(100);

        caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
        caveGenerator.RandomizeMap();
                
        //For testing that init is working
        GenerateAndDisplayTexture(caveGenerator.GetMap());
            
        //Start the simulation
        StartCoroutine(SimulateCavePattern());

        //
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

        //Invoke my event
        if (onSimulationCompletedEvent == null)
            onSimulationCompletedEvent = new UnityEvent();
        onSimulationCompletedEvent.Invoke();
        Debug.Log("Simulation completed!");
    }



    //Generate a black or white texture depending on if the pixel is cave or wall
    //Display the texture on a plane
    private void GenerateAndDisplayTexture(int[,] data) {
        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                var position = new Vector3Int(x, y, 0);
                var tile = data[x, y] == 1 ? wallTile: floorTile;
                tilemap.SetTile(position, tile);
            }
        }
    }

    //find closer floor to checkCell. return Vector3Int.left if not have.
    private Vector3Int findeCloserFloor(Vector3Int checkCell){
         int[,] data = caveGenerator.GetMap();
        Vector3Int oldCell = Vector3Int.left;
        int oldMinDistance = System.Int32.MaxValue;

         for(int i=0; i<gridSize;i++)
                for(int j=0; j<gridSize ;j++)
                    if(data[i,j] == 0){
                        Vector3Int newCell = new Vector3Int(i,j,0);
                        int newMinDistance = distance(newCell,checkCell);
                        if(newMinDistance < oldMinDistance){
                            oldMinDistance = newMinDistance;
                            oldCell = newCell;
                        }
                    }

        return oldCell; 
    }

    private int distance(Vector3Int newCell,Vector3Int checkCell){
        return System.Math.Abs(newCell.x - checkCell.x) + System.Math.Abs(newCell.y - checkCell.y);
    }

   //Vector3Int.down not finde yet.
    private Vector3Int CacheLeftBottom = Vector3Int.down;
     //finde left bottom tile that player can walk on.
    //return Vector3Int.left if not have.
    public Vector3Int findeLeftBottomFloor(){
        if(CacheLeftBottom == Vector3Int.down){
            CacheLeftBottom = findeCloserFloor(Vector3Int.zero);
        }    
        return CacheLeftBottom;                                
    }

    //Vector3Int.down not finde yet.
    private Vector3Int CacheRightTop = Vector3Int.down;
    //like findeLeftBottomFloor just Right Top
     public Vector3Int findeRightTopFloor(){
        Vector3Int RightTopCell = new Vector3Int(gridSize-1,gridSize-1,0);
        if(CacheRightTop == Vector3Int.down){
            CacheRightTop = findeCloserFloor(RightTopCell);
        }    
        return CacheRightTop;                     
    }

    //pull random tile that player can walk on.
    //return Vector3Int.left if not have.
    private System.Random rand = new System.Random();
    private ArrayList CacheFloorList = null; 
    public Vector3Int pullRandomFloor(){
        
        if(CacheFloorList == null){
            CacheFloorList = new ArrayList();
            int[,] data = caveGenerator.GetMap();
            for(int i=0; i<gridSize;i++)
                for(int j=0; j<gridSize ;j++)
                    if(data[i,j] == 0)
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
}
