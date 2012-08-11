// Matrices
float4x4 World;
float4x4 View;
float4x4 Projection;

// Light related
float4 AmbientColor;
float AmbientIntensity;

// Diffuse
float3 LightDirection;
float4 DiffuseColor;
float DiffuseIntensity;

// Specular
float4 SpecularColor;
float3 EyePosition;

// Texture
texture2D ColorMap;
sampler2D ColorMapSampler = sampler_state {
	Texture = <ColorMap>;
	MinFilter = linear;
	MagFilter = linear;
	MipFilter = linear;
};


// The input for the vertex shader
struct VertexShaderInput {
	float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

// The output from the vertex shader
struct VertexShaderOutput {
	float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float3 Normal : TEXCOORD1;
	float3 View : TEXCOORD2;
};

// The vertex shader
VertexShaderOutput VertexShaderFunction(VertexShaderInput input, float3 Normal : NORMAL) {
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
	float3 normal = normalize(mul(Normal, World));
	output.Normal = normal;
	output.View = normalize(float4(EyePosition, 1.0) - worldPosition);
	output.TexCoord = input.TexCoord;

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0 {

	float4 color = tex2D(ColorMapSampler, input.TexCoord);

	float4 norm = float4(input.Normal, 1.0);
	float4 diffuse = saturate(dot(-LightDirection, norm));
	float4 reflect = normalize(2*diffuse*norm-float4(LightDirection, 1.0));
	float4 specular = pow(saturate(dot(reflect, input.View)), 5); // last parameter is shininess

	return 	color * AmbientColor*AmbientIntensity + 
			color * DiffuseIntensity*DiffuseColor*diffuse + 
			color * SpecularColor*specular;
}

// Our technique
technique Technique1 {
	pass Pass1 {
		VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}