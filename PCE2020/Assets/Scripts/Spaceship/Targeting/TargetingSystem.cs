using Assets.Scripts.Quadrants;
using Assets.Scripts.Spaceship.Flocking;
using Assets.Scripts.Tags;
using Assets.Scripts.Teams;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Targeting {
    /// <summary>
    /// System handling enemy starship targeting.
    /// </summary>
    /// <remarks>
    /// This system needs to be updated after the <c>QuadrantSystem</c>, because it needs the
    /// quadrant system to be built.
    /// </remarks>
    [UpdateAfter(typeof(QuadrantSystem))]
    public class TargetingSystem : SystemBase {
        /// <summary>
        /// Handles the starship's targeting.
        /// </summary>
        protected override void OnUpdate() {
            // Note that we need to pass the quadrant system structure to WithReadOnly(). This is because other systems are
            // reading from it in parallel and we need to ensure the safety system that we will not modify the memory.
            var quadrantHashMap = QuadrantSystem.QuadrantHashMap;

            Entities.WithReadOnly(quadrantHashMap).WithAll<SpaceshipTag>().ForEach(
                (Entity entity, ref TargetingComponent target, in Translation pos, in TeamComponent team,
                    in BoidComponent boid) => {
                    var hashMapKey = QuadrantSystem.HashKeyFromPosition(pos.Value);

                    // Search neighboring quadrants if the currently locked target is still in range,
                    // or find a new closest enemy target
                    var minDistance = float.MaxValue;
                    var closestEntity = target.TargetEntity;
                    var closestPos = target.TargetPosition;
                    var newTargetFound = false;
                    if (SearchQuadrantNeighbors(in quadrantHashMap, hashMapKey, entity, target.TargetingRadius,
                        pos.Value, team.Team, target.TargetLocked, target.TargetEntity, ref minDistance,
                        ref closestEntity, ref closestPos, ref newTargetFound)) {
                        return; // Currently locked target still in range
                    }

                    if (newTargetFound) {
                        target.TargetPosition = closestPos;
                        target.TargetEntity = closestEntity;
                        target.TargetLocked = true;
                    } else {
                        target.TargetLocked = false;
                    }
                }).ScheduleParallel();
        }

        /// <summary>
        /// Searches a given quadrant for enemy starships and looks for currently locked target or for a new target.
        /// </summary>
        private static bool SearchQuadrantNeighbor(in NativeMultiHashMap<int, QuadrantData> quadrantHashMap, in int key,
            in Entity currentEntity, in float radius, in float3 pos, in TeamComponent.TeamEnum team, bool targetLocked,
            Entity targetEntity, ref float minDistance, ref Entity closestEntity, ref float3 closestPos,
            ref bool newTargetFound) {
            // Check if there are any starships in the quadrant
            if (!quadrantHashMap.TryGetFirstValue(key, out var quadrantData, out var iterator))
                return false;

            // Iterate through all the starships in the quadrant and do the work
            do {
                if (team == quadrantData.Team)
                    continue; // teammate

                var distance = Vector3.Distance(pos, quadrantData.Position);
                if (distance < radius) {
                    if (targetLocked && quadrantData.Entity == targetEntity)
                        return true; // found locked target we can end

                    if (distance < minDistance) {
                        // found a closer potential target
                        newTargetFound = true;
                        minDistance = distance;
                        closestEntity = quadrantData.Entity;
                        closestPos = quadrantData.Position;
                    }
                }
            } while (quadrantHashMap.TryGetNextValue(out quadrantData, ref iterator));

            return false;
        }

        /// <summary>
        /// Searches all neighboring quadrants and looks for currently locked target or for a new target.
        /// </summary>
        private static bool SearchQuadrantNeighbors(in NativeMultiHashMap<int, QuadrantData> quadrantHashMap,
            in int key, in Entity currentEntity, in float radius, in float3 pos, in TeamComponent.TeamEnum team,
            bool targetLocked, Entity targetEntity, ref float minDistance, ref Entity closestEntity,
            ref float3 closestPos, ref bool newTargetFound) {
            if (SearchQuadrantNeighbor(quadrantHashMap, key, currentEntity, radius, pos, team, targetLocked,
                targetEntity, ref minDistance, ref closestEntity, ref closestPos, ref newTargetFound))
                return true;

            if (SearchQuadrantNeighbor(quadrantHashMap, key + 1, currentEntity, radius, pos, team, targetLocked,
                targetEntity, ref minDistance, ref closestEntity, ref closestPos, ref newTargetFound))
                return true;

            if (SearchQuadrantNeighbor(quadrantHashMap, key - 1, currentEntity, radius, pos, team, targetLocked,
                targetEntity, ref minDistance, ref closestEntity, ref closestPos, ref newTargetFound))
                return true;

            if (SearchQuadrantNeighbor(quadrantHashMap, key + QuadrantSystem.QuadrantYMultiplier, currentEntity, radius,
                pos, team, targetLocked, targetEntity, ref minDistance, ref closestEntity, ref closestPos,
                ref newTargetFound))
                return true;

            if (SearchQuadrantNeighbor(quadrantHashMap, key - QuadrantSystem.QuadrantYMultiplier, currentEntity, radius,
                pos, team, targetLocked, targetEntity, ref minDistance, ref closestEntity, ref closestPos,
                ref newTargetFound))
                return true;

            return false;
        }
    }
}
