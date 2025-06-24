using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumeBonusItemUseCase
{
    bool Execute(Vector2 pacmanPos, bool isPlayerOne);
}
