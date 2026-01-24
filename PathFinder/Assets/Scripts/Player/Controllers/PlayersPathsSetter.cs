using System;
using UnityEngine;

public class PlayersPathsSetter : IDisposable
{
    private MouseClickHandler mouseClickHandler;
    private PathFinderFacade pathFinderFacade;

    private Player currentSelectedPlayer;

    private bool disposed;

    public PlayersPathsSetter(
        MouseClickHandler mouseClickHandler,
        PathFinderFacade pathFinderFacade)
    {
        this.mouseClickHandler = mouseClickHandler;
        this.pathFinderFacade = pathFinderFacade;


        this.mouseClickHandler.planeClicked += OnPlaneClicked;
        this.mouseClickHandler.playerClicked += OnPlayerClicked;
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
    }

    private void OnPlaneClicked(Vector3 point)
    {
        if (currentSelectedPlayer == null)
        {
            return;
        }

        var path = pathFinderFacade.FindPath(currentSelectedPlayer.transform.position, point);

        if (path.Count < 1)
        {
            return;
        }

        currentSelectedPlayer.SetMovePoints(path.ToArray());

        currentSelectedPlayer = null;
    }

    private void OnPlayerClicked(Player player)
    {
        currentSelectedPlayer = player;
    }
}