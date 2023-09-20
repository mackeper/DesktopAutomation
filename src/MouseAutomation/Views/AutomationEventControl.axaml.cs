using Avalonia;
using Avalonia.Controls.Primitives;

namespace MouseAutomation.Views;

public class AutomationEventControl : TemplatedControl
{
    public static readonly StyledProperty<string> EventTextProperty
        = AvaloniaProperty.Register<AutomationEventControl, string>(nameof(EventText), "Mouse click");

    public string EventText
    {
        get => GetValue(EventTextProperty);
        set => SetValue(EventTextProperty, value);
    }

    public static readonly StyledProperty<string> EventDetailsProperty =
        AvaloniaProperty.Register<AutomationEventControl, string>(nameof(EventDetails), "50ms | (X, Y)");

    public string EventDetails
    {
        get => GetValue(EventDetailsProperty);
        set => SetValue(EventDetailsProperty, value);
    }
}