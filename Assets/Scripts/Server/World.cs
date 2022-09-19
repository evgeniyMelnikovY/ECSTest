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
                .Add(new ButtonPressSystem())
                .Add(new OpenSystem())
                .Init();

            //костыль так как GetPoolByType не создает новый пул
            _world.GetPool<Transform>();
            _world.GetPool<Destination>();
            _world.GetPool<Actor>();
            _world.GetPool<Movement>();
            _world.GetPool<Button>();
            _world.GetPool<Link>();
            _world.GetPool<Door>();
            _world.GetPool<OpenTrigger>();
            _world.GetPool<Wall>();
            //

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

        public void AppendRawData(Dictionary<int, List<object>> data)
        {
            foreach (var pair in data)
            {
                foreach (var component in pair.Value)
                {
                    var p = _world.GetPoolByType(component.GetType());
                    
                    if (!p.Has(pair.Key))
                        p.AddRaw(pair.Key, component);
                    else
                        p.SetRaw(pair.Key, component);
                }
            }
        }

        public void Dispose()
        {
            _world.Destroy();
            _systems.Destroy();
        }
    }
}