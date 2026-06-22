using Godot;
using Godot.Collections;
using System;

public partial class Cutscene : Control
{
    Dictionary<String, Control> cutscenes = new();
    int index;
    int end;
    Control currentScene = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (Control node in GetChildren())
        {
            node.Visible = false;
            node.MouseFilter = MouseFilterEnum.Ignore;
            cutscenes.Add(node.Name, node);
        }
        ProcessMode = Node.ProcessModeEnum.Always;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

        if (currentScene != null)
        {
            if (Input.IsActionJustPressed("ui_accept"))
            {
                Next();
            }
        }
        else
        {

            if (Input.IsKeyPressed(Key.Key1))
            {
                GD.Print("Showing");
                Show("CutScene1");
            }
        }

    }

    public void Show(string name)
    {
        GetTree().Paused = true;

        currentScene = cutscenes[name];
        currentScene.Visible = true;
        MouseFilter = MouseFilterEnum.Pass;

        index = 0;
        end = currentScene.GetChildCount();

        // 🔥 HARD RESET ALL PAGES
        for (int i = 0; i < end; i++)
        {
            if (currentScene.GetChild(i) is Control c)
                c.Visible = (i == 0);
        }
    }

    public void Next()
    {
        if (currentScene == null) return;

        currentScene.GetChild<Control>(index).Visible = false;

        index++;

        if (index >= end)
        {
            End();
            return;
        }

        currentScene.GetChild<Control>(index).Visible = true;
    }

    public void End()
    {
        foreach (Control node in GetChildren())
        {
            node.Visible = false;
        }

        currentScene.Visible = false;
        currentScene.Visible = false;
        currentScene = null;
        MouseFilter = MouseFilterEnum.Ignore;
        GetTree().Paused = false;
    }
}
