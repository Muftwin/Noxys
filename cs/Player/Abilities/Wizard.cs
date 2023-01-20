using Godot;
public partial class Wizard : Ability
{
    Player player;
    bool enabled = true; //allowed to use at all
    bool currentTransform = false; //currently using

    public bool climbing = false;
    public Wizard(Player player)
    {
        this.player = player;
    }

    public void transform()
    {
        if (!enabled) return;

        player.character = Player.Character.WIZARD;
        player.setSprite("Images/Wizard.png");
        currentTransform = true;
    }
    public void passive()
    {
        if (!enabled || !currentTransform) return;

    }
}