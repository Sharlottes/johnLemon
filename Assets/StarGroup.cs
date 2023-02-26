using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Structs.Singleton;

public class StarGroup : SingletonMonoBehaviour<StarGroup>
{
    public Sprite star_default, star_filled;

    Image[] m_Images;
    
    [SerializeField] int m_StarPoint;
    public int StarPoint
    {
        get => m_StarPoint; 
        set
        {
            m_StarPoint = value;
            UpdateStarImages();
        }
    } 
    void Start()
    {
        m_Images = GetComponentsInChildren<Image>();
    }

    void UpdateStarImages()
    {
        for (int i = 0; i < m_Images.Length; i++)
        {
            if (i + 1 <= m_StarPoint)
            {
                m_Images[i].sprite = star_filled;
            }
            else
            {
                m_Images[i].sprite = star_default;
            }
        }
    }
}
