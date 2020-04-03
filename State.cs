using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public enum cState { Chase, Patrol, AFK };
    public cState CurrentState;
}
