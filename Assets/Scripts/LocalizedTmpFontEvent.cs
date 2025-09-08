using System;
using TMPro;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

[AddComponentMenu("Localization/Asset/" + nameof(LocalizedTmpFontEvent))]
public class LocalizedTmpFontEvent : LocalizedAssetEvent<TMP_FontAsset, LocalizedTmpFont, UnityEventTmpFont>
{
#if UNITY_EDITOR
    [MenuItem("CONTEXT/TextMeshProUGUI/Localize Extension")]
    private static void LocalizedTmpFontAsset(MenuCommand command)
    {
        var target = (TextMeshProUGUI)command.context;
        LocalizedTmpFontAsset(target);
    }

    public static LocalizedTmpFontEvent LocalizedTmpFontAsset(TextMeshProUGUI target)
    {
        var localizeEvent = (LocalizedTmpFontEvent)Undo.AddComponent(target.gameObject, typeof(LocalizedTmpFontEvent));

        // イベント発生時に font が変更されるように
        var setMethod = typeof(TextMeshProUGUI).GetProperty("font")?.GetSetMethod();
        if (setMethod != null)
        {
            var methodDelegate = (UnityAction<TMP_FontAsset>)Delegate.CreateDelegate(typeof(UnityAction<TMP_FontAsset>), target, setMethod);
            UnityEventTools.AddPersistentListener(localizeEvent.OnUpdateAsset, methodDelegate);
            localizeEvent.OnUpdateAsset.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);
        }

        return localizeEvent;
    }
#endif
}

/// <summary>
/// TmpFontAsset を引数とする Unity Event
/// </summary>
[Serializable]
public class UnityEventTmpFont : UnityEvent<TMP_FontAsset>
{
}



