using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillPacManUseCase
{
    IEnumerator Execute(PacManEntity entity, PacManView view);
}
