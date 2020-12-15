# Unity week 5: Two-dimensional scene-building and path-finding

A project with step-by-step scenes illustrating how to construct a 2D scene using tilemaps,
and how to do path-finding using the BFS algorithm.

## we chose 4 sections: 4,5,6,10.


**section 4:**

we changed the caveGenerator script - so it will work with 3 different components.

and the TileMapGenerator accordingly

Path:

/Assets/Scripts/4-generation/caveGenerator

/Assets/Scripts/4-generation/TileMapCaveGenerator

**section 5:**

Mining script- Assets/Scripts/assignment/mining.cs

the script changes wall by pickaxe picture, waits T time, and changes to the grass.

KeyboardMover script- Assets/Scripts/2-player/KeyboardMover.cs

we added directions

TilemapCaveGenerator- Assets/Scripts/4-generation/TilemapCaveGenerator

3 algorithms to find tile locations

Unity event- https://docs.unity3d.com/ScriptReference/Events.UnityEvent.html

gridSize variable - changed to static variable.

**Pictures:** 

arrow pictures:

Assets/Tiles/arrow_left.asset

Assets/Tiles/arrow_right.asset

Assets/Tiles/arrow_up.asset

Assets/Tiles/arrow_down.asset

tile pickaxe- for mining in to the mountains 

mine picture :Assets/Tiles/pickax.asset

**section 6:**

TransformForBuild script- Assets/Scripts/assignment/TransformOnBuild.cs

works with every game object, and transform the object to floor position. (legal floor)

NextLevel script- Assets/Scripts/assignment/NextLevel.cs

works with the gate- and sends the player to the next level.

**section 10:**

according to www.mybandonware.com- we chose a tile pictures and work with them.

https://www.myabandonware.com/game/rpg-maker-xp-d31

the tiles pictures:

Assets/Tiles/floor1.asset

Assets/Tiles/floor2.asset

Assets/Tiles/floor3.asset


Assets/Tiles/grass1.asset

Assets/Tiles/grass2.asset

Assets/Tiles/grass3.asset


## Instructions:
Space- Mining
arrows- to move
change the zoom- S\W 

Your goal- go to the gate at the right top corner of the map.


## Credits

assignment solver-
Aviv, Jessica and Omri.

Graphics:
* [Ultima 4 Graphics](https://github.com/jahshuwaa/u4graphics) by Joshua Steele.

Online course:
* [Unity 2D](https://www.udemy.com/course/unitycourse/learn/lecture/10246496), a Udemy course by Gamedev.tv.
* [Unity RPG](https://www.gamedev.tv/p/unity-rpg/?product_id=1503859&coupon_code=JOINUS).

Procedural generation:
* [Habrador - Unity Programming Patterns](https://github.com/Habrador/Unity-Programming-Patterns#7-double-buffer)

Programming:
* Erel Segal-Halevi
