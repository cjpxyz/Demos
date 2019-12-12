// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Triplebrick/TwoLayerStandard"
{
	Properties
	{
		_BaseColorA("Base Color A", 2D) = "white" {}
		_NormalA("Normal A", 2D) = "white" {}
		_NormalAScale("Normal A Scale", Range( 0 , 1)) = 1
		_BaseColorB("BaseColor B", 2D) = "white" {}
		_NormalB("Normal B", 2D) = "white" {}
		_NormalBScale("Normal B Scale", Range( 0 , 1)) = 1
		_HeightMaskAlpha("Height + Mask (Alpha)", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 2)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NormalAScale;
		uniform sampler2D _NormalA;
		uniform float4 _NormalA_ST;
		uniform float _NormalBScale;
		uniform sampler2D _NormalB;
		uniform float4 _NormalB_ST;
		uniform sampler2D _HeightMaskAlpha;
		uniform float4 _HeightMaskAlpha_ST;
		uniform sampler2D _BaseColorA;
		uniform float4 _BaseColorA_ST;
		uniform sampler2D _BaseColorB;
		uniform float4 _BaseColorB_ST;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalA = i.uv_texcoord * _NormalA_ST.xy + _NormalA_ST.zw;
			float2 uv_NormalB = i.uv_texcoord * _NormalB_ST.xy + _NormalB_ST.zw;
			float2 uv_HeightMaskAlpha = i.uv_texcoord * _HeightMaskAlpha_ST.xy + _HeightMaskAlpha_ST.zw;
			float4 tex2DNode30 = tex2D( _HeightMaskAlpha, uv_HeightMaskAlpha );
			float3 lerpResult7 = lerp( UnpackScaleNormal( tex2D( _NormalA, uv_NormalA ) ,_NormalAScale ) , UnpackScaleNormal( tex2D( _NormalB, uv_NormalB ) ,_NormalBScale ) , tex2DNode30.a);
			o.Normal = lerpResult7;
			float2 uv_BaseColorA = i.uv_texcoord * _BaseColorA_ST.xy + _BaseColorA_ST.zw;
			float4 tex2DNode1 = tex2D( _BaseColorA, uv_BaseColorA );
			float2 uv_BaseColorB = i.uv_texcoord * _BaseColorB_ST.xy + _BaseColorB_ST.zw;
			float4 tex2DNode2 = tex2D( _BaseColorB, uv_BaseColorB );
			float4 lerpResult6 = lerp( tex2DNode1 , tex2DNode2 , tex2DNode30.a);
			o.Albedo = lerpResult6.rgb;
			float lerpResult8 = lerp( tex2DNode1.a , tex2DNode2.a , tex2DNode30.a);
			float temp_output_39_0 = ( lerpResult8 * _Smoothness );
			float clampResult38 = clamp( temp_output_39_0 , 0.0 , 1.0 );
			o.Smoothness = clampResult38;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
8;100;1205;789;2308.593;291.9361;1;True;False
Node;AmplifyShaderEditor.TexturePropertyNode;28;-2012.922,-76.91004;Float;True;Property;_HeightMaskAlpha;Height + Mask (Alpha);6;0;Create;True;0;0;False;0;None;6a700239d392b0e47a76a2e33d01666c;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;2;-782.124,-156.6205;Float;True;Property;_BaseColorB;BaseColor B;3;0;Create;True;0;0;False;0;None;6aa34313375c232459d7de6dfee3a500;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;30;-1611.669,-62.87793;Float;True;Property;_TextureSample0;Texture Sample 0;8;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-793.0571,-369.4553;Float;True;Property;_BaseColorA;Base Color A;0;0;Create;True;0;0;False;0;None;d2c0802492fd93447b2390f2ee2ba6d4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;35;-563.9111,58.06955;Float;False;Property;_Smoothness;Smoothness;7;0;Create;True;0;0;False;0;1;0.74;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;8;-374.5288,-117.6219;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-1188.556,432.2158;Float;False;Property;_NormalAScale;Normal A Scale;2;0;Create;True;0;0;False;0;1;0.592;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1176.556,610.2158;Float;False;Property;_NormalBScale;Normal B Scale;5;0;Create;True;0;0;False;0;1;0.813;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-789.7111,313.1663;Float;True;Property;_NormalA;Normal A;1;0;Create;True;0;0;False;0;None;5a8b4e10eaa58024c89a5d503d83bed8;True;0;True;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-225.6329,-5.028137;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-789.7278,505.6085;Float;True;Property;_NormalB;Normal B;4;0;Create;True;0;0;False;0;None;2d86f82ddd854624dbefe091f065e909;True;0;True;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;33;-173.7161,165.1931;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-535.6162,157.3806;Float;False;Property;_SmoothnessContrast;Smoothness Contrast;8;0;Create;True;0;0;False;0;1;0;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;6;-376.5826,-248.2689;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;38;42.5752,111.0944;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;7;-403.5092,443.7461;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;210.5282,-4.419421;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Triplebrick/TwoLayerStandard;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;30;0;28;0
WireConnection;8;0;1;4
WireConnection;8;1;2;4
WireConnection;8;2;30;4
WireConnection;3;5;31;0
WireConnection;39;0;8;0
WireConnection;39;1;35;0
WireConnection;4;5;32;0
WireConnection;33;0;39;0
WireConnection;33;1;36;0
WireConnection;6;0;1;0
WireConnection;6;1;2;0
WireConnection;6;2;30;4
WireConnection;38;0;39;0
WireConnection;7;0;3;0
WireConnection;7;1;4;0
WireConnection;7;2;30;4
WireConnection;0;0;6;0
WireConnection;0;1;7;0
WireConnection;0;4;38;0
ASEEND*/
//CHKSM=45766BA7D8DD8241D1145996A1916998D3EAE381