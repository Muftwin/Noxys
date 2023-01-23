using Godot;
using System;

public partial class Level_Menu : Node2D
{

	Node ActiveNode;
	bool OpenLevel;
	string path;





	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}
	public void _on_Level_Menu_child_entered_tree(Node node)
{

ActiveNode = node;
OpenLevel = true;
path = ActiveNode.GetPath();
path = path.RemoveAt(0,17);
}


public override void _Process(float delta)
{


if (Input.IsKeyPressed((int)KeyList.M))
{
if(OpenLevel)
{
this.RemoveChild(GetNode(path));

	Show();
	OpenLevel = false;
}


}
	
}
}



