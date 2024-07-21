Main Scene: Mars_Scene 

i. Start scene: Mars_Scene 
-The start scene presents the main character, the astronaut, on the planet Mars. 

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
-The start_scene presents the player the start menu that includes starting the game, instructions on how to play the game, 
quitting the game, and the credits.

v. Manifest of which files authored by each teammate
Detail who on the team did what:
Indulekha Ghandikota
- Added animation/movement to the player 
- Added picking up objects to the scene 

Jan Ephraim Nino Tanja 
- Added collectibles to the scene  
- Added homebase to the scene 
- Added HUD functionality to the scene 
- Added sprinting and powerups 

Christian Anthony Tran 
- Added enemies to the scene 
- Iterated on collectibles and random generation

Mimi Pomephimkham 
- Added background noise to the scene 
- Added footstep sounds to the player 

Mariana Mendez 
- Added a start menu (escape button)
- Quit menu to the scene (escape button)
- Added Pause menu
- Added HUD assets and functionality along with Jan

For each team member, list each asset implemented: 
- Indulekha Ghandikota
i. Assets: 
-Terrain Sample Asset Pack: https://assetstore.unity.com/packages/3d/environments/landscapes/terrain-sample-asset-pack-145808
-Outdoor Ground Textures: https://assetstore.unity.com/packages/2d/textures-materials/floors/outdoor-ground-textures-12555 
ii. C# script files: 
-PlayerController.cs, PickUpObjects.cs, PressurePlateControl.cs

- Jan Ephraim Nino Tanja
i. Assets:
-Sci-Fi Styled Modular Pack: https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913
-Food props (Health Collectibles): https://assetstore.unity.com/packages/3d/food-props-163295
ii. C# script files: 
-CollectibleManager.cs, HealthManager.cs, PlayerMetrics.cs, EnvironmentController.cs

- Christian Anthony Tran 
i. Assets:
ii. C# script files: 
-CollectiblesGenerator.cs, EnemyAI.cs, GenerateEnemies.cs

- Mimi Pomephimkham
i. Assets:
-Background Music: https://assetstore.unity.com/packages/audio/music/the-invasion-atmospheric-title-screen-sci-fi-95819
-Footstep Noise: https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879
ii. C# script files: 
-FootstepsController.cs 

- Mariana Mendez
i. Assets: 
-Hud Assest Pack: https://assetstore.unity.com/packages/2d/gui/sci-fi-ui-hud-background-281048
ii. C# script files: 
-PanelMenu.cs, PauseGame.cs, QuitGame.cs, RestartGame.cs, HealthCollectibleUIManage.cs

----------------------------
Resources:
1. Terrain Sample Asset Pack: https://assetstore.unity.com/packages/3d/environments/landscapes/terrain-sample-asset-pack-145808

2. Outdoor Ground Textures: https://assetstore.unity.com/packages/2d/textures-materials/floors/outdoor-ground-textures-12555 

3. Package Manager -> Terrain Tools -> Install 

4. Package Manager -> Cinemachine -> Install

5. Sci-Fi Styled Modular Pack: https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913
   
6. Food props (Health Collectibles): https://assetstore.unity.com/packages/3d/food-props-163295

7. Hud Assest: https://assetstore.unity.com/packages/2d/gui/sci-fi-ui-hud-background-281048

8. Sci-Fi Asset: https://assetstore.unity.com/packages/2d/gui/sci-fi-ui-hud-background-281048

9. Background Music: https://assetstore.unity.com/packages/audio/music/the-invasion-atmospheric-title-screen-sci-fi-95819

10. Footstep Noise: https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879

11. Banana man: https://assetstore.unity.com/packages/3d/characters/humanoids/banana-man-196830

12. Stylized Astronaut (no characters, just materials): https://assetstore.unity.com/packages/3d/characters/humanoids/sci-fi/stylized-astronaut-114298

13. Low Poly Space Alien: https://assetstore.unity.com/packages/3d/environments/sci-fi/free-demo-of-low-poly-space-alien-worlds-3d-asset-pack-258683

14. Stylized cute alien character: https://assetstore.unity.com/packages/3d/characters/creatures/stylized-cute-alien-character-247126

15. Sci Fi Battery Pack: https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-battery-pack-free-19738

16. GUI kit https://assetstore.unity.com/packages/2d/gui/icons/cute-menu-gui-kit-141535

17. Heart https://www.vecteezy.com/vector-art/5644880-red-heart-in-pixel-art-style-8-bit-icon-valentine-s-day-symbol

18. Running Icon https://www.svgrepo.com/svg/189274/running-run

19. Lightning Bolt https://iconduck.com/icons/168309/lightning-bolt

20. Medic Icon https://www.deviantart.com/slithbane/art/Red-Medic-class-icon-263466764

Animations are from https://www.mixamo.com/#/
