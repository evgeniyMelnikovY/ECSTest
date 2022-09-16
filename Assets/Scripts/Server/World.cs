using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Remote
{
    public class World
    {
        private readonly EcsWorld _world;
        private readonly IEcsSystems _systems;

        public static float DeltaTime { get; private set; }

        public World(List<List<object>> data)
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems.Add(new MovingSystem())
                .Init();

            _world.GetPool<Transform>();
            _world.GetPool<Destination>();
            _world.GetPool<Actor>();

            foreach (var entity in data)
            {
                var e = _world.NewEntity();

                foreach (var component in entity)
                {
                    var p = _world.GetPoolByType(component.GetType());
                    p.AddRaw(e, component);
                }
            }
        }

        public void Update(float deltaTime)
        {
            DeltaTime = deltaTime;
            _systems.Run();
        }

        public Dictionary<int, List<object>> GetRawData()
        {
            var result = new Dictionary<int, List<object>>();

            int[] entities = null;
            _world.GetAllEntities(ref entities);

            foreach (var entity in entities)
            {
                result[entity] = new List<object>();

                IEcsPool[] pools = null;
                _world.GetAllPools(ref pools);

                foreach (var pool in pools)
                {
                    if (pool.Has(entity))
                        result[entity].Add(pool.GetRaw(entity));
                }
            }

            return result;
        }

        public void Dispose()
        {
            _world.Destroy();
            _systems.Destroy();
        }
    }
}