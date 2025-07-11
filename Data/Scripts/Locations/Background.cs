using Godot;
using System;
using System.Collections.Generic;

public partial class Background : TileMapLayer
{
	public override void _Ready()
	{
		NavigationRegion2D navigation = new NavigationRegion2D();
        NavigationPolygon navPolygon = new NavigationPolygon();
        navigation.NavigationPolygon = navPolygon;
        navPolygon.SetVertices(new Vector2[] { });
        NavigationMeshSourceGeometryData2D sourceGeometry = new NavigationMeshSourceGeometryData2D();
        foreach (var cell in GetUsedCells())
        {
            var tileData = GetCellTileData(cell);
            if (tileData != null && tileData.GetCollisionPolygonsCount(0) > 0)
            {
                Vector2 tilePosition = MapToLocal(cell);
                for (int i = 0; i < tileData.GetCollisionPolygonsCount(0); i++)
                {
                    var polygon = tileData.GetCollisionPolygonPoints(0, i);
                    navPolygon.AddOutline(polygon);
                    var globalPolygon = new Vector2[polygon.Length];
                    for (int j = 0; j < polygon.Length; j++)
                    {
                        globalPolygon[j] = polygon[j] + tilePosition;
                    }
                    sourceGeometry.AddTraversableOutline(globalPolygon);
                    navPolygon.AddOutline(polygon);
                }
            }
        }
        NavigationServer2D.ParseSourceGeometryData(navPolygon, sourceGeometry, GetTree().CurrentScene);
        NavigationServer2D.BakeFromSourceGeometryData(navPolygon, sourceGeometry);
        GetTree().CurrentScene.CallDeferred("add_child", navigation);
    }
}
