using System;
using Leopotam.EcsLite;

namespace Remote
{
    public class OpenSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _triggersFilter;
        private EcsFilter _doorsFilter;
        private EcsPool<Door> _doors;
        private EcsPool<Link> _links;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _triggersFilter = world
                .Filter<OpenTrigger>()
                .Inc<Link>()
                .End();
            
            _doorsFilter = world
                .Filter<Door>()
                .End();
             
            _doors = world.GetPool<Door>();
            _links = world.GetPool<Link>();
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            foreach (var triggerEntity in _triggersFilter)
            {
                foreach (var doorEntity in _doorsFilter)
                {
                    ref var door = ref _doors.Get(doorEntity);
                    
                    if (_links.Get(triggerEntity).Id != _links.Get(doorEntity).Id)
                        continue;

                    door.OpenProcess -= World.DeltaTime / 2;
                    door.OpenProcess = Math.Max(door.OpenProcess, 0);
                }
                
                world.DelEntity(triggerEntity);
            }
        }
    }
}