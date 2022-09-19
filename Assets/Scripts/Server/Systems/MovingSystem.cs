using Leopotam.EcsLite;
using UnityEngine;

namespace Remote
{
    public class MovingSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsPool<Transform> _transforms;
        private EcsPool<Destination> _destinations;
        private EcsPool<Movement> _movements;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _filter = world
                .Filter<Transform>()
                .Inc<Destination>()
                .Inc<Movement>()
                .Inc<Actor>()
                .End();
            
            _transforms = world.GetPool<Transform>();
            _destinations = world.GetPool<Destination>();
            _movements = world.GetPool<Movement>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var transform = ref _transforms.Get(entity);
                ref var destination = ref _destinations.Get(entity);
                ref var movement = ref _movements.Get(entity);
                
                var range = destination.Position - transform.Position;
                var step = World.DeltaTime * movement.Speed;

                if (range.magnitude <= step)
                    transform.Position = destination.Position;
                else
                    transform.Position += range.normalized * step;

                if (transform.Position == destination.Position)
                    _destinations.Del(entity);
            }
        }
    }
}
