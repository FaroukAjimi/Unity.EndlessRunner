# Endless Runner Game

## Overview

This hyper-casual game follows the endless runner concept, taking inspiration from popular games like Subway Surfers. The game comprises two main components: the pathway and the player capsule.

### Pathway

The pathway is dynamically generated endlessly as the player continues to run. To prevent the player from leaving the pathway, a pathway block prefab is implemented with colliders on both sides. This prefab includes three lane markers (center, right, and left) to represent the train lanes, reminiscent of Subway Surfers.

### Player Capsule

The player capsule is equipped with a CharacterController component and a Controller Script. The game begins with the player on the center lane. The Controller script is responsible for maintaining continuous forward translation. It ensures the player stays within the three lanes using magnetizers that release or attract the player when changing directions.

## How to Play

The objective is to run as far as possible without colliding with obstacles. Players can move left or right with left and right arrows , jump with space, and slide by down arrow. The game concludes when the player hits an obstacle.

## Features

- **Endless Gameplay:** The game offers an infinite and dynamically generated pathway.
- **Three Lanes:** Players navigate through three distinct lanes within the pathway.
- **Obstacles:** Various obstacles challenge players, and colliding with them ends the game.

## Instructions for Use

To set up the game, follow these steps:

1. Clone or download the repository.
2. Open the project in Unity.
3. Adjust pathway and player settings as needed.
4. Build and run the game to experience endless runner gameplay.

Feel free to explore and modify the code to suit your preferences or add new features to enhance the gaming experience. Enjoy the endless runner journey!

## Video Preview

[![Video Title](http://img.youtube.com/vi/Bzzk3kU_UjM/0.jpg)](http://www.youtube.com/watch?v=Bzzk3kU_UjM)


