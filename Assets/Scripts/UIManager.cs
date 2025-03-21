using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider dashSlider;
    [SerializeField] Slider healthSlider;
    [SerializeField] Combo combo;
    [SerializeField] TextMeshProUGUI scoreTextField;
    int score = 0;

    private void Start()
    {
        dashSlider.maxValue = 20;

        // testing
        dashSlider.value = 20;
    }

    public Slider DashSlider
    {
        get => dashSlider;
        //set => dashSlider = value;
    }

    public Slider HealthSlider 
    { 
        get => healthSlider;
        //set => healthSlider = value; 
    }

    public Combo Combo
    {
        get => combo;
        //set => combo = value;
    }

    public void IncreaseScore(int amount)
    {
        score += amount * combo.comboMult;
        scoreTextField.text = $"Score: {score}";
    }
}
