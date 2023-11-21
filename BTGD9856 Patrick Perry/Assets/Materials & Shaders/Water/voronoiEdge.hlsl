inline float2 randomVector(float2 UV, float offset) 
{
	float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
	UV = frac(sin(mul(UV, m)) * 46839.32);
	return float2(sin(UV.y * +offset) * 0.5 + 0.5, cos(UV.x * offset) * 0.5 + 0.5);
}

void CustomVoronoi_float(float2 UV, float AngleOffset, float CellDensity, out float DistFromCenter, out float DistFromEdge)
{
	int2 cell = floor(UV * CellDensity);
	float2 posInCell = frac(UV * CellDensity);

	DistCromCenter = 8.0f;
}