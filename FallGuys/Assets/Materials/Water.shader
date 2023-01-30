// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Water"
{
	Properties
	{
		_TestWater("TestWater", 2D) = "white" {}
		_Scale("Scale", Float) = 18
		_Speed("Speed", Float) = 0.1
		_DeepWater("DeepWater", Color) = (0,0.3093882,1,1)
		_ShadowWater("ShadowWater", Color) = (0,0.767278,1,1)
		_WaterDepth("WaterDepth", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _ShadowWater;
		uniform float4 _DeepWater;
		uniform sampler2D _TestWater;
		uniform float _Scale;
		uniform float _Speed;
		uniform float _WaterDepth;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_cast_0 = (_Scale).xx;
			float2 temp_cast_1 = (( _Speed * _Time.y )).xx;
			float2 uv_TexCoord3 = i.uv_texcoord * temp_cast_0 + temp_cast_1;
			float4 lerpResult10 = lerp( _ShadowWater , ( _DeepWater + ( 1.0 - tex2D( _TestWater, uv_TexCoord3 ).g ) ) , _WaterDepth);
			o.Emission = lerpResult10.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18934
1753.333;-140.6666;1152;771.6667;2044.772;447.8146;2.294793;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;2;-871.1998,87.4;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1;-866.1998,-48.59999;Inherit;False;Property;_Speed;Speed;2;0;Create;True;0;0;0;False;0;False;0.1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-829.1998,-142.6;Inherit;False;Property;_Scale;Scale;1;0;Create;True;0;0;0;False;0;False;18;18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-704.1998,32.39999;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-569.2,-108.6;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-509.8002,155.3;Inherit;True;Property;_TestWater;TestWater;0;0;Create;True;0;0;0;False;0;False;-1;e843d268f533e0c4fbd3a0d2cdedc6b2;e843d268f533e0c4fbd3a0d2cdedc6b2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;7;-223.8623,118.3467;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-259.373,-183.6258;Inherit;False;Property;_DeepWater;DeepWater;3;0;Create;True;0;0;0;False;0;False;0,0.3093882,1,1;0,0.2862744,0.7058823,0.682353;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-19.27283,95.70177;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;11;-33.17267,-218.7258;Inherit;False;Property;_ShadowWater;ShadowWater;4;0;Create;True;0;0;0;False;0;False;0,0.767278,1,1;0,0.517647,0.7843137,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;49;-958.6074,415.4969;Inherit;False;Property;_WaterDepth;WaterDepth;5;0;Create;True;0;0;0;False;0;False;1;0.45;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;10;239.9273,41.87422;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.5943396;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenDepthNode;47;-1142.6,206.3046;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;50;-718.1154,350.9784;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;48;-921.6007,303.8047;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;51;-591.0183,372.9308;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;46;-1193.301,336.3046;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;30;640.6611,115.1901;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1019.097,-28.33718;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;3;0;5;0
WireConnection;3;1;4;0
WireConnection;6;1;3;0
WireConnection;7;0;6;2
WireConnection;8;0;9;0
WireConnection;8;1;7;0
WireConnection;10;0;11;0
WireConnection;10;1;8;0
WireConnection;10;2;49;0
WireConnection;50;0;48;0
WireConnection;50;1;49;0
WireConnection;48;0;47;0
WireConnection;48;1;46;4
WireConnection;51;0;50;0
WireConnection;30;0;10;0
WireConnection;0;2;10;0
ASEEND*/
//CHKSM=E2383502429DB0C7E5A6E1B42B9E8990BD27AF45