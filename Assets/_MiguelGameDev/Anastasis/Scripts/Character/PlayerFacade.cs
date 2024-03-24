using MiguelGameDev.Anastasis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacade : MonoBehaviour
{
    [SerializeField] CharacterAnimation _animation;
    
    public void Init()
    {
        _animation.Init();
    }
}
