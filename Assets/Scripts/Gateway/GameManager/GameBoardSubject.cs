using System.Collections.Generic;
using UnityEngine;
public class GameBoardSubject : ISubjectGame
{
    private GameTilesManager _tiles;
    private GameGhostManager _ghostManager;
    private NodeEntity[] _nodes;
    public int Score { get; set; }
    public int Level { get; set; }
    public int Lives { get; set; }

    public void Initialize(GameTilesManager tiles, GameGhostManager ghostManager, IEnumerable<NodeEntity> nodes)
    {
        _tiles = tiles;
        _ghostManager = ghostManager;
        _nodes = new List<NodeEntity>(nodes).ToArray();
    }
    public NodeEntity GetNodeAt(IPosition position)
    {
        for (int i = 0; i < _nodes.Length; i++)
        {
            if (_nodes[i].Position.Equals(position))
            {
                return _nodes[i];
            }
        }
        return null;
    }

    public TileEntity GetTileAt(IPosition position)
    {
        var rounded = new Position(Mathf.RoundToInt(position.X), Mathf.RoundToInt(position.Y));
        return _tiles.GetTileAt(rounded);
    }

    public void Notify()
    {
        _ghostManager.Notify();
    }
}
