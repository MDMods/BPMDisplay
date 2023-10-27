using Assets.Scripts.Database;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace BPMDisplay
{
    public class Main : MelonMod
    {
    }

    [HarmonyPatch(typeof(PnlPreparation))]
    internal static class PnlPreparationPatch
    {
        private static GameObject BPM { get; set; }

        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        internal static void AwakePostfix()
        {
            var txtStageDesigner = GameObject.Find("TxtStageDesigner");
            BPM = Object.Instantiate(txtStageDesigner.transform.GetChild(0).gameObject, txtStageDesigner.transform);
            BPM.name = "BPM";
        }

        [HarmonyPostfix]
        [HarmonyPatch("OnEnable")]
        internal static void OnEnablePostfix()
        {
            var musicInfo = GlobalDataBase.s_DbMusicTag.m_CurSelectedMusicInfo.bpm != null ? GlobalDataBase.s_DbMusicTag.m_CurSelectedMusicInfo : GlobalDataBase.s_DbMusicTag.m_CurRandomMusicInfo;
            var bpm = musicInfo.bpm;
            BPM.GetComponent<Text>().text = "BPM: " + bpm;
            BPM.GetComponent<RectTransform>().position = new Vector3(-9.2f, 4.35f, 100f);
        }
    }
}