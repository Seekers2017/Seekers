// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33090,y:32704,varname:node_2865,prsc:2|diff-671-OUT,spec-3415-OUT,gloss-7513-OUT,normal-1344-OUT,emission-8579-OUT;n:type:ShaderForge.SFN_Tex2d,id:9587,x:31860,y:32157,ptovrint:False,ptlb:track smooth,ptin:_tracksmooth,varname:node_9587,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:81d207f2564315b47bc774fdff7ccf89,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4832,x:31860,y:31967,ptovrint:False,ptlb:track mid,ptin:_trackmid,varname:node_4832,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:850fd1a077d3a5d4aae02d0c8de87f6a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9147,x:31860,y:32375,ptovrint:False,ptlb:track rough,ptin:_trackrough,varname:node_9147,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:eaa410735b0bfa04681fd9f40e09d09e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:15,x:32094,y:31967,varname:node_15,prsc:2|A-3712-OUT,B-9587-RGB,T-5144-G;n:type:ShaderForge.SFN_Lerp,id:671,x:32094,y:32157,varname:node_671,prsc:2|A-15-OUT,B-9147-RGB,T-5144-B;n:type:ShaderForge.SFN_Lerp,id:3712,x:32094,y:31762,varname:node_3712,prsc:2|A-5381-RGB,B-4832-RGB,T-5144-R;n:type:ShaderForge.SFN_Tex2d,id:5381,x:31860,y:31762,ptovrint:False,ptlb:Track,ptin:_Track,varname:node_5381,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5d0b227dc60c57b459b7de56eed2b38a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5144,x:31490,y:32784,ptovrint:False,ptlb:Track_Map,ptin:_Track_Map,varname:node_5144,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3eb29efab4eaafb4992c4a1b5393094d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4731,x:31860,y:32604,ptovrint:False,ptlb:Track Normal,ptin:_TrackNormal,varname:node_4731,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c3ecd6620e499a34893d82bb6f6cf910,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:4214,x:31860,y:33008,ptovrint:False,ptlb:Track Fine Normal,ptin:_TrackFineNormal,varname:node_4214,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fdc99ecbd47bbf340bb586b7d95d3954,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:3943,x:31860,y:32796,ptovrint:False,ptlb:Track MId Normal,ptin:_TrackMIdNormal,varname:node_3943,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7f6dff97be8e2454ab8a0a7bb66514cd,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:9508,x:31860,y:33193,ptovrint:False,ptlb:Track Rough Normal,ptin:_TrackRoughNormal,varname:node_9508,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:83cd6a82df3c43f4cbf8f72c9cea93d9,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Lerp,id:3374,x:32091,y:32604,varname:node_3374,prsc:2|A-4731-RGB,B-3943-RGB,T-5144-R;n:type:ShaderForge.SFN_Lerp,id:1403,x:32091,y:32796,varname:node_1403,prsc:2|A-3374-OUT,B-4214-RGB,T-5144-G;n:type:ShaderForge.SFN_Lerp,id:1344,x:32091,y:33008,varname:node_1344,prsc:2|A-1403-OUT,B-9508-RGB,T-5144-B;n:type:ShaderForge.SFN_Tex2d,id:4852,x:32659,y:33013,ptovrint:False,ptlb:Mid emmisive,ptin:_Midemmisive,varname:node_4852,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b40e7ca529209d041849a47d86686db1,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8579,x:32811,y:32885,varname:node_8579,prsc:2|A-5144-R,B-4852-RGB;n:type:ShaderForge.SFN_Tex2d,id:4140,x:32470,y:32512,ptovrint:False,ptlb:metal mid,ptin:_metalmid,varname:node_4140,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ea8f2200d2c037a46b30d623454a9ea4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ComponentMask,id:3415,x:32869,y:32489,varname:node_3415,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-2139-OUT;n:type:ShaderForge.SFN_Multiply,id:2139,x:32695,y:32489,varname:node_2139,prsc:2|A-5144-R,B-4140-RGB;n:type:ShaderForge.SFN_Slider,id:7513,x:32569,y:32708,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_7513,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:9587-4832-9147-5381-5144-4731-4214-3943-9508-4852-4140-7513;pass:END;sub:END;*/

Shader "Shader Forge/Track forge" {
    Properties {
        _tracksmooth ("track smooth", 2D) = "white" {}
        _trackmid ("track mid", 2D) = "white" {}
        _trackrough ("track rough", 2D) = "white" {}
        _Track ("Track", 2D) = "white" {}
        _Track_Map ("Track_Map", 2D) = "white" {}
        _TrackNormal ("Track Normal", 2D) = "bump" {}
        _TrackFineNormal ("Track Fine Normal", 2D) = "bump" {}
        _TrackMIdNormal ("Track MId Normal", 2D) = "bump" {}
        _TrackRoughNormal ("Track Rough Normal", 2D) = "bump" {}
        _Midemmisive ("Mid emmisive", 2D) = "white" {}
        _metalmid ("metal mid", 2D) = "white" {}
        _Gloss ("Gloss", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _tracksmooth; uniform float4 _tracksmooth_ST;
            uniform sampler2D _trackmid; uniform float4 _trackmid_ST;
            uniform sampler2D _trackrough; uniform float4 _trackrough_ST;
            uniform sampler2D _Track; uniform float4 _Track_ST;
            uniform sampler2D _Track_Map; uniform float4 _Track_Map_ST;
            uniform sampler2D _TrackNormal; uniform float4 _TrackNormal_ST;
            uniform sampler2D _TrackFineNormal; uniform float4 _TrackFineNormal_ST;
            uniform sampler2D _TrackMIdNormal; uniform float4 _TrackMIdNormal_ST;
            uniform sampler2D _TrackRoughNormal; uniform float4 _TrackRoughNormal_ST;
            uniform sampler2D _Midemmisive; uniform float4 _Midemmisive_ST;
            uniform sampler2D _metalmid; uniform float4 _metalmid_ST;
            uniform float _Gloss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _TrackNormal_var = UnpackNormal(tex2D(_TrackNormal,TRANSFORM_TEX(i.uv0, _TrackNormal)));
                float3 _TrackMIdNormal_var = UnpackNormal(tex2D(_TrackMIdNormal,TRANSFORM_TEX(i.uv0, _TrackMIdNormal)));
                float4 _Track_Map_var = tex2D(_Track_Map,TRANSFORM_TEX(i.uv0, _Track_Map));
                float3 _TrackFineNormal_var = UnpackNormal(tex2D(_TrackFineNormal,TRANSFORM_TEX(i.uv0, _TrackFineNormal)));
                float3 _TrackRoughNormal_var = UnpackNormal(tex2D(_TrackRoughNormal,TRANSFORM_TEX(i.uv0, _TrackRoughNormal)));
                float3 normalLocal = lerp(lerp(lerp(_TrackNormal_var.rgb,_TrackMIdNormal_var.rgb,_Track_Map_var.r),_TrackFineNormal_var.rgb,_Track_Map_var.g),_TrackRoughNormal_var.rgb,_Track_Map_var.b);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float perceptualRoughness = 1.0 - _Gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float4 _metalmid_var = tex2D(_metalmid,TRANSFORM_TEX(i.uv0, _metalmid));
                float3 specularColor = (_Track_Map_var.r*_metalmid_var.rgb).r;
                float specularMonochrome;
                float4 _Track_var = tex2D(_Track,TRANSFORM_TEX(i.uv0, _Track));
                float4 _trackmid_var = tex2D(_trackmid,TRANSFORM_TEX(i.uv0, _trackmid));
                float4 _tracksmooth_var = tex2D(_tracksmooth,TRANSFORM_TEX(i.uv0, _tracksmooth));
                float4 _trackrough_var = tex2D(_trackrough,TRANSFORM_TEX(i.uv0, _trackrough));
                float3 diffuseColor = lerp(lerp(lerp(_Track_var.rgb,_trackmid_var.rgb,_Track_Map_var.r),_tracksmooth_var.rgb,_Track_Map_var.g),_trackrough_var.rgb,_Track_Map_var.b); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _Midemmisive_var = tex2D(_Midemmisive,TRANSFORM_TEX(i.uv0, _Midemmisive));
                float3 emissive = (_Track_Map_var.r*_Midemmisive_var.rgb);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _tracksmooth; uniform float4 _tracksmooth_ST;
            uniform sampler2D _trackmid; uniform float4 _trackmid_ST;
            uniform sampler2D _trackrough; uniform float4 _trackrough_ST;
            uniform sampler2D _Track; uniform float4 _Track_ST;
            uniform sampler2D _Track_Map; uniform float4 _Track_Map_ST;
            uniform sampler2D _TrackNormal; uniform float4 _TrackNormal_ST;
            uniform sampler2D _TrackFineNormal; uniform float4 _TrackFineNormal_ST;
            uniform sampler2D _TrackMIdNormal; uniform float4 _TrackMIdNormal_ST;
            uniform sampler2D _TrackRoughNormal; uniform float4 _TrackRoughNormal_ST;
            uniform sampler2D _Midemmisive; uniform float4 _Midemmisive_ST;
            uniform sampler2D _metalmid; uniform float4 _metalmid_ST;
            uniform float _Gloss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _TrackNormal_var = UnpackNormal(tex2D(_TrackNormal,TRANSFORM_TEX(i.uv0, _TrackNormal)));
                float3 _TrackMIdNormal_var = UnpackNormal(tex2D(_TrackMIdNormal,TRANSFORM_TEX(i.uv0, _TrackMIdNormal)));
                float4 _Track_Map_var = tex2D(_Track_Map,TRANSFORM_TEX(i.uv0, _Track_Map));
                float3 _TrackFineNormal_var = UnpackNormal(tex2D(_TrackFineNormal,TRANSFORM_TEX(i.uv0, _TrackFineNormal)));
                float3 _TrackRoughNormal_var = UnpackNormal(tex2D(_TrackRoughNormal,TRANSFORM_TEX(i.uv0, _TrackRoughNormal)));
                float3 normalLocal = lerp(lerp(lerp(_TrackNormal_var.rgb,_TrackMIdNormal_var.rgb,_Track_Map_var.r),_TrackFineNormal_var.rgb,_Track_Map_var.g),_TrackRoughNormal_var.rgb,_Track_Map_var.b);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float perceptualRoughness = 1.0 - _Gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float4 _metalmid_var = tex2D(_metalmid,TRANSFORM_TEX(i.uv0, _metalmid));
                float3 specularColor = (_Track_Map_var.r*_metalmid_var.rgb).r;
                float specularMonochrome;
                float4 _Track_var = tex2D(_Track,TRANSFORM_TEX(i.uv0, _Track));
                float4 _trackmid_var = tex2D(_trackmid,TRANSFORM_TEX(i.uv0, _trackmid));
                float4 _tracksmooth_var = tex2D(_tracksmooth,TRANSFORM_TEX(i.uv0, _tracksmooth));
                float4 _trackrough_var = tex2D(_trackrough,TRANSFORM_TEX(i.uv0, _trackrough));
                float3 diffuseColor = lerp(lerp(lerp(_Track_var.rgb,_trackmid_var.rgb,_Track_Map_var.r),_tracksmooth_var.rgb,_Track_Map_var.g),_trackrough_var.rgb,_Track_Map_var.b); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _tracksmooth; uniform float4 _tracksmooth_ST;
            uniform sampler2D _trackmid; uniform float4 _trackmid_ST;
            uniform sampler2D _trackrough; uniform float4 _trackrough_ST;
            uniform sampler2D _Track; uniform float4 _Track_ST;
            uniform sampler2D _Track_Map; uniform float4 _Track_Map_ST;
            uniform sampler2D _Midemmisive; uniform float4 _Midemmisive_ST;
            uniform sampler2D _metalmid; uniform float4 _metalmid_ST;
            uniform float _Gloss;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _Track_Map_var = tex2D(_Track_Map,TRANSFORM_TEX(i.uv0, _Track_Map));
                float4 _Midemmisive_var = tex2D(_Midemmisive,TRANSFORM_TEX(i.uv0, _Midemmisive));
                o.Emission = (_Track_Map_var.r*_Midemmisive_var.rgb);
                
                float4 _Track_var = tex2D(_Track,TRANSFORM_TEX(i.uv0, _Track));
                float4 _trackmid_var = tex2D(_trackmid,TRANSFORM_TEX(i.uv0, _trackmid));
                float4 _tracksmooth_var = tex2D(_tracksmooth,TRANSFORM_TEX(i.uv0, _tracksmooth));
                float4 _trackrough_var = tex2D(_trackrough,TRANSFORM_TEX(i.uv0, _trackrough));
                float3 diffColor = lerp(lerp(lerp(_Track_var.rgb,_trackmid_var.rgb,_Track_Map_var.r),_tracksmooth_var.rgb,_Track_Map_var.g),_trackrough_var.rgb,_Track_Map_var.b);
                float specularMonochrome;
                float3 specColor;
                float4 _metalmid_var = tex2D(_metalmid,TRANSFORM_TEX(i.uv0, _metalmid));
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, (_Track_Map_var.r*_metalmid_var.rgb).r, specColor, specularMonochrome );
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
