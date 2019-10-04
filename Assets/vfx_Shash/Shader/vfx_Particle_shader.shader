Shader "Custom/vfx_Particle_sha001"
{
    Properties
    {
		[HDR]_Color("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Unlit alpha:fade vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;

		struct appdata_particles
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 color : COLOR;
			float4 texcoord : TEXCOORD0;

			float4 CustomDataTest : TEXCOORD1; //커스텀 데이터
		};

        struct Input
        {
			float2 uv_MainTex;
			float2 texcoord;
			float4 color;

			float4 CustomDataFinal; //커스텀 데이터
        };

        fixed4 _Color;

		void vert (inout appdata_particles v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.uv_MainTex = v.texcoord.xy; //기본 UV표현을 위한 좌표1 (필수)
			o.texcoord = v.texcoord.zw; //기본 UV표현을 위한 좌표2 (필수)
			o.color = v.color;
			o.CustomDataFinal = v.CustomDataTest;
			o.uv_MainTex = v.texcoord.xy;
		}


        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			//위 if문들을 아래 step( x , y ) 함수로 대체함 

			float alpha = step( IN.CustomDataFinal.x, c.r );


            o.Emission = c.rgb;
            o.Alpha = alpha;
        }

		float4 LightingUnlit(SurfaceOutput s, float3 viewDir, float atten)
		{
			return float4(0, 0, 0, s.Alpha);
		}

        ENDCG
    }
    FallBack "Diffuse"
}
