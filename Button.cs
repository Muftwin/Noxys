using Godot;
using System;

public class Button : Godot.Button
{


string buttonnum;
				public Node NextScene { get; set; }
		string path;
		Node2D Menu;
	public override void _Ready()
	{
		buttonnum = this.Text;
		Menu = GetNode<Node2D>("/root/Level_Menu");
		path =  "Level " + buttonnum + ".tscn";
	}


public void LevelSelected(string path)
{





	var nextScene = (PackedScene)GD.Load(path);
	
if(nextScene != null)
{
	Menu.Hide();
	NextScene = nextScene.Instance();

Menu.AddChild(NextScene);

}


}
	


public void _on_Button_mouse_entered()
{

}
public void _on_Button_mouse_exited()
{

}


public void _on_Button_pressed()
{
	
CallDeferred(nameof(LevelSelected), path);

}
}


