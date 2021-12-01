// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "SolidColor" {
    Properties{
        _Color("Main Color", Color) = (1,1,1,1)
    }
        SubShader{
            Tags {"Queue" = "Geometry" "RenderType" = "Opaque"}

            Pass {
                Tags {"LightMode" = "ForwardBase"}
                CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag
                    #pragma multi_compile_fwdbase
                    #pragma fragmentoption ARB_precision_hint_fastest

                    #include "UnityCG.cginc"
                    #include "AutoLight.cginc"

                    struct appdata_pos {
                        float4 vertex : POSITION;
                    };

                    struct v2f
                    {
                        float4    pos            : SV_POSITION;
                        LIGHTING_COORDS(0,1)
                    };

                    v2f vert(appdata_pos v)
                    {
                        v2f o;
                        o.pos = UnityObjectToClipPos(v.vertex);
                        TRANSFER_VERTEX_TO_FRAGMENT(o);
                        return o;
                    }

                    fixed4 _Color;
                    fixed4 frag(v2f i) : COLOR
                    {
                        return _Color * LIGHT_ATTENUATION(i);
                    }
                ENDCG
            }
    }
        FallBack "VertexLit"    // Use VertexLit's shadow caster/reciever passes.
}