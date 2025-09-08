using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

using System.Threading.Tasks;

public class Sample : MonoBehaviour
{
    [SerializeField]
    Button[] buttons;

    enum Language
    {
        Japanese,
        English,
        Chinese
    }

    void Start()
    {
        for(var i = 0; i < buttons.Length; i++)
        {
            var id = i;
            buttons[i].onClick.AddListener(() => OnButtonCallback(id));
        }
    }

    async void OnButtonCallback(int id)
    {
        switch((Language)id)
        {
            case Language.Japanese:
                await ChangeLocale("ja-JP");
            break;

            case Language.English:
                await ChangeLocale("en");
            break;

            case Language.Chinese:
                await ChangeLocale("zh");
            break;
        }
    }

    /// <summary>
    /// 言語を切り替える
    /// </summary>
    private async Task ChangeLocale(string key)
    {
        // 指定したLocaleを取得
        var locale = LocalizationSettings.AvailableLocales.Locales.Find((x) => x.Identifier.Code == key);

        // Localeの設定
        LocalizationSettings.SelectedLocale = locale;

        // 初期化待ち
        await LocalizationSettings.InitializationOperation.Task;
    }
}
