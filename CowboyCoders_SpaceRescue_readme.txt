Main Scene: Mars_Scene 
Project Github: https://github.com/OMSCS-Cowboy-Coders/space-rescue

i. Start scene: StartMenu 
- The start menu presents the main character, the astronaut, on the planet Mars. 
- Other scenes include the Mars_Scene, Tutorial Scene, and the Game Credits Scene.

ii. How to play:
- The game begins with the player on the terrain. 
- There are four battery spots in total around the map that contain a battery part for the spaceship that you can collect.
- To move around, you can use the arrow keys (up, right, left). 
- To jump, press spacebar.
- To pick up a spaceship part, press E. To put it down, press Q. You can only pick up one battery at a time.
- To pause game, press ESC that brings up a resume, restart, or quit game menu. To resume game, press the ESC button again.
- There are hamburgers scattered around the map to increase your health once you get hit. After 10 seconds, your health can also regenerate automatically. 
- There are capsule shaped sprint powerups to help you go faster for 3 seconds.
- You can sprint using the shift key, and also press R to use your powerup.
- There are aliens to avoid and touching lava or falling into craters makes you lose health. There is also fall damage. 

iii. What to observe in various parts of the game:
3D Feel Game
- Start Menu with options to play the game, see instructions, see credits, and quit the game
- Pause Menu to restart or quit the game when escape is pressed and resumes the game when escape is pressed again 
- There is a Win Screen once you reach the end of the game
- Replayability: The game is based on time. The faster you play, the more stars you earn.
- Lose screen: When the player dies, this screen shows up allowing them to restart the game
- Player respawns at the beginning of the game if they die at any level - level here is defined as increasing each time they have collected/placed a battery piece

Precursors to Fun Gameplay
- The goal is to retrieve four battery spots to power up the space station to open the doors to the space station to retrieve the last battery spot to return back home from Mars. This is communicated in the tutorial scene of the game.
- The player can earn stars based on how quickly they complete the game. 
- Player has to and can engage with multiple ledges throughout the game world to both gather parts and also use these places as a temporary sanctuary against aliens.
- Player is able to get both health and sprint powerups to help them regain health after encountering lava or aliens and also be able to get speed to bypass aliens
- Player must make choices when navigating the game world in terms of how to escape the aliens, and get to the batteries. 
- As each battery part is placed, the number of aliens increases. 

3D Character/Real Time Control
- Player has fluid hybrid root motion and can walk forward and is also able to turn. There is an idle and running animation using a blend tree attached to the player. 
- Player is able to jump and there is a function to validate the player is on the ground to make sure they can only jump when on the ground 
- Player is able to sprint using the shift key and with powerup usage using R.
- Player also has the ability to pick up and put down items (using E and Q respectively). Items are placed down without being thrown around. 
- There is a camera controller using Cinemachine to follow the player during Game Play
- Player has a footstep audio

3D world
- The environment is procedurally generated whereas the terrain is fixed.
- There are several elements synthesized for building the environment that are also procedurally generated including plants, rocks, and trees 
- There are several large environment pieces added to provide the player with various challenges including a volcano, crater, an ice crater and a space station.
- In the space station, each set of doors will open once the player has placed a battery in the battery pad and they have stepped close to the door (i.e. door opens with respect to the proximity of the player)
- Some other aspects of the environment include:
- Compound colliders on some of the rocks and the crater floor throughout the game to minimize clipping and to better handle interactivity with the player
- There is ice on one of the obstacles which has the player being unable to jump and they can only skate off the ice. 
- The space station has a final obstacle where the player has to interact with switches on different platforms to stop lifts
- Pressure plates that have an animation attached so that they reveal the battery part once the player steps on it
- Player jump distance is constrained and consistent. They are also able to take fall damage

AI 
- Multiple AI states have been created for the aliens 
- AI states include idling, patrolling, and following/stalking.
- Once an alien attacks the player, the player loses health. The health system does allow a grace damage period and health regeneration.
- Procedurally generated environment elements act as NavMesh Obstacles and the terrain’s navmesh is built on runtime such that the navmesh agent knows where to move.
- Alien will attack upon entering the range of the player and will walk around otherwise. 

Polish
- Start Menu with options to play the game, see instructions, see credits, and quit the game
- Pause Menu to resume, restart or quit the game when escape is pressed. You can press escape again to resume the game, or click resume game.git 
- There is a health bar and sprint bar to inform the player how much health they have and their sprint usage
- Background music throughout the game
- Cohesive style between the start menu and the HUD

iv. Known problem areas/bugs
- Clipping: If you try reasonably hard, then the player can clip through objects when running. 
It should not however be a major detriment to the overall gameplay.
- There is sometimes a delay in the lose screen when falling into the lava at the volcano obstacle. 

v. Second Scene: StartMenu 
- The start_scene presents the player the start menu that includes starting the game, instructions on how to play the game, 
quitting the game, and the credits.

vi. Manifest of which files authored by each teammate
Detail who on the team did what:
Indulekha Ghandikota
- Added movement to the player including: walking forward and to the side (with animation), jumping (including a grounded check), and being able to slide on ice. 
- Added picking up and putting down objects to the scene and integrated that with storing the objects in the final battery storage
- Added camera for the player using Cinemachine
- Created three obstacles (volcano, crater, ice crater) for the player to play through to get the parts
   - Includes making sure player dies or loses health depending on various conditions like falling of the volcano or stepping on lava
   - Added a PhysicsMaterial to the ice so that the player slides on the ice for the ice crater
- Created several compound colliders for various structures as part of the obstacles
- Added prefab for getting the spaceship part
- Added a prefab for compilation of multiple rocks put together
- Helped with integrating the battery pad to the space station door animation (i.e. as batteries are dropped, doors are unlocked and batteries cannot be picked up again once dropped on the battery pad) 

Jan Ephraim Nino Tanja
- PlayerMetrics singleton to monitor core numbers essential to the player. 
- Added collectibles to the scene
- Wrote HUD logic to monitor power ups and health
- Added core coroutine mechanism for functionality and powerups 
- Win screen, You Lost Screen
- Subgoals (number of stars) measured by the amount of time taken to complete the game
- Progression of difficulty (spawning more enemies over time)
- Alien damage functionality via colliders
- state machine for alien AI
- Added pause menu functionality

Christian Anthony Tran 
- Added enemy alien AI logic for pathing.
- Added enemy alien animator and blend tree.
- Added enemy state machine such as patrolling and chasing.
- Added zombie-like animation for enemy aliens
- Added functionality to procedurally generate enemy aliens and randomize their assets, position, scaling, and attaching relevant components before being added as a gameobject in runtime.
- Added functionality to procedurally generate collectibles objects and randomize their position and scaling before being added as a gameobject in runtime.
- Added functionality to procedurally generate environmental objects and randomize their position and scaling before being added as a gameobject in runtime.
- Added coroutine to increase the number of enemies as time passes or difficulty increases.
- Added coroutine to increase the number of collectibles as time passes.
- Added procedurally generated terrain data
- Added functionality to procedurally build NavMesh at runtime start.
- Added health regeneration and damage grace period
- Added fall damage for player

Mimi Pomephimkham 
- Added footstep controller to player 
- Added footstep sound to player
- Added background noise to environment 
- Added space station to the environment 
- Created switches for moving platforms prefab to space station 
- Created moving platforms prefab and animations to space station
- Added door animations and logic for space station and battery storage spots 
- Added floor colliders to improve UI between player and doors when player places a battery on the battery storage spot 

Mariana Mendez 
- Added HUD assets
- Created Start Menu Scene
- Created Instructions Scene
- Created Credit Scene

For each team member, list each asset implemented: 
Indulekha Ghandikota
i. Assets: 
- Astronaut (including the idle and running animations): https://assetstore.unity.com/packages/3d/characters/humanoids/sci-fi/stylized-astronaut-114298
- T-pose from Mixamo for Astronaut
- Rocks for various obstacles:
https://assetstore.unity.com/packages/3d/environments/landscapes/mountains-canyons-cliffs-53984
- Textures for various obstacles;
–Lava and Ice: https://assetstore.unity.com/packages/2d/textures-materials/free-stylized-textures-rpg-environment-204187 
–Regular Volcanic Rock: https://assetstore.unity.com/packages/2d/textures-materials/stylized-lava-materials-180943 

ii. C# script files: 
- PlayerController.cs, PickUpObjects.cs, PressurePlateControl.cs 

Jan Ephraim Nino Tanja
i. Assets:
- Sci-Fi Styled Modular Pack: https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913
- Food props (Health Collectibles): https://assetstore.unity.com/packages/3d/food-props-163295
- Heart https://www.vecteezy.com/vector-art/5644880-red-heart-in-pixel-art-style-8-bit-icon-valentine-s-day-symbol
- Running Icon https://www.svgrepo.com/svg/189274/running-run
- Lightning Bolt https://iconduck.com/icons/168309/lightning-bolt
- Medic Icon https://www.deviantart.com/slithbane/art/Red-Medic-class-icon-263466764

ii. C# script files: 
- CollectibleManager.cs, HealthManager.cs, PlayerMetrics.cs, EnvironmentController.cs, PanelMenu.cs, HealthCollectibleUIManager.cs

Christian Anthony Tran 
i. Assets:
- Stylized Cute Alien: https://assetstore.unity.com/packages/3d/characters/creatures/stylized-cute-alien-character-247126
- Zombie Animation: https://assetstore.unity.com/packages/3d/animations/zombie-animation-pack-free-150219

ii. C# script files: 
- EnemyAI.cs, AlienMotionController.cs, CollectiblesGenerator.cs, GenerateEnemies.cs, GenerateTerrainAssets.cs, TerrainGenerator.cs

iii: Imported Registry Packages:
- NavMesh Building Components: https://docs.unity3d.com/Packages/com.unity.ai.navigation@1.0/manual/index.html

Mimi Pomephimkham
i. Assets:
- Background Music: https://assetstore.unity.com/packages/audio/music/the-invasion-atmospheric-title-screen-sci-fi-95819
- Footstep Noise:  https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879

ii. C# script files: 
- DoorOpen.cs, FinalRoomEntry.cs, FinalRoomExit.cs, FloorCollider.cs, FootstepsController.cs, MovingPlatform.cs, SwitchButton.cs 

Mariana Mendez
i. Assets: 
- Hud Assest Pack:
https://assetstore.unity.com/packages/2d/gui/sci-fi-ui-hud-background-281048
ii. C# script files: 
- StartMenu.cs, RestartGame.cs

----------------------------
Resources:
1. Terrain Sample Asset Pack: https://assetstore.unity.com/packages/3d/environments/landscapes/terrain-sample-asset-pack-145808 

2. Outdoor Ground Textures: https://assetstore.unity.com/packages/2d/textures-materials/floors/outdoor-ground-textures-12555   

3. Package Manager -> Terrain Tools -> Install 

4. Package Manager -> Cinemachine -> Install

5. Sci-Fi Styled Modular Pack: https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913 
   
6. Food props (Health Collectibles): https://assetstore.unity.com/packages/3d/food-props-163295

7. Hud Asset: https://assetstore.unity.com/packages/2d/gui/sci-fi-ui-hud-background-281048

8. Background Music: https://assetstore.unity.com/packages/audio/music/the-invasion-atmospheric-title-screen-sci-fi-95819

9. Footstep Noise: https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879

10. Stylized Astronaut (no characters, just materials): https://assetstore.unity.com/packages/3d/characters/humanoids/sci-fi/stylized-astronaut-114298

11. Low Poly Space Alien: https://assetstore.unity.com/packages/3d/environments/sci-fi/free-demo-of-low-poly-space-alien-worlds-3d-asset-pack-258683 

12. Stylized cute alien character: https://assetstore.unity.com/packages/3d/characters/creatures/stylized-cute-alien-character-247126

13. Sci Fi Battery Pack: https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-battery-pack-free-19738

14. Sci-Fi Asset for HUD: https://assetstore.unity.com/packages/2d/gui/sci-fi-ui-hud-background-281048

15. Heart https://www.vecteezy.com/vector-art/5644880-red-heart-in-pixel-art-style-8-bit-icon-valentine-s-day-symbol

16. Running Icon https://www.svgrepo.com/svg/189274/running-run

17. Lightning Bolt https://iconduck.com/icons/168309/lightning-bolt

18. Medic Icon https://www.deviantart.com/slithbane/art/Red-Medic-class-icon-263466764

19. Rocks for various obstacles:: https://assetstore.unity.com/packages/3d/environments/landscapes/mountains-canyons-cliffs-53984

20. Textures for various obstacles;
Lava and Ice: https://assetstore.unity.com/packages/2d/textures-materials/free-stylized-textures-rpg-environment-204187 
Regular Volcanic Rock: https://assetstore.unity.com/packages/2d/textures-materials/stylized-lava-materials-180943 

21. T-pose for player is from Mixamo 

22. Zombie Animation: https://assetstore.unity.com/packages/3d/animations/zombie-animation-pack-free-150219 
