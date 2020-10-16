Shader "TNTC/DissolveSurface" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Bumpmap", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
        _EmissionMap ("Emission Map", 2D) = "black" {}
        [HDR] _EmissionColor ("Emission Color", Color) = (0,0,0)
 

		_DissolveTexture("Dissolve Texutre", 2D) = "white" {} 
		_Amount("Amount", Range(0,1)) = 0
	}
 
	SubShader {
		Tags { 
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
        }
		
		Cull Off 
 
		CGPROGRAM
		#pragma surface surf Standard addshadow
 
		#pragma target 3.0
 
		sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _EmissionMap;
        half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _EmissionColor;
		sampler2D _DissolveTexture;
		half _Amount;
 
		struct Input {
			float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_EmissionMap;
		};
 
		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			half dissolve_value = tex2D(_DissolveTexture, IN.uv_MainTex).r;
			clip(dissolve_value - _Amount);
 
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color; 
            half4 emission = tex2D(_EmissionMap, IN.uv_EmissionMap) * _EmissionColor;
 
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
            o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
            o.Emission = emission;
            
		}
		ENDCG
	}

	FallBack Off
}