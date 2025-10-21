using System;
using System.Collections.Generic;
using Core.Components;
using Core.Lib.Services;
using Core.Systems;
using Leopotam.EcsLite;
using Reflex;

namespace Core.Services
{
    public class PlayersService : IInitializableService
    {
        public readonly List<IEcsPool> PlayerMarkerPools = new();
        public readonly List<EcsFilter> PlayerMarkerFilters = new();
        private readonly List<IPlayerConfig> PlayerConfigs = new();

        public readonly int MaxPlayersCount = 1;
        private Context _context;

        public void InitializeService(Context context)
        {
            var world = context.Resolve<EcsWorld>();

            void Fill<T>() where T : struct
            {
                PlayerMarkerPools.Add(world.GetPool<T>());
                PlayerMarkerFilters.Add(world.Filter<T>().End());
                PlayerConfigs.Add(new PlayerConfig<T>(PlayerConfigs.Count, context));
            }

            Fill<Player1UniqueTag>();
            // Fill<Player2UniqueTag>();

            if (MaxPlayersCount != PlayerConfigs.Count)
                throw new Exception("MaxPlayersCount != PlayerConfigs.Count");

            _context = context;
        }

        public void ForUiEntityBuilder(Action<IUiEntityBuilder> action, UiEntityFactory uiEntityFactory)
        {
            foreach (var config in PlayerConfigs)
                config.ForUiEntityBuilder(action, uiEntityFactory);
        }

        public IEnumerable<IEcsSystem> BuildControllerSystems()
        {
            List<IEcsSystem> systems = new();
            foreach (var config in PlayerConfigs)
                systems.AddRange(config.PlayerControllerSystems(_context));

            return systems;
        }
    }

    internal class PlayerConfig<T> : IPlayerConfig where T : struct
    {
        private readonly int _index;
        private readonly Context _context;

        public PlayerConfig(int index, Context context)
        {
            _index = index;
            _context = context;
        }

        public void ForUiEntityBuilder(Action<IUiEntityBuilder> action, UiEntityFactory uiEntityFactory) =>
            action(uiEntityFactory.BuildUiEntityWithLink<T>());

        public IEnumerable<IEcsSystem> PlayerControllerSystems(Context context) =>
            new IEcsSystem[]
            {
                new PlayerInputsSystem<T>(_context, _context.Resolve<InputService>().GetInputs(_index)),
                new PlayerButtonsSystem<T>(_context, _context.Resolve<InputService>().GetInputs(_index)),
                //инпут обрабатывается в апдейте до этой системы и потому ивент съедается в ней до прочтения 
            };
    }

    internal interface IPlayerConfig
    {
        void ForUiEntityBuilder(Action<IUiEntityBuilder> action, UiEntityFactory uiEntityFactory);

        IEnumerable<IEcsSystem> PlayerControllerSystems(Context context);
    }
}