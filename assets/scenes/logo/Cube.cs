using Godot;
using System;

[Tool]
public class Cube : MeshInstance
{
	public override void _Process(float delta)
	{
		Rotate(new Vector3(1, 1, 1).Normalized(), 2f * delta);
	}

}
