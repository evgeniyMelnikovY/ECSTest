using Leopotam.EcsLite;

namespace Remote
{
    public class ButtonPressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _buttonFilter;
        private EcsFilter _actorFilter;
        private EcsPool<Transform> _transforms;
        private EcsPool<Button> _buttons;
        private EcsPool<Link> _links;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _buttonFilter = world
                .Filter<Button>()
                .Inc<Link>()
                .Inc<Transform>()
                .End();
            
            _actorFilter = world
                .Filter<Actor>()
                .Inc<Transform>()
                .End();
            
            _transforms = world.GetPool<Transform>();
            _buttons = world.GetPool<Button>();
            _links = world.GetPool<Link>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var buttonEntity in _buttonFilter)
            {
                ref var transform = ref _transforms.Get(buttonEntity);
                ref var button = ref _buttons.Get(buttonEntity);
                ref var link = ref _links.Get(buttonEntity);
                
                foreach (var actorEntity in _actorFilter)
                {
                    ref var actorTransform = ref _transforms.Get(actorEntity);

                    var distance = (actorTransform.Position - transform.Position).magnitude;

                    if (distance > button.Radius)
                        continue;
                    
                    var world = systems.GetWorld();
                    var e = world.NewEntity();
                    var pool = world.GetPool<OpenTrigger>();
                    pool.Add(e);
                    ((IEcsPool)_links).AddRaw(e, link);
                }
            }
        }
    }
}