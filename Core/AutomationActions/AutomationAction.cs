namespace Domain.AutomationActions;
public interface IAutomationAction
{
    string Name { get; }
    int DelayBeforeInMilliseconds { get; }
}
