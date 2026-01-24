using System;
using System.Collections.Generic;

public class GameLoop : IDisposable
{
    private List<Player> player;
    private MouseClickHandler mouseClickHandler;
    private PathFinderFacade pathFinderFacade;

    private bool disposed;

    private PlayersPathsSetter playersPathsManager;

    public GameLoop(
        List<Player> player,
        MouseClickHandler mouseClickHandler,
        PathFinderFacade pathFinderFacade)
    {
        this.player = player;
        this.mouseClickHandler = mouseClickHandler;
        this.pathFinderFacade = pathFinderFacade;

        playersPathsManager = new(mouseClickHandler, pathFinderFacade);
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
    }
}