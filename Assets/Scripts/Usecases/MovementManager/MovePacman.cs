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

        // Movimiento entre nodos (incluye reversa)
        if (_pacman.TargetNode != null && _pacman.PreviousNode != null)
        {
            // Permitir reversa en medio del camino
            var reverse = _pacman.Direction.Multiply(-1);
            if (_pacman.NextDirection.Equals(reverse))
            {
                if (CanMove(_pacman.TargetNode, reverse) == _pacman.PreviousNode)
                {
                    var temp = _pacman.PreviousNode;
                    _pacman.PreviousNode = _pacman.TargetNode;
                    _pacman.TargetNode = temp;
                    _pacman.Direction = reverse;
                }
            }

            // Movimiento normal
            _pacman.Position = _pacman.Position.Add(_pacman.Direction.Multiply(_pacman.Speed * deltaTime));
            if (HasOverShotTarget(_pacman))
            {
                // Llega al nodo objetivo
                _pacman.Position = _pacman.TargetNode.Position;
                _pacman.CurrentNode = _pacman.TargetNode;
                // --- Aquí va la lógica de decisión de dirección, igual que en MovePacManUseCase ---
                var tryNext = CanMove(_pacman.CurrentNode, _pacman.NextDirection);
                if (tryNext != null)
                {
                    _pacman.Direction = _pacman.NextDirection;
                    _pacman.PreviousNode = _pacman.CurrentNode;
                    _pacman.TargetNode = tryNext;
                    _pacman.CurrentNode = null;
                }
                else
                {
                    var forward = CanMove(_pacman.CurrentNode, _pacman.Direction);
                    if (forward != null)
                    {
                        _pacman.PreviousNode = _pacman.CurrentNode;
                        _pacman.TargetNode = forward;
                        _pacman.CurrentNode = null;
                    }
                    else
                    {
                        _pacman.Direction = new Position(0, 0);
                        _pacman.TargetNode = null;
                    }
                }
            }
            else
            {
                return; // Aún en trayecto, no decidir
            }
        }

        // Si está en un nodo y no tiene TargetNode, intentar iniciar movimiento
        if (_pacman.CurrentNode != null && _pacman.TargetNode == null)
        {
            var tryNext = CanMove(_pacman.CurrentNode, _pacman.NextDirection);
            if (tryNext != null)
            {
                _pacman.Direction = _pacman.NextDirection;
                _pacman.PreviousNode = _pacman.CurrentNode;
                _pacman.TargetNode = tryNext;
                _pacman.CurrentNode = null;
                return;
            }

            var forward = CanMove(_pacman.CurrentNode, _pacman.Direction);
            if (forward != null)
            {
                _pacman.PreviousNode = _pacman.CurrentNode;
                _pacman.TargetNode = forward;
                _pacman.CurrentNode = null;
                return;
            }

            _pacman.Direction = new Position(0, 0);
        }
    }

    private NodeEntity CanMove(NodeEntity node, IPosition dir)
    {
        if (node == null) return null;

        for (int i = 0; i < node.ValidDirections.Length; i++)
        {
            if (node.ValidDirections[i] != null && node.ValidDirections[i].Equals(dir))
                return node.Neighbors[i];
        }

        return null;
    }

    private bool HasOverShotTarget(PacmanEntity pacman)
    {
        if (pacman.PreviousNode == null || pacman.TargetNode == null)
            return false;

        var fromPrevToTarget = pacman.TargetNode.Position.Subtract(pacman.PreviousNode.Position);
        var fromPrevToNow = pacman.Position.Subtract(pacman.PreviousNode.Position);

        return fromPrevToNow.SqrMagnitude() >= fromPrevToTarget.SqrMagnitude() - 0.01f;
    }
}