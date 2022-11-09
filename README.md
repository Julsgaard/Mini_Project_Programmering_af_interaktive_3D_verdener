# Mini_Project_Programmering_af_interaktive_3D_verdener
**Kristian Julsgaard - Mini Project**


**Overview of the Game:**

The idea of the project is a horror game with some of the same game mechanics as Slenderman. The player needs to locate and collect four dolls. The first doll will always be in the first room where the player spawns. The other three dolls will be at random buildings around the map. When collecting the second doll the first enemies will be instantiated. The enemies are instantiated faster the more dolls are collected. If the player gets too close to a ghoul it will run after the player and the player will have to shoot the ghouls twice to kill them. If the ghoul touches the player, a jump scare will be shown, and the player will have to start over. 

When the player has collected four dolls, they must escape by finding the exit gate. As soon as the fourth doll is collected all the new ghouls instantiated will run directly to the player which makes the game a lot harder. The genres of the game are First-Person Shooter and Horror.


**The main parts of the game are:**

•	Player – Moved with the WASD or arrow keys on the keyboard. The player can jump with the Space key, turn on and off his flashlight with the T key and shoot the gun using the left mouse button. 

•	Camera – First person camera, the camera can be moved freely left and right, but is limited up and down to only go between 80 and -80 degrees. A gun is attached to the camera, it has six bullets and can be reloaded with the key R. 

•	Dolls – The player needs to collect four dolls to escape. Dolls are collected with the E key. There are four dolls active in the scene when the game starts.

•	Enemies – Ghouls run after the player if the player gets close enough. Ghouls can be killed by shooting them twice.

•	Buildings – There are six buildings on the map where the dolls can be.

•	Play area – There are fences around the play area.

•	Exit gate – The player needs to stay in the exit gates collider for 3 seconds to escape.


**Game features:**

•	Ghouls are instantiated faster the more dolls are collected.

•	Ten of the ghouls are predetermined, they are SetActive as soon as the second doll is picked up.

•	Ghouls will only run after the player if the player gets too close to the ghoul.

•	When the last doll is picked up, all the new ghouls will run directly to the player.

•	Dolls can spawn at various locations on the map.


**Project Parts:**

**•	Scripts:**

o	GameManager – Controls the Menu and the UI of the game. It also keeps track of the dolls collected by the player.

o	SoundManager – Controls the music of the game.

o	PlayerController – The PlayerController script controls the player using CharacterController. The script makes the player move with the keyboard, use the mouse to rotate the camera, shoot using ray casting, reload the gun and turn the flashlight on and off.

o	Enemy – Uses NavMesh to navigate to the player, takes damage when shot by the player.

o	SpawnEnemies – Instantiates ghouls at random positions on the map.

o	SpawnDolls – SetActive three random dolls. 

o	CutsceneManager – When the player dies the CutsceneManager is used to switch back to the original scene.

o	VictoryManager – Shows the Victory UI animation and switches scene after 10 seconds.

o	RockingChairTrigger – Opens the second door with an animation and plays sounds.

o	UIButtonSounds – Sounds when mouse clicks or hovers over a button on the Menu.


**•	Models & Prefabs:**

o	Abandoned buildings: https://assetstore.unity.com/packages/3d/environments/abandoned-buildings-62875

o	Fence around the play area: https://assetstore.unity.com/packages/3d/chainlink-fences-73107

o	Ghoul: https://assetstore.unity.com/packages/3d/characters/ghoul-zombie-114531

o	Horror Assets – Doll, rocking chair and more: https://assetstore.unity.com/packages/3d/props/horror-assets-69717

o	Horror menu and ambient music: https://assetstore.unity.com/packages/audio/sound-fx/horror-elements-112021

o	Trees: https://assetstore.unity.com/packages/3d/vegetation/trees/mobile-tree-package-18866

o	Pine tree: https://assetstore.unity.com/packages/3d/vegetation/trees/realistic-pine-tree-pack-232166

o	Abandoned Slaughterhouse: https://assetstore.unity.com/packages/3d/environments/urban/modular-abandoned-slaughterhouse-lite-58082

o	Hospital models: https://assetstore.unity.com/packages/3d/environments/pbr-hospital-horror-pack-free-80117

o	Gun: https://assetstore.unity.com/packages/3d/props/guns/revolver-gun-low-poly-221659

o	Shed: https://assetstore.unity.com/packages/3d/environments/urban/the-shed-10303

o	Exit gate: https://assetstore.unity.com/packages/3d/environments/roadways/vehicle-parking-lot-garage-gate-pbr-111423

o	Muzzle flash and bullet impacts: https://assetstore.unity.com/packages/vfx/particles/war-fx-5669

o	Grass and dirt texture: https://assetstore.unity.com/packages/2d/textures-materials/floors/yughues-free-ground-materials-13001

o	Skybox: https://assetstore.unity.com/packages/3d/environments/planets-of-the-solar-system-3d-90219


**•	Materials:**

o	Only materials from the assets were used in the game. 


**•	Scenes:**

o	Scene1 – The main scene with the map and Menu.

o	Death Cutscene – Scene is loaded if the player dies. When the scene is loaded a jump scare will appear.

o	Victory – Scene is loaded if the player escapes through the exit gate.


**•	Testing:**

o	The game was tested on Windows and does not work on mobile platforms.


**Time Management - Task	Time it Took (in hours)**

Setting up Unity, making a project in GitHub	0.5

Research and conceptualization of game idea	1

Making Player Controller	3.5

Pixelated Rendering	0.5

Enemy (NavMesh)	3

Searching for assets	2

Shooting and reloading	2.5

Level design (Terrain, trees, painting texture on map, buildings, fog)  	10

Menu and UI	2

Spawning dolls	2

Spawning enemies	2

Animations	2

Sounds and music	2

Victory scene and death scene	1

Playtesting and bug fixing	4	

Code documentation	2

All	40 Hours


**Used Resources**

•	Pixelated rendering: https://youtu.be/Sru8XDwxC3I

•	First person movement with keyboard and camera rotation with mouse: https://youtu.be/_QajrabyTJc and http://gyanendushekhar.com/2020/02/06/first-person-movement-in-unity-3d/ 

•	Shooting with raycast: https://youtu.be/THnivyG0Mvo
