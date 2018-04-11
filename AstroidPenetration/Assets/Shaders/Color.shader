Shader "Custom/Color" {
	Properties{
		_Color("Tint", Color) = (1,1,1,1)
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows vertex:vert
		#pragma target 3.0

		struct Input {
			float3 color : COLOR;
		};

		sampler2D _MainTex;
		fixed4 _Color;

		void vert(inout appdata_full v) {
			v.color = tex2Dlod(_MainTex, v.texcoord) * _Color;
		}

		void surf(Input In, inout SurfaceOutputStandard o) {
			o.Albedo = In.color;
			o.Emission = In.color;
		}
	ENDCG
	}
	FallBack "Diffuse"
}
