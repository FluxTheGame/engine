float2 TextureSize;
float Radius;
float Angle;
float2 Center;

//------------------------------ TEXTURE PROPERTIES ----------------------------
// This is the texture that SpriteBatch will try to set before drawing
texture ScreenTexture;
 
// Our sampler for the texture, which is just going to be pretty simple
sampler TextureSampler = sampler_state
{
    Texture = <ScreenTexture>;
};
 
//------------------------ PIXEL SHADER ----------------------------------------
// This pixel shader will simply look up the color of the texture at the
// requested point
float4 PixelShaderFunction(float2 TextureCoordinate : TEXCOORD0) : COLOR0
{
	float2 coord = TextureCoordinate * TextureSize;
	coord -= Center;
	float distance = length(coord);
	if (distance < Radius) {
		float percent = (Radius - distance) / Radius;
		float theta = percent * percent * Angle;
		float s = sin(theta);
		float c = cos(theta);
		coord = float2(
			coord.x * c - coord.y * s,
			coord.x * s + coord.y * c
		);
	}
	
	coord += Center;

	float4 color = tex2D(TextureSampler, (coord / TextureSize));
	float2 clampedCoord = clamp(coord, float2(0, 0), TextureSize);
 
    return color;
}
 
//-------------------------- TECHNIQUES ----------------------------------------
// This technique is pretty simple - only one pass, and only a pixel shader
technique Plain
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

