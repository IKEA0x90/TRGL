sampler2D input : register(s0);

float4 outlineColor : register(c0);   // Outline color to glow (input)
float4 glowColor : register(c1);      // Glow color (input)
float threshold : register(c2);       // Tolerance for matching outline color
float glowStrength : register(c3);    // Glow intensity
float glowRadius : register(c4);      // Glow blur radius

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 col = tex2D(input, uv);

    // --- NEW: convert to straight-alpha so the colour is faithful ---
    float3 colStraight = col.a > 0 ? col.rgb / col.a : float3(0, 0, 0);

    // Is this pixel the outline colour?
    float match = distance(colStraight, outlineColor.rgb) < threshold ? 1 : 0;

    // Sample neighbours the same way
    float glow = 0;
    const int samples = 12;
    const float PI2 = 6.283185;
    for (int i = 0; i < samples; ++i)
    {
        float2 offset = float2(cos(PI2 * i / samples), sin(PI2 * i / samples)) * glowRadius;
        float4 s  = tex2D(input, uv + offset);
        float3 sStraight = s.a > 0 ? s.rgb / s.a : float3(0,0,0);
        glow += distance(sStraight, outlineColor.rgb) < threshold ? 1 : 0;
    }
    glow /= samples;

    float4 glowCol = glowColor * glow * glowStrength;
    return lerp(col, col + glowCol, glow);
}