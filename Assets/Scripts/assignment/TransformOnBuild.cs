using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**Transform game object when ganeration simulation complate*/
public class TransformOnBuild : MonoBehaviour
{
    enum When{rightTop,leftBottom,random}

    [Tooltip("when to transform")]
    [SerializeField] When when = When.random;
    [Tooltip("to get floor cell")]
    [SerializeField] TilemapCaveGenerator tilemapCaveGenerator = null;
    [Tooltip("to convet cell to position")]
    [SerializeField] GridLayout gridLayout = null;
    // Start is called before the first frame update
    void Start()
    {
        UnityEvent m_MyEvent = TilemapCaveGenerator.getSimulationCompletedEvent();    
         m_MyEvent.AddListener(onGanerationComplate);  
    }

    void onGanerationComplate(){
        Vector3Int cellTo = Vector3Int.left;
        switch(when){
            case When.random:
                cellTo = tilemapCaveGenerator.pullRandomFloor();
            break;
            case When.leftBottom:
                cellTo = tilemapCaveGenerator.findeLeftBottomFloor();
            break;
            case When.rightTop:
                cellTo = tilemapCaveGenerator.findeRightTopFloor();
            break;
        }

        // if there no floor- returned Vector.left (-1). Error.
        if(cellTo != Vector3Int.left){
            //gridLayout.cellSize/2 to transform on cell center and not on gride.
            transform.localPosition = gridLayout.CellToWorld(cellTo) + gridLayout.cellSize/2;
        }else{
            Debug.Log("cell not exist");
        }
            
    }
}
