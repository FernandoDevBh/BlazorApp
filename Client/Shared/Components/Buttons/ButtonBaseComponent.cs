using Microsoft.AspNetCore.Components;

namespace Client.Shared.Components.Buttons;

public abstract class ButtonBaseComponent : ComponentBase
{
    private static List<string> styles = new List<string>{ "primary", "secondary", "success", "danger", "warning", "info", "light", "dark", "link" };

    [Parameter]
    public string Label { get; set; } = string.Empty;

    [Parameter]
    public bool IsLink { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> InputAttributes { get; set; } = new Dictionary<string, object>();

    protected string GetCss()
    {
        var haveStyle = InputAttributes.Keys.Any(k => styles.Contains(k));
        var isOutline = InputAttributes.Keys.Any(k => k == "outline");
        var isRounded = InputAttributes.Keys.Any(k => k == "rounded");

        if (!haveStyle)
            return string.Empty;

        var style = InputAttributes.Keys.SingleOrDefault(k => styles.Contains(k));

        if (style == null)
            return string.Empty;


        if (isRounded && isOutline)
            return GetEnum<ButtonRoundedOutline>(style).GeRoundedOutlineCSS();

        if (isRounded)
            return GetEnum<ButtonRounded>(style).GeRoundedCSS();

        if (isOutline)
            return GetEnum<ButtonOutline>(style).GetOutlineCSS();


        return GetEnum<ButtonStyle>(style).GetStyleCSS();
    }

    private T GetEnum<T>(string style) where T : struct
    {
        var type = typeof(T);
        var foundedStyle = Enum.GetNames(type).FirstOrDefault(e => e.ToUpper() == style.ToUpper());

        if (foundedStyle == null) return default(T);

        var foundedEnum = (T)Enum.Parse(type, foundedStyle, false);

        return (T)foundedEnum;
    }
}
