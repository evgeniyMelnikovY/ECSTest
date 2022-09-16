using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Remote
{
    public interface IClientHandler
    {
        public void Tick(Dictionary<int, List<object>> data);
    }

    public class Server : IDisposable
    {
        private readonly IClientHandler _clientHandler;
        private readonly CancellationTokenSource _cts = new();
        private readonly int _frequency;

        private World _world;

        public Server(int fps, IClientHandler clientHandler)
        {
            _clientHandler = clientHandler;
            _frequency = 1000 / fps;

            Run();
        }

        public void SetupWorld(List<List<object>> data)
        {
            _world?.Dispose();

            _world = new World(data);
        }

        private async void Run()
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                while (!_cts.IsCancellationRequested)
                {
                    var elapsed = stopwatch.ElapsedMilliseconds;
                    stopwatch.Restart();

                    Update(elapsed / 1000f);

                    var delay = _frequency - stopwatch.ElapsedMilliseconds;

                    if (delay > 0)
                        await Task.Delay(TimeSpan.FromMilliseconds(delay));
                    else
                        Task.Yield();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Update(float deltaTime)
        {
            if (_world == null)
                return;

            _world.Update(deltaTime);
            _clientHandler.Tick(_world.GetRawData());
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _world?.Dispose();
        }
    }
}