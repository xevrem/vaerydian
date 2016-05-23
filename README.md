# Vaerydian

Vaerydian is an in-work Action RPG written in C# utilizing:
* [MonoGame](http://github.com/mono/MonoGame) - Graphics Framework
* [BehaviorLibrary](http://github.com/NetGnome/BehaviorLibrary) - AI Framework
* [ECSFramework](http://github.com/NetGnome/ECSFramework) - Entity-Component-System based Game Framework
* [AgentComponentBus](http://github.com/NetGnome/AgentComponentBus) - Asynchronous processing framework for ECSFramework
* [Glimpse](http://github.com/NetGnome/Glimpse) - UI Framework for MonoGame & ECSFramework
* [fastJSON](http://fastjson.codeplex.com) - A Very fast JSON parser, used for driving the game with data
* [LibNoise](http://libnoisedotnet.codeplex.com) - a C# implementation of the famous libnoise noise framework, used for procedural content

## Building:
Vaerydian can be compiled under both Windows (MonoGame WindowsGL) and Linux(MonoGame Linux).

### Pre-Build
To build Vaerydian you need to compile all the appropriate libraries beforehand (listed above). The best way to compile them is to use the following order:
* MonoGame, BehaviorLibrary, ECSFramework, LibNoise, fastJSON
* Glimpse, AgentComponentBus
* Vaerydian

### Post-Build
After Vaerydian has been built, copy the Content directory to the build output directory and run the game.

### Dependencies on Linux:
These are dependencies that I have typically had to double-check to install on most Linux distros. There may be others, but the error output of mono & MonoGame is usually good at telling you what is missing.
* Mono 2.10.x
* LibSDL-Image
* OpenAL

## Controls:

### Start Menu
* Click Buttons - start a new game, generate a world map, exit the game
* ESC - exit the game

### New Game
* WASD - movement
* R - target closest enemy
* RightClick - fire bolts at target
* LeftClick - swing melee weapon in direction of reticle
* Enter - descend to next level based on current map position (i.e., generates the next map based on current map & position)
* F12 - go back to previous map
* F6 - dump current map and player configuration to .v files
* ESC - return to start menu
* Others - some other debug commands in here... ;)

### World Map
* Up/Down/Left/Right - movement
* T - show temperature map
* P - show precipitation map
* ESC - return to start menu

## Data

All data that drives the game is stored in

	Content/json/

All files are the format of

	filename.v

At this time I have no guidance for editing the files, so do so at your own risk. Nothing will permanently break anything, but you may want to back these up in case you forget to undo all your changes.

Enjoy!
