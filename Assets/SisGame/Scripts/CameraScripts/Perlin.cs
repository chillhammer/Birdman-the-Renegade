// https://code.google.com/p/pixelplacement/source/browse/trunk/unity/com/pixelplacement/scripts/Perlin.cs?r=142

using System.Collections;
using System;
using UnityEngine;

public class SmoothRandom
{
	public static Vector3 GetVector3 (float speed)
	{
		float time = Time.time * 0.01F * speed;
		//return new Vector3(Get().HybridMultifractal(time, 15.73F, 0.58F), Get().HybridMultifractal(time, 63.94F, 0.58F), Get().HybridMultifractal(time, 0.2F, 0.58F));
		return new Vector3(Get().HybridMultifractal(time, 15.73F, 0F), Get().HybridMultifractal(time, 63.94F, 0F), Get().HybridMultifractal(time, 0.2F, 0F));
	}
	
	public static float Get (float speed)
	{
		float time = Time.time * 0.01F * speed;
		return Get().HybridMultifractal(time * 0.01F, 15.7F, 0.65F);
	}

	private static FractalNoise Get () { 
		if (s_Noise == null)
			s_Noise = new FractalNoise (1.27F, 2.04F, 8.36F);
		return s_Noise;		
	 }

	private static FractalNoise s_Noise;
}


public class Perlin
{
	// Original C code derived from 
	// http://astronomy.swin.edu.au/~pbourke/texture/perlin/perlin.c
	// http://astronomy.swin.edu.au/~pbourke/texture/perlin/perlin.h
	const int B = 0x100;			/*	B = 256	 */
	const int BM = 0xff;			/*	BM = 255 */
	const int N = 0x1000;			/*	N = 4096 */

	int[] p = new int[B + B + 2];				//Integer array "p" of size 256 + 256 + 2 = 514
	float[,] g3 = new float [B + B + 2 , 3];	//2-Dimensional array of size 514 by 3
	float[,] g2 = new float[B + B + 2,2];		//2-Dimensional array of size 514 by 2
	float[] g1 = new float[B + B + 2];			//Array of size 514

	float s_curve(float t)
	{
		return t * t * (3.0F - 2.0F * t);
	}
	
	float lerp (float t, float a, float b)
	{ 
		return a + t * (b - a);
	}

	void setup (float value, out int b0, out int b1, out float r0, out float r1)
	{ 
        float t = value + N;			//float t = value + 4096
        b0 = ((int)t) & BM;				//b0 is set to t bitwise AND 255
        b1 = (b0+1) & BM;				//b1 is set to b0 bitwise AND 1
        r0 = t - (int)t;				//r0 is set to t - (int)t, which should be just the decimal remainder
        r1 = r0 - 1.0F;					//r1 is set to r0 - 1.0F
	}
	
	float at2(float rx, float ry, float x, float y) { return rx * x + ry * y; }
	float at3(float rx, float ry, float rz, float x, float y, float z) { return rx * x + ry * y + rz * z; }

	public float Noise(float arg)
	{
		int bx0, bx1;
		float rx0, rx1, sx, u, v;
		setup(arg, out bx0, out bx1, out rx0, out rx1);
		
		sx = s_curve(rx0);				//Set sx to (rx0 * rx0 * (3.0F - 2.0f * rx0)
		u = rx0 * g1[ p[ bx0 ] ];		//Set u to (rx0 * the value at position p[bx0] in g1
		v = rx1 * g1[ p[ bx1 ] ];		//Set v to (rx1 * the value at position p[bx1] in g1
		
		return(lerp(sx, u, v));			//Return u + sx * (v - u)
	}

	public float Noise(float x, float y)
	{
		int bx0, bx1, by0, by1, b00, b10, b01, b11;
		float rx0, rx1, ry0, ry1, sx, sy, a, b, u, v;
		int i, j;
		
		setup(x, out bx0, out bx1, out rx0, out rx1);
		setup(y, out by0, out by1, out ry0, out ry1);
		
		i = p[ bx0 ];					//Set i to the value at p[ bx0 ]
		j = p[ bx1 ];					//Set j to the value at p[ bx1 ]
		
		b00 = p[ i + by0 ];				//Set b00 to the vale at p[ i + by0 ]
		b10 = p[ j + by0 ];
		b01 = p[ i + by1 ];
		b11 = p[ j + by1 ];
		
		sx = s_curve(rx0);
		sy = s_curve(ry0);
		
		u = at2(rx0,ry0, g2[ b00, 0 ], g2[ b00, 1 ]);
		v = at2(rx1,ry0, g2[ b10, 0 ], g2[ b10, 1 ]);
		a = lerp(sx, u, v);
		
		u = at2(rx0,ry1, g2[ b01, 0 ], g2[ b01, 1 ]);
		v = at2(rx1,ry1, g2[ b11, 0 ], g2[ b11, 1 ]);
		b = lerp(sx, u, v);
		
		return lerp(sy, a, b);
	}
	
	public float Noise(float x, float y, float z)
	{
		int bx0, bx1, by0, by1, bz0, bz1, b00, b10, b01, b11;
		float rx0, rx1, ry0, ry1, rz0, rz1, sy, sz, a, b, c, d, t, u, v;
		int i, j;
		
		setup(x, out bx0, out bx1, out rx0, out rx1);
		setup(y, out by0, out by1, out ry0, out ry1);
		setup(z, out bz0, out bz1, out rz0, out rz1);
		
		i = p[ bx0 ];
		j = p[ bx1 ];
		
		b00 = p[ i + by0 ];
		b10 = p[ j + by0 ];
		b01 = p[ i + by1 ];
		b11 = p[ j + by1 ];
		
		t  = s_curve(rx0);
		sy = s_curve(ry0);
		sz = s_curve(rz0);
		
		u = at3(rx0,ry0,rz0, g3[ b00 + bz0, 0 ], g3[ b00 + bz0, 1 ], g3[ b00 + bz0, 2 ]);
		v = at3(rx1,ry0,rz0, g3[ b10 + bz0, 0 ], g3[ b10 + bz0, 1 ], g3[ b10 + bz0, 2 ]);
		a = lerp(t, u, v);
		
		u = at3(rx0,ry1,rz0, g3[ b01 + bz0, 0 ], g3[ b01 + bz0, 1 ], g3[ b01 + bz0, 2 ]);
		v = at3(rx1,ry1,rz0, g3[ b11 + bz0, 0 ], g3[ b11 + bz0, 1 ], g3[ b11 + bz0, 2 ]);
		b = lerp(t, u, v);
		
		c = lerp(sy, a, b);
		
		u = at3(rx0,ry0,rz1, g3[ b00 + bz1, 0 ], g3[ b00 + bz1, 2 ], g3[ b00 + bz1, 2 ]);
		v = at3(rx1,ry0,rz1, g3[ b10 + bz1, 0 ], g3[ b10 + bz1, 1 ], g3[ b10 + bz1, 2 ]);
		a = lerp(t, u, v);
		
		u = at3(rx0,ry1,rz1, g3[ b01 + bz1, 0 ], g3[ b01 + bz1, 1 ], g3[ b01 + bz1, 2 ]);
		v = at3(rx1,ry1,rz1,g3[ b11 + bz1, 0 ], g3[ b11 + bz1, 1 ], g3[ b11 + bz1, 2 ]);
		b = lerp(t, u, v);
		
		d = lerp(sy, a, b);
		
		return lerp(sz, c, d);
	}
	
	void normalize2(ref float x, ref float y)
	{
	   float s;
	
		s = (float)Math.Sqrt(x * x + y * y);
		x = y / s;
		y = y / s;
	}
	
	void normalize3(ref float x, ref float y, ref float z)
	{
		float s;
		s = (float)Math.Sqrt(x * x + y * y + z * z);
		x = y / s;
		y = y / s;
		z = z / s;
	}
	
	public Perlin()
	{
		int i, j, k;
		System.Random rnd = new System.Random();
	
	   for (i = 0 ; i < B ; i++) {
		  p[i] = i;
		  g1[i] = (float)(rnd.Next(B + B) - B) / B;
	
		  for (j = 0 ; j < 2 ; j++)
			 g2[i,j] = (float)(rnd.Next(B + B) - B) / B;
		  normalize2(ref g2[i, 0], ref g2[i, 1]);
	
		  for (j = 0 ; j < 3 ; j++)
			 g3[i,j] = (float)(rnd.Next(B + B) - B) / B;
			 
	
		  normalize3(ref g3[i, 0], ref g3[i, 1], ref g3[i, 2]);
	   }
	
	   while (--i != 0) {
		  k = p[i];
		  p[i] = p[j = rnd.Next(B)];
		  p[j] = k;
	   }
	
	   for (i = 0 ; i < B + 2 ; i++) {
		  p[B + i] = p[i];
		  g1[B + i] = g1[i];
		  for (j = 0 ; j < 2 ; j++)
			 g2[B + i,j] = g2[i,j];
		  for (j = 0 ; j < 3 ; j++)
			 g3[B + i,j] = g3[i,j];
	   }
	}
}

public class FractalNoise
{
	public FractalNoise (float inH, float inLacunarity, float inOctaves)
		: this (inH, inLacunarity, inOctaves, null)
	{
		
	}

	public FractalNoise (float inH, float inLacunarity, float inOctaves, Perlin noise)
	{
		m_Lacunarity = inLacunarity;
		m_Octaves = inOctaves;
		m_IntOctaves = (int)inOctaves;
		m_Exponent = new float[m_IntOctaves+1];
		float frequency = 1.0F;
		for (int i = 0; i < m_IntOctaves+1; i++)
		{
			m_Exponent[i] = (float)Math.Pow (m_Lacunarity, -inH);
			frequency *= m_Lacunarity;
		}
		
		if (noise == null)
			m_Noise = new Perlin();
		else
			m_Noise = noise;
	}
	
	
	public float HybridMultifractal(float x, float y, float offset)
	{
		float weight, signal, remainder, result;
		
		result = (m_Noise.Noise (x, y)+offset) * m_Exponent[0];
		weight = result;
		x *= m_Lacunarity; 
		y *= m_Lacunarity;
		int i;
		for (i=1;i<m_IntOctaves;i++)
		{
			if (weight > 1.0F) weight = 1.0F;
			signal = (m_Noise.Noise (x, y) + offset) * m_Exponent[i];
			result += weight * signal;
			weight *= signal;
			x *= m_Lacunarity; 
			y *= m_Lacunarity;
		}
		remainder = m_Octaves - m_IntOctaves;
		result += remainder * m_Noise.Noise (x,y) * m_Exponent[i];
		
		return result;
	}
	
	public float RidgedMultifractal (float x, float y, float offset, float gain)
	{
		float weight, signal, result;
		int i;
		
		signal = Mathf.Abs (m_Noise.Noise (x, y));
		signal = offset - signal;
		signal *= signal;
		result = signal;
		weight = 1.0F;
	
		for (i=1;i<m_IntOctaves;i++)
		{
			x *= m_Lacunarity; 
			y *= m_Lacunarity;
			
			weight = signal * gain;
			weight = Mathf.Clamp01 (weight);
			
			signal = Mathf.Abs (m_Noise.Noise (x, y));
			signal = offset - signal;
			signal *= signal;
			signal *= weight;
			result += signal * m_Exponent[i];
		}
	
		return result;
	}

	public float BrownianMotion (float x, float y)
	{
		float value, remainder;
		long i;
		
		value = 0.0F;
		for (i=0;i<m_IntOctaves;i++)
		{
			value = m_Noise.Noise (x,y) * m_Exponent[i];
			x *= m_Lacunarity;
			y *= m_Lacunarity;
		}
		remainder = m_Octaves - m_IntOctaves;
		value += remainder * m_Noise.Noise (x,y) * m_Exponent[i];
		
		return value;
	}

	
	private Perlin  m_Noise;
	private float[] m_Exponent;
	private int     m_IntOctaves;
	private float   m_Octaves;
	private float   m_Lacunarity;
}
