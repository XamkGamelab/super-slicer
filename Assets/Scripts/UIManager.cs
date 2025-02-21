using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider dashSlider;
    [SerializeField] Slider healthSlider;
    [SerializeField] Combo combo;

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
}
