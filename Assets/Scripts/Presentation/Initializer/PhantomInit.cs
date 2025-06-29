using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhantomInit : MonoBehaviour
{
    public GameGhostManager InitializeGhosts(PacmanEntity pacman)
    {
        PhantomController[] ghostControllers = FindObjectsOfType<PhantomController>();
        List<PhantomEntity> ghostEntities = new List<PhantomEntity>();

        foreach (var gc in ghostControllers)
        {
            if (gc == null)
            {
                Debug.LogWarning("⚠️ PhantomController es null.");
                continue;
            }

            var entity = gc.ToEntity();

            if (entity == null)
            {
                Debug.LogError($"❌ PhantomEntity generado por {gc.gameObject.name} es null.");
                continue;
            }

            ghostEntities.Add(entity);
        }

        Debug.Log($"👻 Fantasmas inicializados correctamente: {ghostEntities.Count}");

        return new GameGhostManager(pacman, ghostEntities.ToArray());
    }
}
