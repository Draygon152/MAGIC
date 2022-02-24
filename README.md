# MAGIC!
<pre>
Hello, and welcome to the wonderful world of MAGIC!
To play the current build of this game, a mouse and keyboard are required, but
gamepad usage for gameplay is supported. Gamepads should be plugged in before
pressing "Start Game" from the Main Menu.

If no input devices are selected for either player, only one player character
will spawn, which will be controlled by keyboard controls. If the same input
device is selected for both players, two player characters will spawn, and
will both be controlled by that input device. If at least two input devices
are plugged in and different ones are selected for each player, then two
player characters will spawn, each controlled by a different input device.


MAGIC! is a PvE wave-survival game. You lose when the health of both player characters
is depleted, and you win when you've destroyed every wave of enemies that dares to
approach you.

CONTROLS:
  MENU NAVIGATION:
    - Move the mouse and left-click on buttons!
    
  PLAYER:
    KEYBOARD:
      - W, A, S, D for movement
      - Press Q to cast a spell in the direction the player is facing
      
    GAMEPAD:
      - Left analog stick for movement
      - Left trigger to cast a spell in the direction the player is facing
    
	
NOTES:
  In the second-playtest build of the game, the submenus within the Options menu
  have not yet been implemented. Even when testing with one player, both player
  GUI elements must have an element selected in the Lobby Menu, and both Ready
  checkboxes must be checked for the game to begin. This will be changed in future
  builds. Extra effects for many spells are still a work in progress. The lightning
  spell effect is partially implemented, when the projectile has finished travelling
  its path, it will teleport the player who casted it to the projectile's end location.
  If it collides with an enemy or terrain object, it will teleport the player directly
  onto that collision target. This is expected behavior that will be patched out. Trees
  now have collision boxes, and the pine trees in particular have deceptively large
  colliders. This will be changed in a future build. The borders surrounding the play area
  have been lowered and made visible, to help with player understanding.
  
  For the second playtest, players will challenge three waves of enemies which implement
  NavMesh navigation and more complex AI behaviors. The second wave contains an enemy
  called the Hiskgar, which will begin to regenerate health and run away once it reaches
  20% health. This regeneration stops when it reaches 80%. This regeneration can only
  occur once per Hiskgar, to make it easier to kill the Hiskgar without having to trap it
  in a corner. The third wave contains an enemy called the Nsquelsnaak which will
  periodically spawn Larvae to attack the player, while the Nsquelsnaak continues to wander.
  At some points, it may appear that the enemy counter does not decrease properly. This is
  not the case, and is instead a result of how our game decides when to spawn the next wave.
  When there is only one enemy left, the next wave spawns. When moving from the second wave
  to the third wave, this may give the impression that killing an enemy did not decrease the
  counter properly, when in reality an enemy is spawned right as the second-to last enemy
  of wave two is killed. This will be addressed in a future build for clarity.
</pre>