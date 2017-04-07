using InControl;

public class UIActions : PlayerActionSet
{
    public PlayerAction Start;
    public PlayerAction Select;
    public PlayerAction Back;
    public PlayerAction Toggle;

    public UIActions()
    {
        Start = CreatePlayerAction("Start");
        Select = CreatePlayerAction("Select");
        Back = CreatePlayerAction("Back");
        Toggle = CreatePlayerAction("Toggle");

        Start.AddDefaultBinding(InputControlType.Start);
        Start.AddDefaultBinding(InputControlType.Options);
        Select.AddDefaultBinding(InputControlType.Action1);
        Back.AddDefaultBinding(InputControlType.Action2);
        Toggle.AddDefaultBinding(InputControlType.Action4);
    }
}
