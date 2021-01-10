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


## Current state
TODO

## Build download
TODO

## Requirements
TODO

## Notes
TODO