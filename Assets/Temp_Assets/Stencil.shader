﻿Shader "Sprites/Stencil"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
    }
        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }
            Cull Off
            Lighting Off
            ZWrite Off
            Blend One OneMinusSrcAlpha
            Pass
            {
                Stencil {
                    Ref 1
                    Comp Greater
                    Pass replace
                    Fail Zero
                }
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                struct appdata {
                    float4 vertex : POSITION;
                };
                struct v2f {
                    float4 pos : SV_POSITION;
                };
                v2f vert(appdata v) {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    return o;
                }
                half4 frag(v2f i) : SV_Target {
                    return half4(0,0,0,0);
                }
            ENDCG
            }
            Pass
            {
                Stencil {
                    Ref 1
                    Comp equal
                    Pass keep
                }
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile _ PIXELSNAP_ON
                #include "UnityCG.cginc"
                struct appdata_t
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 texcoord : TEXCOORD0;
                };
                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord  : TEXCOORD0;
                };
                fixed4 _Color;
                v2f vert(appdata_t IN)
                {
                    v2f OUT;
                    OUT.vertex = UnityObjectToClipPos(IN.vertex);
                    OUT.texcoord = IN.texcoord;
                    OUT.color = IN.color * _Color;
                    #ifdef PIXELSNAP_ON
                    OUT.vertex = UnityPixelSnap(OUT.vertex);
                    #endif
                    return OUT;
                }
                sampler2D _MainTex;
                sampler2D _AlphaTex;
                float _AlphaSplitEnabled;
                fixed4 SampleSpriteTexture(float2 uv)
                {
                    fixed4 color = tex2D(_MainTex, uv);
    #if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
                    if (_AlphaSplitEnabled)
                        color.a = tex2D(_AlphaTex, uv).r;
    #endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED
                    return color;
                }
                fixed4 frag(v2f IN) : SV_Target
                {
                    fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
                    c.rgb *= c.a;
                    return c;
                }
            ENDCG
            }
        }
}
