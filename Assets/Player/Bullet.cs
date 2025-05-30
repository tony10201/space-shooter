using Godot;
using System;


public partial class Bullet : Node3D
{

    [Export] private float bulletSpeed = 10f;

    public override void _Ready()
    {
        VisibleOnScreenNotifier3D onScreen = GetNode<VisibleOnScreenNotifier3D>("onScreen");
    }

    public override void _Process(double delta)
    {

        Transform = Transform.Translated(Vector3.Forward * bulletSpeed * (float)delta );
    }

    public void OnNotVisable()
    {
        QueueFree();
    }
}
