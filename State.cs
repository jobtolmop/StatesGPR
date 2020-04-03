using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public enum cState { Chase, Patrol, AFK };
    public cState CurrentState;
    public enum pState {NONE,pos1, pos2, pos3, pos4 };
    public pState CurrentPatrol;
}
