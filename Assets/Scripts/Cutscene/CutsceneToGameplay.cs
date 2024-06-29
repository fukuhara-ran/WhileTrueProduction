using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneToGameplay : MonoBehaviour
{
    public void GotoGameplay()
    {
        SceneTransitionManager.Instance.LoadScene("Level 1");
    }
}
