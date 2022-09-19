using System.Collections.Generic;
using Remote;

public class CommandsHandler
{
    private readonly Server _server;
    private readonly Ground _ground;
    
    public CommandsHandler(Server server, Ground ground)
    {
        _server = server;
        _ground = ground;

        _ground.Click += SendDestinationCommand;
    }

    private void SendDestinationCommand(Destination obj)
    {
        _server.UserAction(new Dictionary<int, List<object>> { { _ground.Id, new List<object> { obj } } });
    }
}