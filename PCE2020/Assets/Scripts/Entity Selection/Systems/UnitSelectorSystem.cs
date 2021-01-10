using Assets.Scripts.Entity_Selection.Components;
using Assets.Scripts.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Entity_Selection.Systems {
    /// <summary>
    /// System handling unit selection after a selection area has been activated.
    /// </summary>
    /// 
    /// <remarks>
    /// We want to update this system in <c>InitializationSystemGroup</c> and add all commands to the
    /// <c>EndInitializationEntityCommandBufferSystem</c>. Because in the Simulation group, we are
    /// handling destroying of spaceships and we could try to modify the Archetype of an entity, that
    /// has already been deleted.
    /// </remarks>
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public class UnitSelectorSystem : JobComponentSystem {
        /// <summary>
        /// Reference to the cursor entity.
        /// </summary>
        private Entity _cursor;

        /// <summary>
        /// Reference to the <c>EndInitializationEntityCommandBufferSystem</c> that
        /// handles buffering of commands that affect entities.
        /// </summary>
        private EndInitializationEntityCommandBufferSystem _ecbSystem;

        /// <summary>
        /// Gets or creates an <c>EndSimulationEntityCommandBufferSystem</c>
        /// </summary>
        protected override void OnCreate() {
            base.OnCreate();
            _ecbSystem = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
        }

        /// <summary>
        /// Gets reference to the cursor entity
        /// </summary>
        protected override void OnStartRunning() {
            base.OnStartRunning();

            var cursorQuery = GetEntityQuery(ComponentType.ReadOnly<CursorTag>());
            _cursor = cursorQuery.ToEntityArray(Allocator.Temp)[0];
            Debug.Assert(_cursor != Entity.Null);
        }

        /// <summary>
        /// Handles unit selection if a selection was activated.
        /// </summary>
        /// <param name="inputDeps">Input job dependencies</param>
        /// <returns>Job Handle</returns>
        protected override JobHandle OnUpdate(JobHandle inputDeps) {
            // Find the selection component
            var allSelections = GetComponentDataFromEntity<SelectionComponent>(true);

            var selection = allSelections[_cursor];
            if (!selection.IsActive)
                return default; // if selection is not active

            // Compute bottom left and top right corners of the selection area
            var bottomLeftCorner = GetBottomLeftPosition(selection.StartPosition, selection.EndPosition);
            var topRightCorner = GetTopRightPosition(selection.StartPosition, selection.EndPosition);

            // Create command buffer where component manipulation commands will be added
            var ecb = _ecbSystem.CreateCommandBuffer().AsParallelWriter();

            // Handle selection of units by adding or removing SelectedUnitTag component
            var jobHandle = Entities.WithAll<SpaceshipTag>().ForEach(
                (Entity entity, int entityInQueryIndex, ref Translation pos) => {
                    if (IsPointInSelection(in pos.Value, in bottomLeftCorner, in topRightCorner)) {
                        ecb.AddComponent<SelectedUnitTag>(entityInQueryIndex, entity); // Select unit
                    } else {
                        ecb.RemoveComponent<SelectedUnitTag>(entityInQueryIndex, entity); // De-select unit
                    }
                }).Schedule(inputDeps);

            _ecbSystem.AddJobHandleForProducer(jobHandle);
            return jobHandle;
        }

        /// <summary>
        /// Method returns bottom left point of a rectangle defined by two points.
        /// </summary>
        /// <param name="start">Starting point of the rectangle</param>
        /// <param name="end">End point of the rectangle</param>
        /// <returns>Bottom left point of the rectangle</returns>
        private static float3 GetBottomLeftPosition(float3 start, float3 end) {
            return new float3(math.min(start.x, end.x), math.min(start.y, end.y), 0);
        }

        /// <summary>
        /// Method returns top right point of a rectangle defined by two points.
        /// </summary>
        /// <param name="start">Starting point of the rectangle</param>
        /// <param name="end">End point of the rectangle</param>
        /// <returns>Top right point of the rectangle</returns>
        private static float3 GetTopRightPosition(float3 start, float3 end) {
            return new float3(math.max(start.x, end.x), math.max(start.y, end.y), 0);
        }

        /// <summary>
        /// Method returns true if given point is inside a rectangle defined by
        /// the bottom left and top right points.
        /// </summary>
        /// <param name="point">Given point</param>
        /// <param name="bottomLeft">Bottom left point of the rectangle</param>
        /// <param name="topRight">Top right point of the rectangle</param>
        /// <returns>Boolean if point is in selection or not.</returns>
        private static bool IsPointInSelection(in float3 point, in float3 bottomLeft, in float3 topRight) {
            return (point.x >= bottomLeft.x && point.y >= bottomLeft.y && point.x <= topRight.x &&
                    point.y <= topRight.y);
        }
    }
}
