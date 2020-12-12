using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys.
 */
 [RequireComponent(typeof(SpriteRenderer))]
public class KeyboardMover : MonoBehaviour {

    public enum Direction{up,down,left,right}
    Direction direction;

    [SerializeField] Sprite TileArrowUp = null;
    [SerializeField] Sprite TileArrowDown = null;
    [SerializeField] Sprite TileArrowLeft = null;
    [SerializeField] Sprite TileArrowRight = null;

    private SpriteRenderer spriteRenderer;


    public Direction getDirection(){
        return direction;
    }

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = TileArrowUp;
        direction = Direction.up;
    }

    protected Vector3 NewPosition() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            spriteRenderer.sprite = TileArrowLeft;
            direction = Direction.left;
            return transform.position + Vector3.left;            
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
             spriteRenderer.sprite = TileArrowRight;
             direction = Direction.right;
            return transform.position + Vector3.right;           
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            spriteRenderer.sprite = TileArrowDown;
            direction = Direction.down;
            return transform.position + Vector3.down;             
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            spriteRenderer.sprite = TileArrowUp;
            direction = Direction.up;
            return transform.position + Vector3.up;            
        } else {
            return transform.position;
        }
    }


    void Update()  {
        transform.position = NewPosition();
    }
}
