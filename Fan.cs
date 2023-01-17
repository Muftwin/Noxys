using Godot;

public partial class Fan : Node
{
	private void _on_area_2d_body_entered(Node2D node)
	{
		if (node is not Player player)
			return;

		player.InFanZone = true;
	}

	private void _on_area_2d_body_exited(Node2D node)
	{
		if (node is not Player player)
			return;

		player.InFanZone = false;
	}
}
