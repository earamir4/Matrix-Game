Shader "Custom/CartesianGraphShader"
{
	Properties
	{
		_MainColor ("Background Color", Color) = (1,1,1,1)
		_MainTex ("Background Texture", 2D) = "white" {}
		_LinesColor("Lines Color", Color) = (1,1,1,1)
		_LinesTex("Lines Texture", 2D) = "white" {}
		_GraphSize ("Lines in Graph", Range(1,1000)) = 10
		_WidthOfMainLine("Width of Main Lines", Range(0,.005)) = .001
	}
	SubShader
		{
		Tags { "RenderType"="Opaque" }

		CGPROGRAM
		#pragma surface surf Lambert

		struct Input {
			float2 uv_MainTex;
		};

		sampler2D _MainTex;
		fixed4 _MainColor;

		sampler2D _LinesTex;
		fixed4 _LinesColor;

		int _GraphSize;
		float _WidthOfMainLine;

		void surf (Input IN, inout SurfaceOutput o)
		{
			//background
			fixed4 mainC = tex2D (_MainTex, IN.uv_MainTex) * _MainColor;

			//lines
			fixed2 linesUV = IN.uv_MainTex * (.1* _GraphSize);
			fixed4 linesC = tex2D(_LinesTex, linesUV) * _LinesColor;

			//main lines
			if (abs(IN.uv_MainTex.x - .5) < _WidthOfMainLine || abs(IN.uv_MainTex.y - .5) < _WidthOfMainLine)
			{
				o.Albedo = 0;
			}
			else
			{
				o.Albedo = mainC.rgb * linesC.rgb;
			}
			
			o.Alpha = mainC.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
