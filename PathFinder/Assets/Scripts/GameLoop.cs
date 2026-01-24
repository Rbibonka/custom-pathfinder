using System;
using System.Collections.Generic;

public class GameLoop : IDisposable
{
    private bool disposed;

    private PlayersPathsSetter playersPathsManager;

    public GameLoop(
        List<Player> player,
        MouseClickHandler mouseClickHandler,
        PathFinderFacade pathFinderFacade)
    {
        playersPathsManager = new(mouseClickHandler, pathFinderFacade);
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        playersPathsManager.Dispose();

        disposed = true;
    }
}