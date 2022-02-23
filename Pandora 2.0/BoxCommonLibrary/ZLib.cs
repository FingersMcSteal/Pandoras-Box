using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace TheBox.Common
{
	/// <summary>
	/// Provides access to compression and decompression
	/// </summary>
	public class BoxZLib
	{
		private enum ZLibError : int
		{
			Z_OK = 0,
			Z_STREAM_END = 1,
			Z_NEED_DICT = 2,
			Z_ERRNO = (-1),
			Z_STREAM_ERROR = (-2),
			Z_DATA_ERROR = (-3),
			Z_MEM_ERROR = (-4),
			Z_BUF_ERROR = (-5),
			Z_VERSION_ERROR = (-6)
		}

		private enum ZLibCompressionLevel : int
		{
			Z_NO_COMPRESSION = 0,
			Z_BEST_SPEED = 1,
			Z_BEST_COMPRESSION = 9,
			Z_DEFAULT_COMPRESSION = (-1)
		}

		[DllImport( "zlib" )]
		private static extern string zlibVersion();
		[DllImport( "zlib" )]
		private static extern ZLibError compress( byte[] dest, ref int destLength, byte[] source, int sourceLength );
		[DllImport( "zlib" )]
		private static extern ZLibError compress2( byte[] dest, ref int destLength, byte[] source, int sourceLength, ZLibCompressionLevel level );
		[DllImport( "zlib" )]
		private static extern ZLibError uncompress( byte[] dest, ref int destLen, byte[] source, int sourceLen );

		/// <summary>
		/// Checks the zlib version for compatibility
		/// </summary>
		/// <returns>True if the liberary version matches, false otherwise or if the library can't be found</returns>
		public static bool CheckVersion()
		{
			string[] ver = null;

			try
			{
				ver = zlibVersion().Split( '.' );
			}
			catch ( DllNotFoundException )
			{
				return false;
			}

			return ver[0] == "1";
		}

		/// <summary>
		/// Compresses a Serializable object
		/// </summary>
		/// <param name="source">The Serializable object that should be compressed</param>
		/// <returns>An array of bytes</returns>
		public static byte[] Compress( object source )
		{
			try
			{
				// Xml serialization to a stream
				XmlSerializer serializer = new XmlSerializer( source.GetType() );
				MemoryStream stream = new MemoryStream();
				serializer.Serialize( stream, source );

				// Convert stream to bytes
				byte[] SourceBytes = stream.ToArray();

				stream.Close();

				int length = SourceBytes.Length;

				// Create output array
				int DestLength = SourceBytes.Length + 1;
				byte[] Dest = new byte[ DestLength ];

				// Compression
				ZLibError result = compress2( Dest, ref DestLength, SourceBytes, SourceBytes.Length, ZLibCompressionLevel.Z_BEST_COMPRESSION );

				if ( result != ZLibError.Z_OK )
				{
					return new byte[ 0 ];
				}
				else
				{
					// Trim the results according to the useful length
					byte[] compressed = new byte[ DestLength + 4 ];

					Array.Copy( Dest, 0, compressed, 4, DestLength );

					// Copy length to the start of the array
					compressed[ 0 ] = (byte) length;
					compressed[ 1 ] = (byte) ( length >> 8 );
					compressed[ 2 ] = (byte) ( length >> 16 );
					compressed[ 3 ] = (byte) ( length >> 24 );

					return compressed;
				}
			}
			catch ( Exception err )
			{
				System.Windows.Forms.Clipboard.SetDataObject( err.ToString() );
				return new byte[ 0 ];
			}
		}

		/// <summary>
		/// Decompresses data corresponding to a Serializable object
		/// </summary>
		/// <param name="data">The array of bytes representing the compressed stream</param>
		/// <param name="type">The type of the object being deserialized</param>
		/// <returns>A Serializable object</returns>
		public static object Decompress( byte[] data, Type type  )
		{
			try
			{
				// Get length
				int length = data[ 0 ] | ( data[ 1 ] << 8 ) | ( data[ 2 ] << 16 ) | ( data[ 3 ] << 24 );

				byte[] actualData = new byte[ data.Length - 4 ];
				Array.Copy( data, 4, actualData, 0, data.Length - 4 );

				// Uncompress
				byte[] output = new byte[ length ];

				ZLibError result = uncompress( output, ref length, actualData, actualData.Length );

				if ( result != ZLibError.Z_OK )
				{
					return null;
				}

				MemoryStream stream = new MemoryStream( output );

				XmlSerializer serializer = new XmlSerializer( type );

				object o = serializer.Deserialize( stream );

				stream.Close();

				return o;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Compresses a stream of bytes
		/// </summary>
		/// <param name="data">The input data to be compressed</param>
		/// <returns>The compressed bytes stream</returns>
		public static byte[] Compress( byte[] data )
		{
			int length = data.Length;
			int destLength = data.Length;
			byte[] dest = new byte[ data.Length ];

			ZLibError result = compress( dest, ref destLength, data, data.Length );

			if ( result != ZLibError.Z_OK )
				return null;

			byte[] res = new byte[ destLength + 4 ];
			Array.Copy( dest, 0, res, 4, destLength );

			// Copy length to the start of the array
			res[ 0 ] = (byte) ( length & 0xFF );
			res[ 1 ] = (byte) ( ( length >> 8 ) & 0xFF );
			res[ 2 ] = (byte) ( ( length >> 16 & 0xFF ) );
			res[ 3 ] = (byte) ( ( length >> 24 ) & 0xFF );

			return res;
		}

		/// <summary>
		/// Decompresses a stream of bytes
		/// </summary>
		/// <param name="data">The bytes stream to decompress</param>
		/// <returns>The decompressed stream</returns>
		public static byte[] Decompress( byte[] data )
		{
			int length = data[ 0 ] | ( data[ 1 ] << 8 ) | ( data[ 2 ] << 16 ) | ( data[ 3 ] << 24 );

			byte[] realData = new byte[ data.Length - 4 ];
			Array.Copy( data, 4, realData, 0, data.Length - 4 );

			byte[] result = new byte[ length ];

			ZLibError err = uncompress( result, ref length, realData, realData.Length );

			if ( err != ZLibError.Z_OK )
			{
				return null;
			}

			return result;
		}
	}
}