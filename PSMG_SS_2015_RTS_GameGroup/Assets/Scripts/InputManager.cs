using UnityEngine;
using System.Collections;

public class InputManager : MonoBehavior
{

    enum MOUSEBUTTON
    {
        LEFT,
        RIGHT,
        MIDDLE
    }

    void Update()
    {
        WatchForMouseEvents();
        WatchForKeyBoardEvents();
    }

    private void WatchForMouseEvents()
    {
        // TODO
    }

    private void WatchForKeyBoardEvents()
    {
        // TODO
    }

}