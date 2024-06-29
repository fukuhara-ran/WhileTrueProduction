using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologToCutscene : MonoBehaviour
{
    public void GotoCutsence()
    {
        SceneManager.LoadScene("First Encounter Scene");
    }
}
