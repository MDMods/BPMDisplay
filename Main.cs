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
        private static GameObject BpmGameObject { get; set; }

        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        internal static void AwakePostfix()
        {
            var txtStageDesigner = GameObject.Find("TxtStageDesigner");
            var levelDesignerTransform = txtStageDesigner.transform.GetChild(0);
            var position = levelDesignerTransform.position;
            BpmGameObject = Object.Instantiate(levelDesignerTransform.gameObject, txtStageDesigner.transform);
            BpmGameObject.name = "BPM";
            BpmGameObject.GetComponent<RectTransform>().position = new Vector3(position.x, position.y + 0.9f, position.z);
        }

        [HarmonyPostfix]
        [HarmonyPatch("OnEnable")]
        internal static void OnEnablePostfix()
        {
            var musicInfo = GlobalDataBase.s_DbMusicTag.m_CurSelectedMusicInfo.bpm != null
                ? GlobalDataBase.s_DbMusicTag.m_CurSelectedMusicInfo
                : GlobalDataBase.s_DbMusicTag.m_CurRandomMusicInfo;
            var bpm = musicInfo.bpm;
            BpmGameObject.transform.GetChild(0).GetComponent<Text>().text = "BPM: " + bpm;
        }
    }
}