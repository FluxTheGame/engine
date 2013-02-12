float4x4 xLightsWorldViewProjection;
float4x4 xWorldViewProjection;

struct SMapPixelToFrame
{
	float4 Color : COLOR0;
};

struct SMapVertexToPixel
{
	float4 Position		: POSITION;
	float4 Position2D	: TEXCOORD0;
};

SMapVertexToPixel ShadowMapVertexShader(float4 inPos : POSITION)
{
	SMapVertexToPixel Output = (SMapVertexToPixel)0;
	Output.Position = mul(inPos, xLightsWorldViewProjection);
	Output.Position2D = Output.Position;

	return Output;
}


SMapPixelToFrame ShadowMapPixelShader(SMapVertexToPixel PSIn)
{
	SMapPixelToFrame Output = (SMapPixelToFrame)0;

	Output.Color = PSIn.Position2D.z / PSIn.Position2D.w;

	return Output;
}


technique ShadowMap
{
	pass Pass0 {
		VertexShader = compile vs_2_0 ShadowMapVertexShader();
		PixelShader = compile ps_2_0 ShadowMapPixelShader();
	}
}


Texture xShadowMap;

struct SSceneVertexToPixel
{
	float4 Position : POSITION;
	float4 Pos2DAsSeenByLight : TEXCOORD0;
};

struct SScenePixelToFrame
{
	float4 Color : COLOR0;
};


sampler ShadowMapSampler = sampler_state { 
	texture = <xShadowMap>;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
	AddressU = clamp;
	AddressV = clamp;
 };

 SSceneVertexToPixel ShadowedSceneVertexShader( float4 inPos : POSITION )
 {
 	SSceneVertexToPixel Output = (SSceneVertexToPixel)0;

 	Output.Position = mul(inPos, xWorldViewProjection);
 	Output.Pos2DAsSeenByLight = mul(inPos, xLightsWorldViewProjection);

 	return Output;
 }

 SScenePixelToFrame ShadowedScenePixelShader(SSceneVertexToPixel PSIn)
 {
 	SScenePixelToFrame Output = (SScenePixelToFrame)0;

 	float2 ProjectedTexCoords;
 	ProjectedTexCoords[0] = PSIn.Pos2DAsSeenByLight.x / PSIn.Pos2DAsSeenByLight.w / 2.0f + 0.5f;
 	ProjectedTexCoords[1] = -PSIn.Pos2DAsSeenByLight.y / PSIn.Pos2DAsSeenByLight.w / 2.0f + 0.5f;

 	Output.Color = tex2D(ShadowMapSampler, ProjectedTexCoords);

 	return Output;
 }

technique ShadowedScene
{
	pass Pass0
	{
		VertexShader = compile vs_2_0 ShadowedSceneVertexShader();
		PixelShader = compile ps_2_0 ShadowedScenePixelShader();
	}
}