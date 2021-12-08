namespace AdventOfCode2021.Tools.Mathematics.Vectors
{
	using System;
	using System.Linq;

	public class VectorN
	{
		public float[] Values { get; }

		/// <summary>
		/// Total length of vector
		/// </summary>
		public float Length => MathF.Sqrt(Values.Select(v => MathF.Pow(v, 2)).Sum());

		public float this[int index]
		{
			get => Values[index];
			set => Values[index] = value;
		}

		public int Dimensions => Values.Length;

		public VectorN(int length)
		{
			Values = new float[length];
		}

		public static VectorN operator +(VectorN a, VectorN b)
		{
			VectorN output = new VectorN(a.Dimensions);

			for (int i = 0; i < a.Dimensions; i++)
			{
				output[i] = a[i] + b[i];
			}

			return output;
		}

		public static VectorN operator -(VectorN a, VectorN b)
		{
			VectorN output = new VectorN(a.Dimensions);

			for (int i = 0; i < a.Dimensions; i++)
			{
				output[i] = a[i] - b[i];
			}

			return output;
		}

		/// <summary>
		/// Dot product
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static VectorN operator *(VectorN a, VectorN b)
		{
			VectorN output = new VectorN(a.Dimensions);

			for (int i = 0; i < a.Dimensions; i++)
			{
				output[i] = a[i] * b[i];
			}

			return output;
		}

		public static VectorN operator *(VectorN a, float f)
		{
			VectorN output = new VectorN(a.Dimensions);

			for (int i = 0; i < a.Dimensions; i++)
			{
				output[i] = a[i] * f;
			}

			return output;
		}

		public static VectorN operator /(VectorN a, float f)
		{
			VectorN output = new VectorN(a.Dimensions);

			for (int i = 0; i < a.Dimensions; i++)
			{
				output[i] = a[i] / f;
			}

			return output;
		}

		public static bool operator ==(VectorN a, VectorN b)
		{
			if (a == null || b == null)
			{
				return false;
			}

			return a.Equals(b);
		}

		public static bool operator !=(VectorN a, VectorN b)
		{
			if (a == null || b == null)
			{
				return false;
			}

			return !a.Equals(b);
		}


		protected bool Equals(VectorN other)
		{
			if (Dimensions != other.Dimensions)
			{
				return false;
			}

			for (int i = 0; i < Values.Length; i++)
			{
				if (MathF.Abs(Values[i] - other[i]) > 0.001f)
				{
					return false;
				}
			}

			return false;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((VectorN) obj);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return (Values != null ? Values.GetHashCode() : 0);
		}
	}
}
