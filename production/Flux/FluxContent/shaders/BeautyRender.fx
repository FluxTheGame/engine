float2 TextureSize;

//------------------------------ TEXTURE PROPERTIES ----------------------------
// This is the texture that SpriteBatch will try to set before drawing
texture ScreenTexture;
// Our sampler for the texture, which is just going to be pretty simple
sampler TextureSampler = sampler_state
{
    Texture = <ScreenTexture>;
};

// Texture
texture2D NoiseMap;
sampler2D NoiseMapSampler = sampler_state {
	Texture = <NoiseMap>;
	MinFilter = linear;
	MagFilter = linear;
	MipFilter = linear;
};
 
//------------------------ PIXEL SHADER ----------------------------------------
// This pixel shader will simply look up the color of the texture at the
// requested point
float4 PixelShaderFunc(float2 TextureCoordinate : TEXCOORD0) : COLOR0
{
	float2 coord = TextureCoordinate * TextureSize;
	float4 color = tex2D(TextureSampler, TextureCoordinate);
	float4 mapSample = tex2D(NoiseMapSampler, TextureCoordinate);

	//loat2 clampedCoord = clamp(coord, float2(0, 0), TextureSize);
    return color;
}
 
//-------------------------- TECHNIQUES ----------------------------------------
// This technique is pretty simple - only one pass, and only a pixel shader
technique Pretty
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunc();
    }
}