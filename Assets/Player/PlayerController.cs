using Godot;
using System;

public partial class PlayerController : Node
{
    Node3D player;
    public override void _Ready()
    {
        player = this.GetParent<Node3D>();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }
}
