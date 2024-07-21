Main Scene: Mars_Scene 
Project Github: https://github.com/OMSCS-Cowboy-Coders/space-rescue

i. Start scene: Mars_Scene 
- The start scene presents the main character, the astronaut, on the planet Mars. 

ii. How to play and what parts of the level to observe technology requirements
- The game begins with the player on the terrain. 
- There are four battery spots in total around the map that contain a battery part for the spaceship that you can collect.
- To move around, you can use the arrow keys (up, right, left). 
- To jump, press spacebar.
- To pick up a spaceship part, press E. To put it down, press Q. You can only pick up one battery at a time. 
- There are hamburgers scattered around the map to increase your health once you get hit
- There are capsule shaped sprint powerups to help you go faster for 3 seconds.
- You can sprint using the shift key, and also press R to use your powerup.
- There are aliens to avoid and touching lava or falling into craters makes you lose health.

iii. Known problem areas/bugs
- Clipping: If you try reasonably hard, then the player can clip through objects when running. 
It should not however be a major detriment to the overall gameplay.
- There is sometimes a delay in the lose screen when falling into the lava at the volcano obstacle. 

iv. Second Scene: StartMenu 
- The start_scene presents the player the start menu that includes starting the game, instructions on how to play the game, 
quitting the game, and the credits.

v. Manifest of which files authored by each teammate
Detail who on the team did what:
Indulekha Ghandikota
- Added movement to the player including: walking forward and to the side (with animation), jumping (including a grounded check), and being able to slide on ice. 
- Added picking up and putting down objects to the scene and integrated that with storing the objects in the final battery storage
- Added camera for the player using Cinemachine
- Created three obstacles (volcano, crater, ice crater) for the player to play through to get the parts
   - Includes making sure player dies or loses health depending on various conditions
   - Added a PhysicsMaterial to the ice so that the player slides on the ice for the ice crater
- Created several compound colliders for various structures as part of the obstacles
- Added prefab for getting the spaceship part
- Added a prefab for compilation of multiple rocks put together
- Helped with integrating the battery pad to the space station door animation (i.e. as batteries are dropped, doors are unlocked and batteries cannot be picked up again once dropped on the battery pad) 

Jan Tanja
- PlayerMetrics singleton to monitor core numbers essential to the player. 
- Added collectibles to the scene
- Wrote HUD logic to monitor power ups and health
- Added core coroutine mechanism for functionality and powerups 
- Win screen, You Lost Screen
- Subgoals (number of stars) measured by the amount of time taken to complete the game
- Progression of difficulty (spawning more enemies over time)
- Alien damage functionality via colliders
- Helped draft Instructions copy for Instructions scene

Christian Anthony Tran 
- Added enemy alien’s AI logic for pathing.
- Added enemy alien’s animator and blend tree.
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
- Added restart button when player clicks escape
- Added quit button when player clicks escape
- Added pause menu
- Added HUD assets
- Created Start Menu Scene
- Created Instructions Scene
- Created Credit Scene
- Added audio to Start Menu
- Created quit menu button in Start Menu

For each team member, list each asset implemented: 
Indulekha Ghandikota
i. Assets: 
- Astronaut (including the idle and running animations): https://assetstore.unity.com/packages/3d/characters/humanoids/sci-fi/stylized-astronaut-114298
- T-pose from Mixamo for Astronaut
- Rocks for various obstacles:
https://assetstore.unity.com/packages/3d/environments/landscapes/mountains-canyons-cliffs-53984
- Textures for various obstacles;
– Lava and Ice: https://assetstore.unity.com/packages/2d/textures-materials/free-stylized-textures-rpg-environment-204187 
– Regular Volcanic Rock: https://assetstore.unity.com/packages/2d/textures-materials/stylized-lava-materials-180943 

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

Ii: Imported Registry Packages:
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
- StartMenu.cs, PauseGame.cs, QuitGame.cs, RestartGame.cs

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
