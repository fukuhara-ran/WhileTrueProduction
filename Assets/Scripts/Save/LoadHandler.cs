using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadHandler : MonoBehaviour {
    [SerializeField] InputField username;
    [SerializeField] InputField password;

    public void LoadPlayer() {
        SaveManager.GetInstance().Read(username.text, password.text);
        ContinueGameProgress();
    }
    
    public void CreatePlayer() {
        SaveManager.GetInstance().Create(username.text, password.text);
        ContinueGameProgress();
    }

    private void ContinueGameProgress() {
        if(SaveManager.GetInstance().CurrentActivePlayer == null) {
            Debug.Log("Inform player his username or password is wrong");
            return;
        }
        
        SaveManager.GetInstance().GoToLatestProgress();
    }
}