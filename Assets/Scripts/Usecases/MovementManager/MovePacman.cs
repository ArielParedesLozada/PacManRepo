public class MovePacman
{
    private readonly PacmanEntity _pacman;
    public MovePacman(PacmanEntity pacman)
    {
        _pacman = pacman;
    }
    public void Move(float deltaTime)
    {
        if (_pacman.PacManState == PacManState.Dead)
            return;

        // 1. Intentar cambio de dirección anticipado
        if (!_pacman.NextDirection.Equals(_pacman.Direction))
        {
            var possible = CanMove(_pacman.CurrentNode, _pacman.NextDirection);
            if (possible != null)
            {
                _pacman.Direction = _pacman.NextDirection;
                _pacman.TargetNode = possible;
                _pacman.CurrentNode = null;
            }
        }

        // 2. Avanzar nodo a nodo
        if (_pacman.TargetNode != null && _pacman.CurrentNode != _pacman.TargetNode)
        {
            _pacman.Position = _pacman.Position.Add(_pacman.Direction.Multiply(_pacman.Speed * deltaTime));

            if (HasOverShotTarget(_pacman))
            {
                _pacman.CurrentNode = _pacman.TargetNode;
                _pacman.Position = _pacman.CurrentNode.Position;

                // 4. Intentar seguir avanzando en la nueva dirección primero
                var next = CanMove(_pacman.CurrentNode, _pacman.Direction);

                if (next != null)
                {
                    _pacman.TargetNode = next;
                    _pacman.Direction = next.Position.Subtract(_pacman.CurrentNode.Position).Normalize();
                    _pacman.CurrentNode = null;
                }
                else
                {
                    _pacman.Direction = new Position(0, 0);
                    _pacman.TargetNode = null;
                    _pacman.Position = _pacman.CurrentNode.Position;
                }
            }
        }

        // 5. Si no tiene TargetNode, intenta según Direction (si está definida)
        if (_pacman.TargetNode == null && _pacman.CurrentNode != null && !_pacman.Direction.Equals(new Position(0, 0)))
        {
            var fallback = CanMove(_pacman.CurrentNode, _pacman.Direction);
            if (fallback != null)
            {
                _pacman.TargetNode = fallback;
                _pacman.Direction = _pacman.Direction;
                _pacman.CurrentNode = null;
            }
        }
    }

    private NodeEntity CanMove(NodeEntity node, IPosition dir)
    {
        if (node == null) return null;

        for (int i = 0; i < node.ValidDirections.Length; i++)
        {
            if (node.ValidDirections[i].Equals(dir))
                return node.Neighbors[i];
        }
        return null;
    }

    private bool HasOverShotTarget(PacmanEntity pacman)
    {
        if (pacman.TargetNode == null || pacman.CurrentNode == null)
            return false;

        var fromPrevToTarget = pacman.TargetNode.Position.Subtract(pacman.CurrentNode.Position);
        var fromPrevToNow = pacman.Position.Subtract(pacman.CurrentNode.Position);

        return fromPrevToNow.SqrMagnitude() >= fromPrevToTarget.SqrMagnitude() - 0.01f;
    }
}
