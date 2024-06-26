using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] Adventurer adventurer;
    [SerializeField] Image image;

    void OnEnable() {
        adventurer = GameObject.Find("Adventurer").GetComponent<Adventurer>();
        image = GetComponent<Image>();
    }

    void FixedUpdate() {
        image.fillAmount = (float)adventurer.HealthPoint / adventurer.FullHealthPoint;
    }
}