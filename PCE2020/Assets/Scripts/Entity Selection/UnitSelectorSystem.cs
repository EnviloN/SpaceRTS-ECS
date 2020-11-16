using Assets.Scripts.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Entity_Selection {

    public class UnitSelectorSystem : JobComponentSystem {
        private Entity _cursor;
        EndSimulationEntityCommandBufferSystem m_EndSimulationEcbSystem;

        protected override void OnCreate() {
            base.OnCreate();
            m_EndSimulationEcbSystem = World
                .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnStartRunning() {
            base.OnStartRunning();

            var cursorQuery = GetEntityQuery(ComponentType.ReadOnly<CursorTag>());
            _cursor = cursorQuery.ToEntityArray(Allocator.Temp)[0];
            Debug.Assert(_cursor != Entity.Null);
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps) {
            var allSelections = GetComponentDataFromEntity<SelectionComponent>(true);
            if (!allSelections.HasComponent(_cursor))
                return default;

            var selection = allSelections[_cursor];
            if (!selection.IsActive)
                return default;

            var bottomLeftCorner = GetBottomLeftPosition(selection.StartPosition, selection.EndPosition);
            var topRightCorner = GetTopRightPosition(selection.StartPosition, selection.EndPosition);

            var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
            var jobHandle = Entities.WithAll<SpaceshipTag>().ForEach((Entity entity, int entityInQueryIndex, ref Translation pos) => {
                if (IsPointInSelection(in pos.Value, in bottomLeftCorner, in topRightCorner)) {
                    ecb.AddComponent<SelectedUnitTag>(entityInQueryIndex, entity);
                }
            }).Schedule(inputDeps);
            m_EndSimulationEcbSystem.AddJobHandleForProducer(jobHandle);
            return jobHandle;
        }

        private static float3 GetBottomLeftPosition(float3 start, float3 end) {
            return new float3(math.min(start.x, end.x), math.min(start.y, end.y), 0);
        }

        private static float3 GetTopRightPosition(float3 start, float3 end) {
            return new float3(math.max(start.x, end.x), math.max(start.y, end.y), 0);
        }

        private static bool IsPointInSelection(in float3 point, in float3 bottomLeft, in float3 topRight) {
            return (point.x >= bottomLeft.x && point.y >= bottomLeft.y && point.x <= topRight.x &&
                    point.y <= topRight.y);
        }
    }
}
