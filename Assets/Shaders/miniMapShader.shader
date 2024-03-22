Shader "Custom/GradientTransparentShader" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader{
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 100

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    float edgeDist = min(i.uv.x, min(i.uv.y, min(1.0 - i.uv.x, 1.0 - i.uv.y)));
                    float alpha = smoothstep(0.0, 0.3, edgeDist);
                    col.a *= alpha;
                    return col;
                }
                ENDCG
            }
    }
}