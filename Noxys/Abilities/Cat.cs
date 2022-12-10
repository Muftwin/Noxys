using Godot;
public class Cat : Ability
{
    Player player;
    bool enabled = true; //allowed to use at all
    bool currentTransform = false; //currently using
    public Cat(Player player)
    {
        this.player = player;
    }

    public bool transform()
    {
        if (!enabled)
            return false;

        player.character = Player.Character.CAT;
        player.setSprite("cat.png");
        currentTransform = true;
        return true;
    }
    public void passive()
    {
        if (!enabled || !currentTransform)
            return;

    }
}