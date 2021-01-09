using Assets.Scripts.Quadrants;
using Assets.Scripts.Spaceship.Flocking;
using Assets.Scripts.Teams;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Spaceship.Targeting {
    public class TargetingSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var quadrantHashMap = QuadrantSystem.QuadrantHashMap;

            Entities.WithReadOnly(quadrantHashMap).WithAll<SpaceshipTag>().ForEach(
                (Entity entity, ref TargetingComponent target, in Translation pos, in TeamComponent team, in Boid boid) => {
                    var hashMapKey = QuadrantSystem.HashKeyFromPosition(pos.Value);

                    var minDistance = float.MaxValue;
                    var closestEntity = target.TargetEntity;
                    var closestPos = target.TargetPosition;
                    var newTargetFound = false;
                    if (SearchQuadrantNeighbors(in quadrantHashMap, hashMapKey, entity, target.TargetingRadius,
                        pos.Value, team.Team, target.TargetLocked, target.TargetEntity, ref minDistance, ref closestEntity, ref closestPos, ref newTargetFound))
                        return;

                    if (newTargetFound) {
                        target.TargetPosition = closestPos;
                        target.TargetEntity = closestEntity;
                        target.TargetLocked = true;
                    } else {
                        target.TargetLocked = false;
                    }
                }).Schedule();
        }

        private static bool SearchQuadrantNeighbor(in NativeMultiHashMap<int, QuadrantData> quadrantHashMap, in int key,
            in Entity currentEntity, in float radius, in float3 pos, in TeamComponent.TeamEnum team, bool targetLocked, Entity targetEntity, ref float minDistance, ref Entity closestEntity, ref float3 closestPos, ref bool newTargetFound) {
            if (!quadrantHashMap.TryGetFirstValue(key, out var quadrantData, out var iterator))
                return false;

            do {
                if (team == quadrantData.Team)
                    continue;

                var distance = Vector3.Distance(pos, quadrantData.Position);
                if (distance < radius) {
                    if (targetLocked && quadrantData.Entity == targetEntity)
                        return true;

                    if (distance < minDistance) {
                        newTargetFound = true;
                        minDistance = distance;
                        closestEntity = quadrantData.Entity;
                        closestPos = quadrantData.Position;
                    }
                }
            } while (quadrantHashMap.TryGetNextValue(out quadrantData, ref iterator));

            return false;
        }

        private static bool SearchQuadrantNeighbors(in NativeMultiHashMap<int, QuadrantData> quadrantHashMap, in int key,
            in Entity currentEntity, in float radius, in float3 pos, in TeamComponent.TeamEnum team, bool targetLocked, Entity targetEntity, ref float minDistance, ref Entity closestEntity, ref float3 closestPos, ref bool newTargetFound) {

            if (SearchQuadrantNeighbor(quadrantHashMap, key, currentEntity, radius,
                pos, team, targetLocked, targetEntity, ref minDistance, ref closestEntity, ref closestPos, ref newTargetFound))
                return true;

            if (SearchQuadrantNeighbor(quadrantHashMap, key + 1, currentEntity, radius,
                pos, team, targetLocked, targetEntity, ref minDistance, ref closestEntity, ref closestPos, ref newTargetFound))
                return true;

            if (SearchQuadrantNeighbor(quadrantHashMap, key - 1, currentEntity, radius,
                pos, team, targetLocked, targetEntity, ref minDistance, ref closestEntity, ref closestPos, ref newTargetFound))
                return true;

            if (SearchQuadrantNeighbor(quadrantHashMap, key + QuadrantSystem.QuadrantYMultiplier, currentEntity, radius,
                pos, team, targetLocked, targetEntity, ref minDistance, ref closestEntity, ref closestPos, ref newTargetFound))
                return true;

            if (SearchQuadrantNeighbor(quadrantHashMap, key - QuadrantSystem.QuadrantYMultiplier, currentEntity, radius,
                pos, team, targetLocked, targetEntity, ref minDistance, ref closestEntity, ref closestPos, ref newTargetFound))
                return true;

            return false;
        }
    }
}
