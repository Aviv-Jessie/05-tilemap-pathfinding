using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

 [RequireComponent(typeof(KeyboardMover))]
public class mining : MonoBehaviour
{

    [SerializeField] KeyCode keyCode = KeyCode.Space;
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    [SerializeField] TileBase pickaxeTile = null;
    [SerializeField] TileBase floorTile = null;

    [SerializeField] float timeToFloor = 0.25f;

    KeyboardMover keyboardMover;
    // Start is called before the first frame update
    void Start()
    {
        keyboardMover = GetComponent<KeyboardMover>();
    }

    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode)) {
            
            Vector3 pickaxePosition = Vector3.up;
            switch(keyboardMover.getDirection()){
                case KeyboardMover.Direction.up:
                    pickaxePosition = Vector3.up;
                break;
                case KeyboardMover.Direction.down:
                    pickaxePosition = Vector3.down;
                break;
                case KeyboardMover.Direction.left:
                    pickaxePosition = Vector3.left;
                break;
                case KeyboardMover.Direction.right:
                    pickaxePosition = Vector3.right;
                break;
            }
            
            pickaxePosition = transform.position + pickaxePosition;
            Vector3Int cellPosition = tilemap.WorldToCell(pickaxePosition);

            TileBase tileBase = tilemap.GetTile(cellPosition);
            if (!allowedTiles.Contain(tileBase)) {
                tilemap.SetTile(cellPosition,pickaxeTile);
                StartCoroutine(MakeTileFloor(cellPosition));
            }else{
                Debug.Log("You cannot mining " + tileBase + "!");
            }

        }

    }

      IEnumerator MakeTileFloor(Vector3Int cellPosition) {
            yield return new WaitForSeconds(timeToFloor);
            tilemap.SetTile(cellPosition,floorTile);
      }
}
