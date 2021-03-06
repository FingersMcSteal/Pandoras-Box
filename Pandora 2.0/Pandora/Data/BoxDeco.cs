using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

using TheBox.Common;

namespace TheBox.Data
{
	/// <summary>
	/// Provides information about a static deco item
	/// </summary>
	[ Serializable ]
	public class BoxDeco : IComparable
	{
		private string m_Name;
		private int m_ID;

		/// <summary>
		/// Creates a new BoxDeco object
		/// </summary>
		public BoxDeco()
		{
		}

		/// <summary>
		/// Gets or sets the name of the decoration object
		/// </summary>
		[ XmlAttribute ]
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		/// <summary>
		/// Gets or sets the ID of the decoration object
		/// </summary>
		[ XmlAttribute ]
		public int ID
		{
			get { return m_ID; }
			set { m_ID = value; }
		}

		[ XmlIgnore, System.ComponentModel.Browsable( false ) ]
		/// <summary>
		/// Gets the TreeNode corresponding to this BoxDeco object
		/// </summary>
		public System.Windows.Forms.TreeNode TreeNode
		{
			get
			{
				System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode( this.m_Name );
				node.Tag = this;

				return node;
			}
		}

		#region IComparable Members

		public int CompareTo(object obj)
		{
			BoxDeco cmp = obj as BoxDeco;

			if ( cmp == null )
				return 0;

			int res = m_Name.CompareTo( cmp.m_Name );

			if ( res == 0 )
			{
				return m_ID.CompareTo( cmp.m_ID );
			}
			else
			{
				return res;
			}
		}

		#endregion
	}

	/// <summary>
	/// Provides a the list of available decoration items
	/// </summary>
	[ Serializable, XmlInclude( typeof( BoxDeco ) ), XmlInclude( typeof( GenericNode ) ) ]
	public class BoxDecoList
	{
		private ArrayList m_Structure;

		/// <summary>
		/// Gets or sets the structure of the decoration items available
		/// </summary>
		public ArrayList Structure
		{
			get { return m_Structure; }
			set { m_Structure = value; }
		}

		/// <summary>
		/// Creates a new BoxDecoList object
		/// </summary>
		public BoxDecoList()
		{
			m_Structure = new ArrayList();
		}

		/// <summary>
		/// Loads a BoxDecoList from file
		/// </summary>
		/// <param name="filename">The file to read from</param>
		/// <returns>The BoxDecoList read from the specified file</returns>
		public static BoxDecoList FromFile( string filename )
		{
			BoxDecoList list = null;

			try
			{
				FileStream stream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite );
				XmlSerializer serializer = new XmlSerializer( typeof( BoxDecoList ) );
				list = serializer.Deserialize( stream ) as BoxDecoList;
				stream.Close();
				return list;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Saves the BoxDecoList to file
		/// </summary>
		/// <param name="filename">The file to save to</param>
		public void Save( string filename )
		{
			try
			{
				FileStream stream = new FileStream( filename, FileMode.Create, FileAccess.Write, FileShare.Read );
				XmlSerializer serializer = new XmlSerializer( typeof( BoxDecoList ) );
				serializer.Serialize( stream, this );
				stream.Close();
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, "Couldn't save custom deco to {0}", filename );
			}
		}
	}
}
