Shader "Unlit/SquashAndStretch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _StretchAmount ("Stretch Amount", Range(0, 1)) = 0.1
        _SquashAmount ("Squash Amount", Range(0, 1)) = 0.1
        _TimeSpeed ("Time Speed", Range(0.1, 2)) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _StretchAmount;
            float _SquashAmount;
            float _TimeSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Apply squash and stretch effect to UV coordinates
                float time = _Time.y * _TimeSpeed;
                
                // Create a sine wave effect to simulate stretch and squash
                float stretch = 1.0 + _StretchAmount * sin(time);
                float squash = 1.0 - _SquashAmount * sin(time);

                // Apply the UV distortion
                float2 uvDistorted = float2(v.uv.x * stretch, v.uv.y * squash);
                
                o.uv = uvDistorted;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Discard transparent pixels (preserve transparency)
                if (col.a < 0.1)
                    discard;

                // Apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }
            ENDCG
        }
    }
}
