using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPacManUseCase : IKillPacManUseCase
{
    public IEnumerator Execute(PacManEntity entity, PacManView view)
    {
        // Desactiva movimiento y reproduce animación de muerte
        entity.CanMove = false;
        view.PlayDeathAnimation();

        // Espera animación
        yield return new WaitForSeconds(2f);

        // Mueve a posición inicial y coloca sprite idle
        view.MoveToStartingPosition();

        // Reinicia animación y estado como Restart() 
        view.RestartAnimation();
        entity.ResetState();

        // Espera un tiempo sin usar UI
        yield return new WaitForSeconds(3f);

        // Habilita movimiento nuevamente
        entity.CanMove = true;
    }
}
