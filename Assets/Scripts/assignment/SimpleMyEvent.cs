using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/*
this just simple to show who to register to my evetnr
https://docs.unity3d.com/ScriptReference/Events.UnityEvent.html
*/
public class SimpleMyEvent : MonoBehaviour
{

    [SerializeField] TilemapCaveGenerator tilemapCaveGenerator = null;
    // Start is called before the first frame update
    void Start()
    {
         UnityEvent m_MyEvent = TilemapCaveGenerator.getSimulationCompletedEvent();    
         m_MyEvent.AddListener(Ping);  
    }

    void Ping()
    {
        Debug.Log("Ping from on complate simulation");

        Vector3Int lb = tilemapCaveGenerator.findeLeftBottomFloor();
        if(lb != Vector3Int.left)
            Debug.Log("Left bottom floor is" + lb);
        else
            Debug.Log("not have floor");

        Vector3Int rt = tilemapCaveGenerator.findeRightTopFloor();
        if(rt != Vector3Int.left)
            Debug.Log("right top floor is" + rt);
        else
            Debug.Log("not have floor");

        
        Vector3Int r = tilemapCaveGenerator.pullRandomFloor();
        Debug.Log(r + " is floor");
        r = tilemapCaveGenerator.pullRandomFloor();
        Debug.Log(r + " is floor");
         r = tilemapCaveGenerator.pullRandomFloor();
         Debug.Log(r + " is floor");
    }

   
}
