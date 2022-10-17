// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/ToonGradient"
{
	Properties
	{
		[NoScaleOffset]_BaseColorRGBOutlineWidthA("Base Color (RGB) Outline Width (A)", 2D) = "gray" {}
		_BaseTint("Base Tint", Color) = (1,1,1,0)
		_BaseCellSharpness("Base Cell Sharpness", Range( 0.01 , 1)) = 0.01
		_BaseCellOffset("Base Cell Offset", Range( -1 , 1)) = 0
		_ShadowContribution("Shadow Contribution", Range( 0 , 1)) = 0.5
		[HDR]_HighlightTint("Highlight Tint", Color) = (1,1,1,1)
		_HighlightCellOffset("Highlight Cell Offset", Range( -1 , -0.5)) = -0.95
		_HighlightCellSharpness("Highlight Cell Sharpness", Range( 0.001 , 1)) = 0.01
		_IndirectSpecularContribution("Indirect Specular Contribution", Range( 0 , 1)) = 1
		[Toggle(_STATICHIGHLIGHTS_ON)] _StaticHighLights("Static HighLights", Float) = 0
		[Toggle(_RAMPTEXTURESWITH_ON)] _RampTextureSwith("Ramp Texture Swith", Float) = 0
		_RampTexture("Ramp Texture", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _STATICHIGHLIGHTS_ON
		#pragma shader_feature_local _RAMPTEXTURESWITH_ON
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldPos;
			float2 uv_texcoord;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform float4 _HighlightTint;
		uniform float _IndirectSpecularContribution;
		uniform float _HighlightCellOffset;
		uniform float _HighlightCellSharpness;
		uniform float _BaseCellOffset;
		uniform float _BaseCellSharpness;
		uniform float _ShadowContribution;
		uniform sampler2D _RampTexture;
		uniform sampler2D _BaseColorRGBOutlineWidthA;
		uniform float4 _BaseTint;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			#ifdef UNITY_PASS_FORWARDBASE
			float ase_lightAtten = data.atten;
			if( _LightColor0.a == 0)
			ase_lightAtten = 0;
			#else
			float3 ase_lightAttenRGB = gi.light.color / ( ( _LightColor0.rgb ) + 0.000001 );
			float ase_lightAtten = max( max( ase_lightAttenRGB.r, ase_lightAttenRGB.g ), ase_lightAttenRGB.b );
			#endif
			#if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
			half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
			float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
			float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
			ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
			#endif
			float3 temp_cast_0 = (1.0).xxx;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 normalizeResult5 = normalize( ase_worldNormal );
			float3 NewNormals8 = normalizeResult5;
			float3 indirectNormal80 = NewNormals8;
			float temp_output_45_0 = (_HighlightTint).a;
			Unity_GlossyEnvironmentData g80 = UnityGlossyEnvironmentSetup( temp_output_45_0, data.worldViewDir, indirectNormal80, float3(0,0,0));
			float3 indirectSpecular80 = UnityGI_IndirectSpecular( data, 1.0, indirectNormal80, g80 );
			float3 lerpResult98 = lerp( temp_cast_0 , indirectSpecular80 , _IndirectSpecularContribution);
			float3 HighlightColor84 = (_HighlightTint).rgb;
			#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float3 LightColorFalloff85 = ( ase_lightColor.rgb * ase_lightAtten );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 normalizeResult163 = normalize( ( ase_worldViewDir + ase_worldlightDir ) );
			float dotResult43 = dot( normalizeResult163 , NewNormals8 );
			float dotResult11 = dot( NewNormals8 , ase_worldlightDir );
			float NdotL14 = dotResult11;
			#ifdef _STATICHIGHLIGHTS_ON
				float staticSwitch52 = NdotL14;
			#else
				float staticSwitch52 = dotResult43;
			#endif
			float temp_output_41_0 = ( saturate( ( ( NdotL14 + _BaseCellOffset ) / _BaseCellSharpness ) ) * ase_lightAtten );
			float lerpResult49 = lerp( ( 1.0 - ( ( 1.0 - ase_lightAtten ) * _WorldSpaceLightPos0.w ) ) , temp_output_41_0 , _ShadowContribution);
			float temp_output_1_0_g4 = -1.0;
			float2 temp_cast_3 = (( ( NdotL14 - temp_output_1_0_g4 ) / ( 1.0 - temp_output_1_0_g4 ) )).xx;
			#ifdef _RAMPTEXTURESWITH_ON
				float4 staticSwitch144 = saturate( ( 0.3 + ( tex2D( _RampTexture, temp_cast_3 ) * step( 0.6 , ase_lightAtten ) ) ) );
			#else
				float4 staticSwitch144 = float4( ( ase_lightColor.rgb * lerpResult49 ) , 0.0 );
			#endif
			float2 uv_BaseColorRGBOutlineWidthA48 = i.uv_texcoord;
			float4 BaseColor94 = ( ( ( ase_lightColor.a * temp_output_41_0 ) + staticSwitch144 ) * float4( (( tex2D( _BaseColorRGBOutlineWidthA, uv_BaseColorRGBOutlineWidthA48 ) * _BaseTint )).rgb , 0.0 ) );
			float4 temp_output_105_0 = ( float4( ( lerpResult98 * HighlightColor84 * LightColorFalloff85 * pow( temp_output_45_0 , 1.5 ) * saturate( ( ( staticSwitch52 + _HighlightCellOffset ) / ( ( 1.0 - temp_output_45_0 ) * _HighlightCellSharpness ) ) ) ) , 0.0 ) + BaseColor94 );
			c.rgb = temp_output_105_0.rgb;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			o.Normal = float3(0,0,1);
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18934
1660;-945.3333;1346;664.3334;3762.326;576.4108;1.648835;True;False
Node;AmplifyShaderEditor.CommentaryNode;1;-5967.564,-681.9393;Inherit;False;726.8441;264.4353;Comment;3;8;5;4;Normals;0.5220588,0.6044625,1,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;4;-5910.511,-626.6992;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NormalizeNode;5;-5668.876,-627.6428;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;6;-5954.239,-194.1975;Inherit;False;835.6508;341.2334;Comment;4;14;11;9;7;N dot L;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;8;-5483.72,-626.1917;Float;False;NewNormals;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;9;-5904.239,-31.96414;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;7;-5912.148,-144.1975;Inherit;False;8;NewNormals;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;11;-5579.948,-107.3986;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;14;-5361.588,-104.083;Float;False;NdotL;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;13;-3881.845,-69.3762;Inherit;False;14;NdotL;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-3903.832,80.58719;Float;False;Property;_BaseCellOffset;Base Cell Offset;3;0;Create;True;0;0;0;False;0;False;0;0.03;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;123;-1188.028,-1456.82;Inherit;False;607.4526;300.0015;RampTexture;3;108;109;107;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-3620.383,-26.23813;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-3601.756,70.27347;Float;False;Property;_BaseCellSharpness;Base Cell Sharpness;2;0;Create;True;0;0;0;False;0;False;0.01;0.575;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;107;-1138.028,-1397.815;Inherit;False;14;NdotL;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LightAttenuation;16;-3646.815,230.1935;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;109;-1050.405,-1304.854;Inherit;False;Inverse Lerp;-1;;4;09cbe79402f023141a4dc1fddd4c9511;0;3;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightPos;22;-3661.847,323.0302;Inherit;False;0;3;FLOAT4;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.LightAttenuation;129;-994.5282,-1069.195;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;164;-5045.309,-1316.411;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;161;-5093.309,-1156.411;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleDivideOpNode;18;-3321.318,-23.3945;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;19;-3333.788,255.3292;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-3138.059,290.9648;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;130;-752.3229,-1111.345;Inherit;True;2;0;FLOAT;0.6;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;162;-4789.309,-1252.411;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;124;-459.0544,-1334.899;Inherit;False;499.5007;232.6276;Combine Ramp with Shadows;4;128;127;125;126;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;20;-4629.301,-1790.164;Inherit;False;2234.221;738.9581;Comment;15;97;89;84;76;74;70;68;59;55;54;52;47;43;40;26;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;108;-865.0603,-1396.687;Inherit;True;Property;_RampTexture;Ramp Texture;12;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;29;-3190.174,-26.25745;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-3148.786,430.6003;Float;False;Property;_ShadowContribution;Shadow Contribution;5;0;Create;True;0;0;0;False;0;False;0.5;0.638;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;125;-423.0287,-1299.221;Inherit;False;Constant;_Float0;Float 0;20;0;Create;True;0;0;0;False;0;False;0.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;35;-3385.479,-2119.433;Inherit;False;287;165;Comment;1;45;Spec/Smooth;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;26;-3970.93,-1747.678;Float;False;Property;_HighlightTint;Highlight Tint;6;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,0.8431373;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;40;-4515.738,-1166.206;Inherit;False;8;NewNormals;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-3034.19,6.522051;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-411.2334,-1213.659;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;34;-2945.369,214.5574;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;163;-4629.309,-1252.411;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;49;-2643.49,237.6486;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;53;-5913.762,382.6659;Inherit;False;717.6841;295.7439;Comment;4;85;79;69;65;Light Falloff;0.9947262,1,0.6176471,1;0;0
Node;AmplifyShaderEditor.LightColorNode;46;-2714.127,-112.5071;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.DotProductOpNode;43;-4234.082,-1239.867;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;45;-3335.479,-2069.433;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;127;-278.3022,-1287.859;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;47;-4274.373,-1406.079;Inherit;False;14;NdotL;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;51;-2266.79,626.6373;Float;False;Property;_BaseTint;Base Tint;1;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-2330.522,76.61559;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;48;-2376.849,382.553;Inherit;True;Property;_BaseColorRGBOutlineWidthA;Base Color (RGB) Outline Width (A);0;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;6a4f75cb216166648ae306f973808c4a;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;55;-3872.09,-1221.048;Float;False;Property;_HighlightCellOffset;Highlight Cell Offset;7;0;Create;True;0;0;0;False;0;False;-0.95;-0.919;-1;-0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;61;-3590.283,-2661.491;Inherit;False;1008.755;365.3326;Comment;5;98;81;80;73;67;Indirect Specular;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;128;-88.26094,-1268.048;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LightColorNode;69;-5818.058,432.666;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.LightAttenuation;65;-5863.762,568.4098;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;59;-3478.828,-1208.831;Float;False;Property;_HighlightCellSharpness;Highlight Cell Sharpness;8;0;Create;True;0;0;0;False;0;False;0.01;0.001;0.001;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;54;-3256.312,-1446.455;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;52;-4006.749,-1345.181;Float;False;Property;_StaticHighLights;Static HighLights;10;0;Create;True;0;0;0;False;0;False;0;0;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;144;-2063.969,-190.5957;Float;False;Property;_RampTextureSwith;Ramp Texture Swith;11;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-2253.411,-117.1906;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-3161.398,-1215.496;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-2001.093,409.5433;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-5629.763,498.8268;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;68;-3517.312,-1348.961;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;67;-3540.283,-2540.851;Inherit;False;8;NewNormals;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;85;-5455.079,495.2938;Float;False;LightColorFalloff;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;76;-3378.963,-1614.204;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;72;-1861.141,394.2757;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.IndirectSpecularLight;80;-3116.217,-2524.267;Inherit;False;World;3;0;FLOAT3;0,0,0;False;1;FLOAT;0.5;False;2;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;74;-3103.235,-1353.189;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;66;-1902.95,-41.13158;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;81;-3089.588,-2611.491;Float;False;Constant;_Float5;Float 5;20;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-3183.39,-2413.48;Float;False;Property;_IndirectSpecularContribution;Indirect Specular Contribution;9;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;99;-3000.492,-1924.516;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;98;-2765.525,-2558.646;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;97;-3073.032,-1513.313;Inherit;False;85;LightColorFalloff;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;89;-2953.19,-1352.942;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;84;-3066.394,-1616.695;Float;False;HighlightColor;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-1599.865,164.5152;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;94;-1358.719,163.6987;Float;False;BaseColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-2310.071,-1548.962;Inherit;True;5;5;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;21;-3922.475,-748.0497;Inherit;False;828.4254;361.0605;Comment;5;44;39;37;31;30;Indirect Diffuse;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;159;-2242.488,1019.607;Inherit;False;1508.253;548.4563;HightRemap;9;157;145;150;146;158;151;147;153;154;HightRemap;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;145;-2141.817,1069.607;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;147;-1566.375,1120.547;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;158;-2166.621,1447.893;Inherit;False;Property;_HightRemapSmus;HightRemapSmus;15;0;Create;True;0;0;0;False;0;False;1;0.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;104;248.13,-312.6226;Inherit;False;94;BaseColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;151;-1597.978,1435.063;Inherit;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;156;-998.1084,-194.3796;Float;False;Property;_FogColor;FogColor;13;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.509434,0.001922354,0.001922354,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;150;-1788.101,1364.29;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-3492.862,-698.0498;Float;False;Constant;_Float4;Float 4;20;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;155;-618.6038,135.6225;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-3618.479,-501.9894;Float;False;Property;_IndirectDiffuseContribution;Indirect Diffuse Contribution;4;0;Create;True;0;0;0;False;0;False;1;0.677;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.IndirectDiffuseLighting;37;-3588.415,-605.0746;Inherit;False;World;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;44;-3338.299,-629.2657;Inherit;True;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;146;-1896.639,1086.873;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.GetLocalVarNode;30;-3885.474,-626.6696;Inherit;True;8;NewNormals;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;154;-905.5677,1182.214;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;153;-1234.694,1181.48;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;157;-2178.944,1326.222;Inherit;False;Property;_HightRemapPos;HightRemapPos;14;0;Create;True;0;0;0;False;0;False;0;-1.78;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;165;54.08359,-201.1363;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;105;-1107.136,35.18389;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;627.3226,-275.1826;Float;False;True;-1;2;ASEMaterialInspector;0;0;CustomLighting;Custom/ToonGradient;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;4;0
WireConnection;8;0;5;0
WireConnection;11;0;7;0
WireConnection;11;1;9;0
WireConnection;14;0;11;0
WireConnection;17;0;13;0
WireConnection;17;1;12;0
WireConnection;109;3;107;0
WireConnection;18;0;17;0
WireConnection;18;1;15;0
WireConnection;19;0;16;0
WireConnection;28;0;19;0
WireConnection;28;1;22;2
WireConnection;130;1;129;0
WireConnection;162;0;164;0
WireConnection;162;1;161;0
WireConnection;108;1;109;0
WireConnection;29;0;18;0
WireConnection;41;0;29;0
WireConnection;41;1;16;0
WireConnection;126;0;108;0
WireConnection;126;1;130;0
WireConnection;34;0;28;0
WireConnection;163;0;162;0
WireConnection;49;0;34;0
WireConnection;49;1;41;0
WireConnection;49;2;42;0
WireConnection;43;0;163;0
WireConnection;43;1;40;0
WireConnection;45;0;26;0
WireConnection;127;0;125;0
WireConnection;127;1;126;0
WireConnection;56;0;46;1
WireConnection;56;1;49;0
WireConnection;128;0;127;0
WireConnection;54;0;45;0
WireConnection;52;1;43;0
WireConnection;52;0;47;0
WireConnection;144;1;56;0
WireConnection;144;0;128;0
WireConnection;60;0;46;2
WireConnection;60;1;41;0
WireConnection;70;0;54;0
WireConnection;70;1;59;0
WireConnection;57;0;48;0
WireConnection;57;1;51;0
WireConnection;79;0;69;1
WireConnection;79;1;65;0
WireConnection;68;0;52;0
WireConnection;68;1;55;0
WireConnection;85;0;79;0
WireConnection;76;0;26;0
WireConnection;72;0;57;0
WireConnection;80;0;67;0
WireConnection;80;1;45;0
WireConnection;74;0;68;0
WireConnection;74;1;70;0
WireConnection;66;0;60;0
WireConnection;66;1;144;0
WireConnection;99;0;45;0
WireConnection;98;0;81;0
WireConnection;98;1;80;0
WireConnection;98;2;73;0
WireConnection;89;0;74;0
WireConnection;84;0;76;0
WireConnection;77;0;66;0
WireConnection;77;1;72;0
WireConnection;94;0;77;0
WireConnection;102;0;98;0
WireConnection;102;1;84;0
WireConnection;102;2;97;0
WireConnection;102;3;99;0
WireConnection;102;4;89;0
WireConnection;147;0;146;1
WireConnection;147;1;150;0
WireConnection;151;1;158;0
WireConnection;150;0;157;0
WireConnection;155;0;156;0
WireConnection;155;1;105;0
WireConnection;155;2;154;0
WireConnection;37;0;30;0
WireConnection;44;0;31;0
WireConnection;44;1;37;0
WireConnection;44;2;39;0
WireConnection;146;0;145;0
WireConnection;154;0;153;0
WireConnection;153;0;147;0
WireConnection;153;4;151;0
WireConnection;165;1;105;0
WireConnection;165;2;154;0
WireConnection;105;0;102;0
WireConnection;105;1;94;0
WireConnection;0;13;105;0
ASEEND*/
//CHKSM=3E6117C51198C33E6AD1804C28CAC730096E165D