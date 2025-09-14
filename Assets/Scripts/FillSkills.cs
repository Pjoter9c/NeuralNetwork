using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Visualize skills cooldown
public class FillSkills : MonoBehaviour
{
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.fillAmount = Mathf.Clamp01(_image.fillAmount + Time.deltaTime * 0.25f);
    }

    public void ResetCooldown()
    {
        _image.fillAmount = 0;
    }
}
