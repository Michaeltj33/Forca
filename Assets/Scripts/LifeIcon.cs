using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIcon : MonoBehaviour
{
    public GameObject[] icons;
    public int iconIndex;
    private int _lastIconIndex;
    void Start()
    {
        ClearIcons();
        _lastIconIndex = iconIndex;
        icons[iconIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_lastIconIndex != iconIndex)
        {
            _lastIconIndex = iconIndex;
            ClearIcons();
            icons[iconIndex].SetActive(true);
        }
    }

    public void ClearIcons()
    {
        foreach (var icon in icons)
        {
            icon.SetActive(false);
        }
    }
}
