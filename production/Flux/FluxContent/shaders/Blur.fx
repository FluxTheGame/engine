float2 TextureSize;
float BlurAmt;

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
float4 BlurHorizontalFunction(float2 TextureCoordinate : TEXCOORD0) : COLOR0
{
	float2 coord = TextureCoordinate * TextureSize;
	//float4 color = tex2D(TextureSampler, (coord / TextureSize));

	float4 final = float4(0,0,0,0);

	final += 1.0 * tex2D(TextureSampler, TextureCoordinate + float2(BlurAmt * -4.0, 0.0));
	final += 2.0 * tex2D(TextureSampler, TextureCoordinate + float2(BlurAmt * -3.0, 0.0));
	final += 3.0 * tex2D(TextureSampler, TextureCoordinate + float2(BlurAmt * -2.0, 0.0));
	final += 4.0 * tex2D(TextureSampler, TextureCoordinate + float2(BlurAmt * -1.0, 0.0));

	final += 5.0 * tex2D(TextureSampler, TextureCoordinate + float2(BlurAmt, 0.0));

	final += 4.0 * tex2D(TextureSampler, TextureCoordinate + float2(BlurAmt * 1.0, 0.0));
	final += 3.0 * tex2D(TextureSampler, TextureCoordinate + float2(BlurAmt * 2.0, 0.0));
	final += 2.0 * tex2D(TextureSampler, TextureCoordinate + float2(BlurAmt * 3.0, 0.0));
	final += 1.0 * tex2D(TextureSampler, TextureCoordinate + float2(BlurAmt * 4.0, 0.0));

	final /= 25.0;

	//loat2 clampedCoord = clamp(coord, float2(0, 0), TextureSize);
    return final;
}

float4 BlurVerticalFunction(float2 TextureCoordinate : TEXCOORD0) : COLOR0
{
	float2 coord = TextureCoordinate * TextureSize;
	//float4 color = tex2D(TextureSampler, (coord / TextureSize));

	float4 final = float4(0,0,0,0);

	final += 1.0 * tex2D(TextureSampler, TextureCoordinate + float2(0.0, BlurAmt * 4.0));
	final += 2.0 * tex2D(TextureSampler, TextureCoordinate + float2(0.0, BlurAmt * 3.0));
	final += 3.0 * tex2D(TextureSampler, TextureCoordinate + float2(0.0, BlurAmt * 2.0));
	final += 4.0 * tex2D(TextureSampler, TextureCoordinate + float2(0.0, BlurAmt * 1.0));

	final += 5.0 * tex2D(TextureSampler, TextureCoordinate + float2(0.0, BlurAmt));

	final += 4.0 * tex2D(TextureSampler, TextureCoordinate + float2(0.0, BlurAmt * -1.0));
	final += 3.0 * tex2D(TextureSampler, TextureCoordinate + float2(0.0, BlurAmt * -2.0));
	final += 2.0 * tex2D(TextureSampler, TextureCoordinate + float2(0.0, BlurAmt * -3.0));
	final += 1.0 * tex2D(TextureSampler, TextureCoordinate + float2(0.0, BlurAmt * -4.0));

	final /= 25.0;
	final.a = 0.2;

	//float2 clampedCoord = clamp(coord, float2(0, 0), TextureSize);
    return final;
	/*float2 coord = TextureCoordinate * TextureSize;
	float4 color = tex2D(TextureSampler, (coord / TextureSize));

	float2 clampedCoord = clamp(coord, float2(0, 0), TextureSize);
    return color;*/
}
 
//-------------------------- TECHNIQUES ----------------------------------------
// This technique is pretty simple - only one pass, and only a pixel shader
technique BlurHorizontal
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 BlurHorizontalFunction();
    }
}

technique BlurVertical
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 BlurVerticalFunction();
    }
}
