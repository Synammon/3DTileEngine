#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// input parameters
matrix World;           // World matix
matrix View;            // View matrix
matrix Projection;      // Projection Matrix

Texture TextureA;       // primary texture.
sampler TextureSamplerA = sampler_state
{
    texture = <TextureA>;
};
//_______________________________________________________________
// techniques 
// Quad Draw  Position Color Texture
//_______________________________________________________________
struct VsInputQuad
{
    float4 Position : POSITION0;                // Vertex position
    float4 Color : COLOR0;                      // Vertext color
    float2 TexureCoordinateA : TEXCOORD0;       // UV coordinate
};
struct VsOutputQuad
{
    float4 Position : SV_Position;              // Vertex position
    float4 Color : COLOR0;                      // Vertex color
    float2 TexureCoordinateA : TEXCOORD0;       // UV coordinate
};
struct PsOutputQuad
{
    float4 Color : COLOR0;                      // Color
};
// ____________________________
VsOutputQuad VertexShaderQuadDraw(VsInputQuad input)
{
    VsOutputQuad output;                        // Output
    float4x4 wvp = mul(World, mul(View, Projection));
    output.Position = mul(input.Position, wvp); // Transform by WorldViewProjection
    output.Color = input.Color;                 // return the input color
    output.TexureCoordinateA = input.TexureCoordinateA; // return the input texture coordinate
    return output;
}
PsOutputQuad PixelShaderQuadDraw(VsOutputQuad input)
{
    PsOutputQuad output;
    output.Color = tex2D(TextureSamplerA, input.TexureCoordinateA) * input.Color;
    return output;
}

technique QuadDraw
{
    pass
    {
        VertexShader = compile VS_SHADERMODEL VertexShaderQuadDraw();
        PixelShader = compile PS_SHADERMODEL PixelShaderQuadDraw();
    }
}