using InControl;

public class UIActions : PlayerActionSet
{
    public PlayerAction Start;
    public PlayerAction Select;
    public PlayerAction Back;

    public UIActions()
    {
        Start = CreatePlayerAction("Start");
        Select = CreatePlayerAction("Select");
        Back = CreatePlayerAction("Back");

        Start.AddDefaultBinding(InputControlType.Start);
        Select.AddDefaultBinding(InputControlType.Action1);
        Back.AddDefaultBinding(InputControlType.Action2);
    }
}
