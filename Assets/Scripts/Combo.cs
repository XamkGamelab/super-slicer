using System.Collections;
using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI comboTextField;
    [SerializeField] float comboDecay;
    public int comboMult = 1;
    float currentComboDecay;

    private void Update()
    {
        if (currentComboDecay > 0)
        {
            currentComboDecay -= Time.deltaTime;
        }
        if (comboMult != 1)
        {
            currentComboDecay = comboDecay;
            StartCoroutine(Decay());
        }
    }

    IEnumerator Decay()
    {
        // TODO: split in two functions
        int startMult = comboMult;
        yield return new WaitForSeconds(comboDecay);
        //Debug.Log("attempt decay");
        if (startMult == comboMult)
        {
            Debug.Log("Decay");
            comboMult = 1;
            UpdateComboMeter();
        }
        yield return null;
    }

    public void IncreaseCombo()
    {
        comboMult += 1;
        UpdateComboMeter();
    }

    private void UpdateComboMeter()
    {
        comboTextField.text = $"X{comboMult}";
    }
}
