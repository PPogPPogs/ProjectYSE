Shader"Custom/IrisShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Cutoff ("Cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags { "Queue"="Overlay" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"
            
struct appdata_t
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};
            
struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};
            
sampler2D _MainTex;
float _Cutoff;
            
v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}
            
fixed4 frag(v2f i) : SV_Target
{
    fixed4 texcol = tex2D(_MainTex, i.uv);
    float dist = distance(i.uv, float2(0.5, 0.5));
    if (dist > _Cutoff)
        discard;
    return texcol;
}
            ENDCG
        }
    }
FallBack"Diffuse"
}