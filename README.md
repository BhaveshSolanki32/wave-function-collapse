
# wave-function-collapse

This is a Unity project coded in c# that procedurally generates 2D tilemap world using the wave function collapse algorithm. With 2 modes a normal mode and bezier mode.

#### Demo: [Link to the playable demonstration of the project itch.io page](https://bhavesh-solanki.itch.io/aquarium-wave-function-collapse)
## Table of content

- <a href="#About">About</a>
- <a href="#Project-screenshots/GIFs">Screenshots/gifs</a>
- <a href="#Key-features">Key features</a>
- <a href="#Installation">Installation</a>
- <a href="#Usage">Usage</a>
- <a href="#Code-overview">Code overview</a>
- <a href="#Future-plan">Future plan</a>
- <a href="#Acknowledgement">Acknowledgement</a>

<a name="About"></a>
## About

The wave function collapse algorithm allows procedural generation of an image that have constraints to make them locally consistent. This project implements the simple tiled model approach of the algorithm to generate maps with custom tilesets and rules.

There are 2 modes a normal mode where it randomly generates the level and Bezier mode where it collapse a path first by which it almost guarantees an open path so that  no map is created where player can get stuck with no where to go forward to.

The major con of using wfc for real application is it's too random specially simple tile based model. To aid this I created the bezier mode which can collapse a pre defined list of tiles at once.
This is handled by WFCBulkCollapse script which just need a list of tiles to collapse first. The system is completely modular so it is easy to change any feature (different approaches can be used to collapse multiple tiles to create a desired pattern or a level with some constrains).

<a name="Project-screenshots/GIFs"></a>
## Project screenshots/GIFs

![ezgif-5-445a6a7be0](https://github.com/BhaveshSolanki32/wave-function-collapse/assets/66202955/74245424-7298-48f3-9520-681c594f17ff)

![ezgif-5-a482801b62](https://github.com/BhaveshSolanki32/wave-function-collapse/assets/66202955/922bf45a-d44f-4a5f-8d2e-177f744139c3)


<a name="Key-features"></a>
## Key features

* **Grid generation system** for spawning map tiles as gameobjects, with an editor utility script to help generate tileset in editor mode based on given parameters.
* **ScriptableObject tileset data** containing adjacency tile rules which is injected as data container. The scriptable object makes it very easy to manage multiple rule set for different tiles.
* **Wave function collapse system** that performs wfc, which is a completely modular system which allows to easily add new features and modes in system.
* **Modularity and expandability** this project uses SOLID design principle, which makes it easier to maintain the project as it grows and make changes quickly.
* **Bezier mode** which collapse a path open before collapsing other tiles (irrespective of the entropy) which ensures an open path.
* **Fish behavior** after the wave function is collapsed fishes generated with tile are set free to swim across the aquarium.

<a name="Installation"></a>
## Installation

1. Installation is straight forward download the repo.
2. Open using unity hub and add the project.
3. download unity 2021.3.16f1.if not installed.
4.  open the Sample scene in the scene folder.
5. You are all set to use the project.

<a name="Usage"></a>
## Usage

1. Select GridGenerator gameobject, go to inspector window and set the height and width of the grid and click on generate  button, a grid will be generated with grid center at (0,0,0)( world position).
2. Attach WFCTypeHandler script to the Tiles gameobject. Refference the Tiles gameobject in Input manager gameobject.
3. Now create your Tiles and add them to initial tile prefab gameobject, currently the each tile is stored in a Dictionary<string, List<string>> in a ScriptableObject. You can simply add your code based on your constrains, if you want to add current TileData just add a new dictionary item and define tile code, socket codes.
4. Reference you ScriptableObject TilesData to Tiles gameobject where required.
5. You are all set just play the demo with you custom tiles

<a name="Code-overview"></a>
## Code overview


* **GridGenerator** - Handles spawning map tile gameobjects in grid arrangement, with a button made using unity editor utility scripting.
* **GridNodeData** - Holds grid position data for each tile.
* **WFCTypeHandler** - Handles the mode WFC (bezier, normal or test (test mode only collapse one tile.)).
* **SuperStateTile** - Manages available tile type probabilities for each tile.
* **TileUpdateEventHandler** - Event for tile updates between adjacent tiles.
* **WFCTrigger** - Base class for triggering collapse by picking lowest entropy tile.
* **WFCDisplay** - Visualizes tile types as they are selected
* **LowestEntropyTracker** - give the lowest entropy tile(tile with least number of tile left in superpostion.).
* **WFCTilesData** - ScriptableObject holding valid adjacent tile rules
* **WFCInputUIEvent** - Receives UI interaction and send it to respected scripts to make changes.

<a name="Future-plan"></a>
## Future plan

Some potential improvements:

* Support for 3d world generation.
* turning it into full-fledged game with infinite world generation or puzzle generation
* Bulk image mode which can read black and white image data and collapse tile according to that to give a certain pattern in the output and get more control over output.

<a name="Acknowledgement"></a>
## Acknowledgement


 - [Inspiration/original implementation of WFC by Maxim Gumin](https://github.com/mxgmn/WaveFunctionCollapse)
 

