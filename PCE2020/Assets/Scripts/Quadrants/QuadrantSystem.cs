using Assets.Scripts.Teams;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Quadrants {
    public class QuadrantSystem : SystemBase {
        public static NativeMultiHashMap<int, QuadrantData> QuadrantHashMap;
        public const int QuadrantYMultiplier = 1000;
        private const int QuadrantCellSize = 1;

        protected override void OnCreate() {
            base.OnCreate();
            QuadrantHashMap = new NativeMultiHashMap<int, QuadrantData>(0, Allocator.Persistent);
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            QuadrantHashMap.Dispose();
        }

        protected override void OnUpdate() {
            var entityQuery = GetEntityQuery(typeof(SpaceshipTag));

            QuadrantHashMap.Clear();

            Entities.WithAll<SpaceshipTag>().ForEach((Entity entity, in Translation pos, in LocalToWorld ltw, in TeamComponent team) => {
                var hasMapKey = HashKeyFromPosition(pos.Value);
                QuadrantHashMap.Add(hasMapKey, new QuadrantData {
                    Position = pos.Value,
                    Rotation = ltw.Up,
                    Entity = entity,
                    Team = team.Team
                });
            }).WithoutBurst().Run();

            //DebugDrawQuadrant(GetMousePosition());
            //Debug.Log(GetEntityCountInQuadrant(quadrantMultiHashMap, HashKeyFromPosition(GetMousePosition())));
        }

        public static int HashKeyFromPosition(float3 pos) {
            return (int) (math.floor(pos.x / QuadrantCellSize) +
                          QuadrantYMultiplier * math.floor(pos.y / QuadrantCellSize));
        }

        private static int GetEntityCountInQuadrant(NativeMultiHashMap<int, QuadrantData> hashMap, int key) {
            var count = 0;
            if (hashMap.TryGetFirstValue(key, out _, out var iterator)) {
                do {
                    count++;
                } while (hashMap.TryGetNextValue(out _, ref iterator));
            }

            return count;
        }

        private static void DebugDrawQuadrant(float3 pos) {
            var lowerLeft = new Vector3(math.floor(pos.x / QuadrantCellSize) * QuadrantCellSize, math.floor(pos.y / QuadrantCellSize) * QuadrantCellSize);
            Debug.DrawLine(lowerLeft, lowerLeft + new Vector3(+1, +0) * QuadrantCellSize);
            Debug.DrawLine(lowerLeft, lowerLeft + new Vector3(+0, +1) * QuadrantCellSize);
            Debug.DrawLine(lowerLeft + new Vector3(+1, +0) * QuadrantCellSize, lowerLeft + new Vector3(+1, +1) * QuadrantCellSize);
            Debug.DrawLine(lowerLeft + new Vector3(+0, +1) * QuadrantCellSize, lowerLeft + new Vector3(+1, +1) * QuadrantCellSize);
            //Debug.Log(HashKeyFromPosition(pos) + " " + pos);
        }

        private static float3 GetMousePosition() {
            var mouse = new float3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
            var worldSpacePoint = Camera.main.ScreenToWorldPoint(mouse);

            return new float3(worldSpacePoint.x, worldSpacePoint.y, 0);
        }
    }
}
