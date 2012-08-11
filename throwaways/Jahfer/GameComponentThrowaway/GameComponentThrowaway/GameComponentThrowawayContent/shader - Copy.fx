// Matrix
float4x4 World;
float4x4 View;
float4x4 Projection;

// Light related
float4 AmbientColor;
float AmbientIntensity;

// The input for the vertex shader
struct VertexShaderInput {
	float4 Position : POSITION0;
};

// The output from the vertex shader
struct VertexShaderOutput {
	float4 Position : POSITION0;
};

// The vertex shader
VertexShaderOutput VertexShaderFunction(VertexShaderInput input) {
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0 {
	return AmbientColor * AmbientIntensity;
}

// Our technique
technique Technique1 {
	pass Pass1 {
		VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}