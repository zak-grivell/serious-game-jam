using Godot;
using System;
using System.Collections.Generic;

public partial class LevelTile : Node
{
	[Export] private TileMapLayer tilemap;
	[Export] private Vector2i atlasCoords;
	[Export] private TileScript[] scripts;
	
	public void Invoke() {
		foreach(TileScript scr in scripts) scr.Invoke();
	}
}
