using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace PFrame.Entities
{
    /// <summary>
    ///     Rotate entities that have the Rotator component.
    /// </summary>
    public class RotateSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var deltaTime = Time.DeltaTime;

            return Entities.ForEach((ref Rotator rotator, ref Rotation rotation) =>
            {
                var rotateAmount = rotator.RotateSpeed * deltaTime;
                rotation.Value = math.mul(rotation.Value, quaternion.RotateY(math.radians(rotateAmount)));
            }).Schedule(inputDeps);
        }
    }
}