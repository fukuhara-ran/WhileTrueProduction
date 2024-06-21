using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadHandler : MonoBehaviour {
    [SerializeField] InputField username;
    [SerializeField] InputField password;

    public void LoadPlayer() {
        SaveManager.INSTANCE.Read(username.text, password.text);
        ContinueGameProgress();
    }
    
    public void CreatePlayer() {
        SaveManager.INSTANCE.Create(username.text, password.text);
        ContinueGameProgress();
    }

    private void ContinueGameProgress() {
        if(SaveManager.INSTANCE.CurrentActivePlayer == null) {
            Debug.Log("Inform player his username or password is wrong");
            return;
        }
        SceneManager.LoadScene("Level "+SaveManager.INSTANCE.CurrentActivePlayer.Level);
    }
}