Shader "Distort" { // defines the name of the shader
   Properties {
   		_VideoFrame ("Texture", 2D) = "white" {}
		_TwistAmount ("Twist Amount", Range(-180,180)) = 0
		_BendAngle ("Bend Angle", Range(-180,180)) = 0
		_BendAmount ("Bend Amount", Range(-0.3,0.3)) = 0
		_Exposure ("Exposure", Range(0,10)) = 1
		_EyeLevel ("Eye Level", Range(0,80)) = 50
		_Height ("Height", Range(0,4)) = 1

   } 
   SubShader { // Unity chooses the subshader that fits the GPU best
      Pass { // some shaders require multiple passes
         GLSLPROGRAM // here begins the part in Unity's GLSL
 
         #ifdef VERTEX // here begins the vertex shader
 
		uniform float _TwistAmount;
		uniform float _TwistAngle;
		uniform float _BendAngle;
		uniform float _BendAmount;
		uniform float _EyeLevel;
		uniform float _Height;
 
		varying vec3 normal, lightDir[3], eyeVec;

		varying vec4 Color;

		vec4 DoTwist( vec4 pos, float t )
		{
			float st = (sin(t * pos.y));
			float ct = (cos(t * pos.y));
			vec4 new_pos;
			
			new_pos.z = pos.z*ct + pos.x*st;
			new_pos.x = pos.x*ct - pos.z*st;
			
			new_pos.y = pos.y;
			new_pos.w = pos.w;

			return( new_pos );
		}

		vec4 DoBend( vec4 pos, float dir, float amt)
		{
			vec4 new_pos;
			
			float old_pos_r = sqrt(pos.x * pos.x + pos.z * pos.z);
			float old_pos_theta = atan(pos.z, pos.x);
			
			float new_x_dif = cos(dir) * pos.y* pos.y * amt;
			float new_z_dif = sin(dir) * pos.y* pos.y * amt;	
			
			new_pos.x = pos.x + new_x_dif;
			new_pos.y = pos.y * _Height;	
			new_pos.z = pos.z + new_z_dif;
			new_pos.w = pos.w;

			return( new_pos );
		}

		void main(void)
		{
//			float angle_deg = _TwistAmount * _TwistAngle;
//			float angle_rad = angle_deg * 3.14159 / 180.0;
//			float ang = (0.1 * 0.5 + gl_Vertex.y)/0.1 * angle_rad;
			float ang = (_TwistAmount * 3.14159) / (180.0 * _EyeLevel);			
			float _BendAngle_rad = _BendAngle * 3.14159 / 180.0;
			
			
			//twist it
			vec4 twistedPosition = DoTwist(gl_Vertex, ang);
			vec4 twistedNormal = DoTwist(vec4(gl_Normal, ang), 90.); 
			
			//bend it
			vec4 bentPosition = DoBend(twistedPosition, _BendAngle_rad, _BendAmount);
			vec4 bentNormal = DoBend(vec4(gl_Normal, ang), _BendAngle_rad, _BendAmount); 
			
			//gl_Position = gl_ModelViewProjectionMatrix * twistedPosition;
			gl_Position = gl_ModelViewProjectionMatrix * bentPosition;
			
			vec3 vVertex = vec3(gl_ModelViewMatrix * bentPosition);
			
			lightDir[0] = vec3(gl_LightSource[0].position.xyz - vVertex);
			lightDir[1] = vec3(gl_LightSource[1].position.xyz - vVertex);
			lightDir[2] = vec3(gl_LightSource[2].position.xyz - vVertex);
			eyeVec = -vVertex;

			normal = gl_NormalMatrix * bentNormal.xyz;
			
			//gl_TexCoord[0] = gl_MultiTexCoord0;
			

			
			//Transform vertex by modelview and projection matrices
			//gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex ;
			
			//Forward current color and texture coordinates after applying texture matrix
			//gl_FrontColor = gl_Color;
			gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;

			
		}
 
         #endif // here ends the definition of the vertex shader
 
 
         #ifdef FRAGMENT // here begins the fragment shader
 
 		uniform sampler2D _VideoFrame;
		uniform float _Exposure;
 
         void main() // all fragment shaders define a main() function
         {
            gl_FragColor = texture2D(_VideoFrame, gl_TexCoord[0].xy) * _Exposure; 
         }
   
         #endif // here ends the definition of the fragment shader
 
         ENDGLSL // here ends the part in GLSL 
      }
   }
}