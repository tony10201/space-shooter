using Godot;
using System;


public partial class Bullet : Node3D
{
    Timer killTimer;

    [Export] private float bulletSpeed = 10f;

    public override void _Ready()
    {
        killTimer = GetNode<Timer>("kill");
        killTimer.Timeout += OnNotVisable;
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
