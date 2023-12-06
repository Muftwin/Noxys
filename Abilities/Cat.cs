using Godot;
public partial class Cat : Ability
{
	Player player;
	bool enabled = true; //allowed to use at all
	bool currentTransform = false; //currently using

	public bool climbing = false;
	public Cat(Player player)
	{
		this.player = player;
	}

	public void transform()
	{
		if (!enabled) return;

		player.character = Player.Character.CAT;
		player.setSprite("Images/cat.png");
		currentTransform = true;
	}
	public void passive()
	{
		if (!enabled || !currentTransform) return;

		climbing = (player.IsOnWall() && player.speed[1] <= 0);

	}
}
