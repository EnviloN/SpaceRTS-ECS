# NI-APH Semestral Project

## About:
Simple space RTS, where planets can be controlled by teams. Planets controlled by a team generate starships of that team. 
The player can control the starships of their team and send them to other planets. If the planet is not captured, the starships capture it.
However, if starships of other teams are present, they need to be destroyed first. 

## Characteristics
- **Genre**: RTS
- **Engine**: Unity
- **Environment and Camera**: Pure 2D top-down
- **Objects**: planets, starships, projectiles
- **Actions**: camera movement, unit selection, unit orders
- **Technical mechanics**:
    - Captured planets generate starships over time based on their size.
    - Starships use flocking algorithm to move around, avoiding planets, and colisions between them.
    - Starships use targeting system that allows them to lock on an enemy starship and engage in combat. Flocking simulation is affected by this and starship is trying to pursuit the locked target.
    - Camera movement is done by controlling a camera rig entity in ECS and its transform is applied to the camera game object.
    - Starships can shoot projectiles that destroy enemy units. Friendly fire is off.

## Future ideas
- Planets can exchange several orbiting spaceships (as a form of currency) for a mining orbiter that will increase the starship generation speed. Mining orbiters could potentially by captured or destroyed during the planet capture.
- Dynamic cursor icon based on game state, selection.

## Requirements
Unity version: 2020.1.11f1

Other requirements (packages) should be downloaded by the unity package manager automatically when the project is opened.

## Current state
Player can currently control both teams, so it is more like a sandbox at the moment. The functionality of planet capturing is not implemented.
Flocking behavior, targeting and combat systems have been fully implemented. Same goes for controls, and unit selection.

## Play the game
[Download the development build here!](https://www.dropbox.com/s/wdchy4oxqxr7ely/APH_chudyja1.zip?dl=0)

### Controls:
- Camera Movement: WASD / Arrow Keys / Drag with Middle Mouse Button 
- Camera Zoom: Mouse Wheel
- Camera Movement Boost: Shift
- Unit selection: Left Mouse Button
- Move selected units: Right Mouse Button
- End Game: Alt + F4 ('This Is the Way')

Tired of waiting for more units to spawn in order to view an epic space fight? Try our [extreme version](https://www.dropbox.com/s/1g9t9zlkqgdw13n/APH_chudyja1_extreme.zip?dl=0), where units spawn faster!

## Notes
Sometimes when unity editor with the project is opened (especially for the first time after a computer restart), it has several error messages in the console related to the editor's UI. This can be solved by restarting unity.