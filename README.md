Endless Runner Game
This is a hyper-casual game that follows the endless runner concept. The game is inspired by Subway Surfers, which is a popular game in the market.
Technical Approach
The game has two main components: the pathway and the player capsule.
Pathway
The pathway is generated endlessly as long as the player is still running. To ensure that the player never leaves the pathway, a pathway block prefab is created with colliders on the left and right. The prefab also has three lane markers (center, right, and left) that represent the train lanes in Subway Surfers.
Player Capsule
The player capsule has a CharacterController component and a Controller Script. The player gameObject starts on the center lane, and the Controller script is responsible for keeping the endless forward translation. The script also keeps the player on the three lanes by using magnetizers that release or attract the player every time they change directions.
How to Play
The objective of the game is to run as far as possible without hitting any obstacles. The player can move left or right by swiping the screen. The player can also jump by tapping the screen and slide by swiping down. The game ends when the player hits an obstacle.
Features
The game has the following features:
Endless gameplay
Three lanes
Obstacles
Installation
To install the game, download the APK file from the official website.


