using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhantomInit : MonoBehaviour
{
    public GameGhostManager InitializeGhosts(PacmanEntity pacman)
    {
        PhantomController[] ghostControllers = FindObjectsOfType<PhantomController>();
        PhantomEntity[] ghostEntities = ghostControllers.Select(gc => gc.ToEntity()).ToArray();

        return new GameGhostManager(pacman, ghostEntities);
    }
}
