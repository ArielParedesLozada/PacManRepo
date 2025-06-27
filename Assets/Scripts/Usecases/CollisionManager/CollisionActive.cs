using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionActive : MonoBehaviour
{
    private PacmanEntity _pacMan;
    private ISubjectGame _gameBoard;
    public CollisionActive(PacmanEntity pacman, ISubjectGame gameboard)
    {
        _pacMan = pacman;
        _gameBoard = gameboard;
    }
    public void Collide(PhantomEntity phantom)
    {
        _pacMan.Die();
        _gameBoard.Notify();
    }

}
