using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UI.PrefabScripts;
using UnityEngine;

namespace UI.MainScene
{
    public class SettingPanel : MonoBehaviour
    {
        #region Public Values

        

        #endregion

        #region Private Values

        private SettingData _data;
        [SerializeField] private FloatSettingArea graphicsQuality;
        [SerializeField] private FloatSettingArea masterVolume;

        #endregion

        #region Mono Methods

        private void Awake()
        {
            graphicsQuality.Init("그래픽 퀄리티", 5, 1);
            masterVolume.Init("마스터 볼륨", 1);
        }

        #endregion
    }
}


