using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiDiagram : MonoBehaviour
{
	public int[] GetDiagram(int regionAmount, int nbBiomeTypes)
	{
		Vector2Int[] centroids = new Vector2Int[regionAmount];
		int[] regions = new int[regionAmount];
		for(int i = 0; i < regionAmount; i++)
		{
			centroids[i] = new Vector2Int(Random.Range(0, MapGenerator.mapSize), Random.Range(0, MapGenerator.mapSize));
			regions[i] = Random.Range(0, nbBiomeTypes);
		}
		int[] pixelColors = new int[MapGenerator.mapSize * MapGenerator.mapSize];
		for(int x = 0; x < MapGenerator.mapSize; x++)
		{
			for(int y = 0; y < MapGenerator.mapSize; y++)
			{
				int index = x * MapGenerator.mapSize + y;
				pixelColors[index] = regions[GetClosestCentroidIndex(new Vector2Int(x, y), centroids)];
			}
		}
		return pixelColors;
	}

	int GetClosestCentroidIndex(Vector2Int pixelPos, Vector2Int[] centroids)
	{
		float smallestDst = float.MaxValue;
		int index = 0;
		for(int i = 0; i < centroids.Length; i++)
		{
			if (Vector2.Distance(pixelPos, centroids[i]) < smallestDst)
			{
				smallestDst = Vector2.Distance(pixelPos, centroids[i]);
				index = i;
			}
		}
		return index;
	}
}
