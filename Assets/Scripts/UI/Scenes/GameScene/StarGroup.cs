using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Utils.Singletons;

namespace Assets.Scripts.UI.Scenes.GameScene
{
    public class StarGroup : SingletonMonoBehaviour<StarGroup>
    {
        public Sprite star_default, star_filled;

        Image[] m_Images;
        ParticleSystem[] m_pses;

        [SerializeField] int m_StarPoint;
        public int StarPoint
        {
            get => m_StarPoint;
            set
            {
                m_StarPoint = value;
                PlayerPrefs.SetInt("starPoint", value);
                UpdateStarImages();
            }
        }
        void Start()
        {
            m_Images = GetComponentsInChildren<Image>();
            m_pses = GetComponentsInChildren<ParticleSystem>();
        }

        void UpdateStarImages()
        {
            for (int i = 0; i < m_Images.Length; i++)
            {
                if (i + 1 <= m_StarPoint)
                {
                    if (m_Images[i].sprite == star_default) m_pses[i].Play();
                    m_Images[i].sprite = star_filled;
                }
                else
                {
                    m_Images[i].sprite = star_default;
                }
            }
        }
    }
}