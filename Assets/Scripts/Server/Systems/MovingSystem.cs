using Leopotam.EcsLite;

namespace Remote
{
    public class MovingSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var filter = systems.GetWorld().Filter<Transform>().Inc<Destination>().Inc<Actor>().End();
            var transforms = systems.GetWorld().GetPool<Transform>();
            var destinations = systems.GetWorld().GetPool<Destination>();

            foreach (var entity in filter)
            {
                ref var transform = ref transforms.Get(entity);
                ref var destination = ref destinations.Get(entity);
                transform.Position += (destination.Position - transform.Position).normalized * World.DeltaTime;

                if (transform.Position == destination.Position)
                    destinations.Del(entity);
            }
        }
    }
}
