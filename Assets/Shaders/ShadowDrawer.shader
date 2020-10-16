Shader "TNTC/ShadowDrawer"{

    Properties{
        _Color ("Shadow Color", Color) = (0, 0, 0, 0.6)
        [Toggle] _BaseShadow ("Base Shadow", Float) = 1
        [Toggle] _AddShadow ("Add Shadow", Float) = 1
    }

    CGINCLUDE

    #include "UnityCG.cginc"
    #include "AutoLight.cginc"

    struct v2f_shadow {
        float4 pos : SV_POSITION;
        LIGHTING_COORDS(0, 1)
    };

    half4 _Color;
    float _BaseShadow;
    float _AddShadow;

    v2f_shadow vertShadow(appdata_full v){
        v2f_shadow o;
        o.pos = UnityObjectToClipPos(v.vertex);
        TRANSFER_VERTEX_TO_FRAGMENT(o);
        return o;
    }

    half4 fragBaseShadow(v2f_shadow IN) : SV_Target{
        half atten = LIGHT_ATTENUATION(IN);
        return half4(_Color.rgb, lerp(_Color.a, 0, atten)) * _BaseShadow;
    }

    half4 fragAddShadow(v2f_shadow IN) : SV_Target{
        half atten = LIGHT_ATTENUATION(IN);

        return half4(_Color.rgb, lerp(_Color.a, 0, atten)) * _AddShadow;
    }

    ENDCG

    SubShader{
        Tags { "Queue"="AlphaTest+49" }

        Pass{
            ColorMask 0

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            struct v2f {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_full v){
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                return o;
            }

            half4 frag(v2f IN) : SV_Target{
                return (half4)0;
            }

            ENDCG
        }

        Pass{
            Tags { "LightMode" = "ForwardBase" }
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vertShadow
            #pragma fragment fragBaseShadow
            #pragma multi_compile_fwdbase
            ENDCG
        }

        Pass{
            Tags { "LightMode" = "ForwardAdd" }
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vertShadow
            #pragma fragment fragAddShadow
            #pragma multi_compile_fwdadd_fullshadows
            ENDCG
        }
    }
    
    FallBack "Mobile/VertexLit"
}