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


#define Blend(base, blend, funcf)     float3(funcf(base.r, blend.r), funcf(base.g, blend.g), funcf(base.b, blend.b))

#define BlendOverlay(base, blend)  (base < 0.5 ? (2.0 * base * blend) : (1.0 - 2.0 * (1.0 - base) * (1.0 - blend)))
#define BlendSoftLight(base, blend)  (1 - 2*blend) * (base*base) + 2*blend*base

 
//------------------------ PIXEL SHADER ----------------------------------------
// This pixel shader will simply look up the color of the texture at the
// requested point
float4 PixelShaderFunc(float2 TextureCoordinate : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(TextureSampler, TextureCoordinate);

    //float4 midColor = color;
    float3 midColor = lerp(color, Blend(color, float3(0.415,0.526,0.706), BlendSoftLight), 0.35);

	float4 mapSample = tex2D(NoiseMapSampler, TextureCoordinate);
    mapSample.a = 0.02;

    float3 colFinal = lerp(midColor, Blend(midColor, mapSample, BlendOverlay), mapSample.a);
    return float4(colFinal, 1.0);
}
 
//-------------------------- TECHNIQUES ----------------------------------------
// This technique is pretty simple - only one pass, and only a pixel shader
technique Pretty
{
    pass Pass1
    {        
        AlphaBlendEnable = TRUE;
        DestBlend = INVSRCALPHA;
        SrcBlend = SRCALPHA;

        PixelShader = compile ps_2_0 PixelShaderFunc();
    }
}