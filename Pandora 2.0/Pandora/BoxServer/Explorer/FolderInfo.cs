using System;
using System.IO;
using System.Collections;
using System.Xml.Serialization;
using System.Windows.Forms;

using TheBox.Common;

namespace TheBox.BoxServer
{
	/// <summary>
	/// Provides Pandora with information about files and folders on the server
	/// </summary>
	[ Serializable, XmlInclude( typeof( GenericNode ) ) ]
	public class FolderInfo : ExplorerMessage
	{
		private GenericNode m_Structure;

		/// <summary>
		/// Gets or sets the Structure of the allowed folders on the server
		/// </summary>
		public GenericNode Structure
		{
			get
			{
				return m_Structure;
			}
			set
			{
				m_Structure = value;
			}
		}

		/// <summary>
		/// Creates a new FolderInfo object
		/// </summary>
		public FolderInfo()
		{
		}

		/// <summary>
		/// Gets the TreeNodes corresponding to the folder structure
		/// </summary>
		/// <returns>The TreeNode corresponding to the top of the hierarchy</returns>
		public TreeNode[] GetTreeNodes()
		{
			TreeNode[] nodes = new TreeNode[ m_Structure.Elements.Count ];

			for ( int i = 0; i < nodes.Length; i++ )
			{
				GenericNode gNode = m_Structure.Elements[ i ] as GenericNode;

				TreeNode node = new TreeNode( gNode.Name );
				node.ImageIndex = 1;
				node.SelectedImageIndex = 1;

				node.Nodes.AddRange( DoElements( gNode.Elements ) );

				nodes[ i ] = node;
			}

			return nodes;
		}

		/// <summary>
		/// Processes the elements of a folder
		/// </summary>
		/// <param name="elements">An array list of items in a folder</param>
		/// <returns>The corresponding tree nodes</returns>
		private TreeNode[] DoElements( ArrayList elements )
		{
			TreeNode[] nodes = new TreeNode[ elements.Count ];

			for( int i = 0; i < elements.Count; i++ )
			{
				object obj = elements[ i ];

				if ( obj is GenericNode )
				{
					GenericNode gNode = obj as GenericNode;

					TreeNode folder = new TreeNode( gNode.Name );
					folder.ImageIndex = 1;
					folder.SelectedImageIndex = 1;

					folder.Nodes.AddRange( DoElements( gNode.Elements ) );

					nodes[ i ] = folder;
				}
				else if ( obj is string )
				{
					string file = (string) obj;

					TreeNode fileNode = new TreeNode( file );

					if ( file.ToLower().EndsWith( ".cs" ) )
					{
						fileNode.ImageIndex = 0;
						fileNode.SelectedImageIndex = 0;
					}
					else if ( file.ToLower().EndsWith( ".vb" ) )
					{
						fileNode.ImageIndex = 2;
						fileNode.SelectedImageIndex = 2;
					}
					else if ( file.ToLower().EndsWith( ".txt" ) )
					{
						fileNode.ImageIndex = 3;
						fileNode.SelectedImageIndex = 3;
					}
					else if ( file.ToLower().EndsWith( ".xml" ) )
					{
						fileNode.ImageIndex = 4;
						fileNode.SelectedImageIndex = 4;
					}

					nodes[ i ] = fileNode;
				}
			}

			return nodes;
		}
	}
}
