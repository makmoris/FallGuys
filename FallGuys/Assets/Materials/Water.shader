// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Water"
{
	Properties
	{
		_TestWater("TestWater", 2D) = "white" {}
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
		uniform float _WaterDepth;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_cast_0 = (18.0).xx;
			float2 temp_cast_1 = (( 1.0 * _Time.y )).xx;
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
816;251;940;872;1375.675;730.6622;1.70585;True;False
Node;AmplifyShaderEditor.RangedFloatNode;1;-866.1998,-48.59999;Inherit;False;Constant;_Speed;Speed;0;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;2;-871.1998,87.4;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-829.1998,-141.6;Inherit;False;Constant;_Scale;Scale;0;0;Create;True;0;0;0;False;0;False;18;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-704.1998,32.39999;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-569.2,-108.6;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-509.8002,155.3;Inherit;True;Property;_TestWater;TestWater;0;0;Create;True;0;0;0;False;0;False;-1;e843d268f533e0c4fbd3a0d2cdedc6b2;e843d268f533e0c4fbd3a0d2cdedc6b2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;7;-223.8623,118.3467;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-259.373,-183.6258;Inherit;False;Property;_DeepWater;DeepWater;3;0;Create;True;0;0;0;False;0;False;0,0.3093882,1,1;0,0.2862744,0.7058823,0.682353;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;11;-33.17267,-218.7258;Inherit;False;Property;_ShadowWater;ShadowWater;4;0;Create;True;0;0;0;False;0;False;0,0.767278,1,1;0,0.517647,0.7843137,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-19.27283,95.70177;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-958.6074,415.4969;Inherit;False;Property;_WaterDepth;WaterDepth;6;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;15;-597.6824,674.3893;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;38;-264.0606,-620.3436;Inherit;True;Gradient;False;True;2;0;FLOAT2;0,0;False;1;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-577.9481,817.1144;Inherit;False;Property;_FoamOutOff;FoamOutOff;7;0;Create;True;0;0;0;False;0;False;1;0.82;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;27;-433.5788,1015.104;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;100;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;17;-470.5853,696.3417;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-806.0355,-588.1642;Inherit;False;Constant;_Float4;Float 4;0;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenDepthNode;14;-1022.167,529.7155;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;19;-963.278,1242.198;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceCameraPos;59;-1332.504,541.7312;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;13;-801.1678,627.2156;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-874.8938,861.2844;Inherit;False;Property;_FoamAmount;FoamAmount;5;0;Create;True;0;0;0;False;0;False;1;-1.18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;12;-1151.868,713.7155;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;37;-509.0365,-648.1642;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-958.2779,1106.198;Inherit;False;Constant;_Float3;Float 3;0;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;30;640.6611,115.1901;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;23;-661.2789,1046.198;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;140.1745,1026.102;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-644.0362,-507.1642;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-769.0361,-681.1642;Inherit;False;Constant;_Float5;Float 5;0;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;48;-921.6007,303.8047;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;50;-718.1154,350.9784;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;51;-591.0183,372.9308;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;33;-811.0356,-452.1642;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;402.5171,-443.3796;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;29;-150.7618,1106.792;Inherit;False;Property;_FoamColor;FoamColor;2;0;Create;True;0;0;0;False;0;False;1,1,1,0.7450981;1,1,0.6392157,0.3490196;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;21;-921.2785,1013.198;Inherit;False;Constant;_Float2;Float 2;0;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-309.1181,741.103;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenDepthNode;47;-1142.6,206.3046;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;146.5171,-285.3796;Inherit;False;Property;_RefractionStrength;RefractionStrength;1;0;Create;True;0;0;0;False;0;False;0.05;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;46;-1193.301,336.3046;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;10;239.9273,41.87422;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.5943396;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-796.2786,1187.198;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;39;20.64628,-545.2281;Inherit;True;Normal From Height;-1;;1;1942fe2c5f1a1f94881a33d532e4afeb;0;2;20;FLOAT;0;False;110;FLOAT;0.01;False;2;FLOAT3;40;FLOAT3;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;42;401.5171,-695.3796;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;43;652.5171,-522.3796;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StepOpNode;25;-109.5499,862.9951;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1019.097,-28.33718;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;0;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;3;0;5;0
WireConnection;3;1;4;0
WireConnection;6;1;3;0
WireConnection;7;0;6;2
WireConnection;8;0;9;0
WireConnection;8;1;7;0
WireConnection;15;0;13;0
WireConnection;15;1;16;0
WireConnection;38;0;37;0
WireConnection;27;0;23;0
WireConnection;17;0;15;0
WireConnection;13;0;14;0
WireConnection;13;1;12;4
WireConnection;37;0;35;0
WireConnection;37;1;36;0
WireConnection;30;0;10;0
WireConnection;23;0;21;0
WireConnection;23;1;22;0
WireConnection;28;0;25;0
WireConnection;28;1;29;4
WireConnection;36;0;34;0
WireConnection;36;1;33;0
WireConnection;48;0;47;0
WireConnection;48;1;46;4
WireConnection;50;0;48;0
WireConnection;50;1;49;0
WireConnection;51;0;50;0
WireConnection;40;0;39;0
WireConnection;40;1;41;0
WireConnection;18;0;17;0
WireConnection;18;1;24;0
WireConnection;10;0;11;0
WireConnection;10;1;8;0
WireConnection;10;2;49;0
WireConnection;22;0;20;0
WireConnection;22;1;19;0
WireConnection;39;20;38;0
WireConnection;43;0;42;0
WireConnection;43;1;40;0
WireConnection;25;0;18;0
WireConnection;25;1;27;0
WireConnection;0;2;10;0
ASEEND*/
//CHKSM=BEBAEC23812F484B8C8DA419213981109ADF8D6D