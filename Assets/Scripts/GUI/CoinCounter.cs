using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinCounter : MonoBehaviour {
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] Adventurer adventurer;

    private int gold;

    void OnEnable() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        adventurer = GameObject.Find("Adventurer").GetComponent<Adventurer>();
    }
    void FixedUpdate() {
        if(!adventurer.IsDestroyed()) gold = adventurer.Wealth;
        if(adventurer.IsDestroyed() && gold > 0) gold--;
        textMeshPro.text = gold.ToString();
    }
}