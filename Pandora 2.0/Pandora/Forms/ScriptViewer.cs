using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using TheBox.BoxServer;

namespace TheBox.Forms
{
	/// <summary>
	/// Summary description for ScriptViewer.
	/// </summary>
	public class ScriptViewer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView Tree;
		private System.Windows.Forms.ImageList TreeImages;
		private System.Windows.Forms.ToolBar tBar;
		private System.Windows.Forms.ToolBarButton bRefresh;
		private System.Windows.Forms.ToolBarButton bDownload;
		private System.Windows.Forms.ToolBarButton bUpload;
		private System.Windows.Forms.ToolBarButton bDelete;
		private System.Windows.Forms.ToolBarButton bExit;
		private System.Windows.Forms.ImageList BarImages;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.TextBox txFolder;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusBar sBar;
		private System.Windows.Forms.OpenFileDialog OpenFile;
		private System.Windows.Forms.SaveFileDialog SaveFile;
		private System.Windows.Forms.ToolBarButton bCreateFolder;
		private System.Windows.Forms.ToolBarButton bEdit;
		private System.ComponentModel.IContainer components;

		public ScriptViewer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Localized tool tips
			bRefresh.ToolTipText = Pandora.TextProvider[ "Script.Refresh" ];
			bDownload.ToolTipText = Pandora.TextProvider[ "Script.Download" ];
			bUpload.ToolTipText = Pandora.TextProvider[ "Script.Upload" ];
			bDelete.ToolTipText = Pandora.TextProvider[ "Script.Delete" ];
			bExit.ToolTipText = Pandora.TextProvider[ "Script.Exit" ];
			bCreateFolder.ToolTipText = Pandora.TextProvider[ "Script.CreateFolder" ];
			bEdit.ToolTipText = "Download and edit";

			Pandora.LocalizeControl( this );

			m_Editors = new ArrayList();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ScriptViewer));
			this.Tree = new System.Windows.Forms.TreeView();
			this.TreeImages = new System.Windows.Forms.ImageList(this.components);
			this.tBar = new System.Windows.Forms.ToolBar();
			this.bRefresh = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.bCreateFolder = new System.Windows.Forms.ToolBarButton();
			this.bEdit = new System.Windows.Forms.ToolBarButton();
			this.bDownload = new System.Windows.Forms.ToolBarButton();
			this.bUpload = new System.Windows.Forms.ToolBarButton();
			this.bDelete = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.bExit = new System.Windows.Forms.ToolBarButton();
			this.BarImages = new System.Windows.Forms.ImageList(this.components);
			this.txFolder = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.sBar = new System.Windows.Forms.StatusBar();
			this.OpenFile = new System.Windows.Forms.OpenFileDialog();
			this.SaveFile = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// Tree
			// 
			this.Tree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.Tree.HideSelection = false;
			this.Tree.ImageList = this.TreeImages;
			this.Tree.Location = new System.Drawing.Point(4, 104);
			this.Tree.Name = "Tree";
			this.Tree.Size = new System.Drawing.Size(432, 396);
			this.Tree.TabIndex = 3;
			this.Tree.DoubleClick += new System.EventHandler(this.Tree_DoubleClick);
			this.Tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tree_AfterSelect);
			this.Tree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.Tree_AfterLabelEdit);
			// 
			// TreeImages
			// 
			this.TreeImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.TreeImages.ImageSize = new System.Drawing.Size(16, 16);
			this.TreeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeImages.ImageStream")));
			this.TreeImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tBar
			// 
			this.tBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					this.bRefresh,
																					this.toolBarButton2,
																					this.bCreateFolder,
																					this.bEdit,
																					this.bDownload,
																					this.bUpload,
																					this.bDelete,
																					this.toolBarButton1,
																					this.bExit});
			this.tBar.ButtonSize = new System.Drawing.Size(65, 36);
			this.tBar.DropDownArrows = true;
			this.tBar.ImageList = this.BarImages;
			this.tBar.Location = new System.Drawing.Point(0, 0);
			this.tBar.Name = "tBar";
			this.tBar.ShowToolTips = true;
			this.tBar.Size = new System.Drawing.Size(440, 44);
			this.tBar.TabIndex = 4;
			this.tBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tBar_ButtonClick);
			// 
			// bRefresh
			// 
			this.bRefresh.ImageIndex = 0;
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// bCreateFolder
			// 
			this.bCreateFolder.ImageIndex = 1;
			// 
			// bEdit
			// 
			this.bEdit.ImageIndex = 6;
			// 
			// bDownload
			// 
			this.bDownload.ImageIndex = 3;
			// 
			// bUpload
			// 
			this.bUpload.ImageIndex = 2;
			// 
			// bDelete
			// 
			this.bDelete.ImageIndex = 5;
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// bExit
			// 
			this.bExit.ImageIndex = 4;
			// 
			// BarImages
			// 
			this.BarImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.BarImages.ImageSize = new System.Drawing.Size(32, 32);
			this.BarImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("BarImages.ImageStream")));
			this.BarImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// txFolder
			// 
			this.txFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txFolder.BackColor = System.Drawing.SystemColors.Control;
			this.txFolder.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.txFolder.Location = new System.Drawing.Point(96, 76);
			this.txFolder.Name = "txFolder";
			this.txFolder.ReadOnly = true;
			this.txFolder.Size = new System.Drawing.Size(340, 20);
			this.txFolder.TabIndex = 5;
			this.txFolder.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 76);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "Script.CurrFold";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// sBar
			// 
			this.sBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.sBar.Location = new System.Drawing.Point(0, 44);
			this.sBar.Name = "sBar";
			this.sBar.Size = new System.Drawing.Size(440, 22);
			this.sBar.TabIndex = 7;
			// 
			// ScriptViewer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 501);
			this.Controls.Add(this.sBar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txFolder);
			this.Controls.Add(this.tBar);
			this.Controls.Add(this.Tree);
			this.Name = "ScriptViewer";
			this.Text = "Script.Title";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ScriptViewer_Closing);
			this.Load += new System.EventHandler(this.ScriptViewer_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private FolderInfo m_Info;
		private string m_Folder;
		private string m_File;
		private bool m_CreateFolder = false;

		private string Folder
		{
			get { return m_Folder; }
			set
			{
				if ( value.StartsWith( Path.DirectorySeparatorChar.ToString() ) )
					m_Folder = value.Substring( 1 );
				else
					m_Folder = value;
			}
		}

		/// <summary>
		/// On Load: Get the folder information from the server
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ScriptViewer_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Connects to the BoxServer and retrieves folder information
		/// </summary>
		/// <returns></returns>
		public bool GetFolderInfo()
		{
			TheBox.BoxServer.ExplorerRequest msg = new ExplorerRequest();

			BoxMessage result = Pandora.SendToServer( msg );

			m_Info = result as FolderInfo;

			if ( m_Info != null )
			{
				DoTree();
			}
			
			return m_Info != null;
		}

		/// <summary>
		/// Refreshes the tree
		/// </summary>
		private void DoTree()
		{
			Tree.BeginUpdate();
			Tree.Nodes.Clear();
			Tree.Nodes.AddRange( m_Info.GetTreeNodes() );
			Tree.EndUpdate();
		}

		/// <summary>
		/// Refresh the information on the server
		/// </summary>
		private void RefreshInfo()
		{
			TheBox.BoxServer.ExplorerRequest msg = new ExplorerRequest();

			BoxMessage result = Pandora.SendToServer( msg );

			m_Info = result as FolderInfo;

			if ( m_Info != null )
			{
				DoTree();
			}
			else
			{
				Close();
			}

//			Pandora.Profile.Server.FillBoxMessage( msg );
//
//			BoxServerForm form = new BoxServerForm( msg );
//
//			if ( form.ShowDialog() == DialogResult.OK )
//			{
//				m_Info = form.Response as FolderInfo;
//
//				if ( m_Info != null )
//				{
//					DoTree();
//				}
//				else
//				{
//					Close();
//				}
//			}
//			else
//			{
//				Close();
//			}
		}

		/// <summary>
		/// Gets the folder string by recursing the tree structure
		/// </summary>
		/// <param name="node">The TreeNode to start from (must be a folder node)</param>
		/// <returns>The string containing the current folder</returns>
		private string GetFolder( TreeNode node )
		{
			char sep = System.IO.Path.DirectorySeparatorChar;

			string folder = sep.ToString();

			TreeNode n = node;

			while ( n != null )
			{
				folder = sep + n.Text + folder;
				n = n.Parent;
			}

			Folder = folder;

			folder = ".." + folder;

			return folder;
		}

		/// <summary>
		/// Node selected on the tree. Update tool bar and current folder
		/// </summary>
		private void Tree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if ( e.Node == null )
			{
				bDownload.Enabled = false;
				bUpload.Enabled = false;
				bDelete.Enabled = false;
				bEdit.Enabled = false;

				Tree.LabelEdit = false;

				txFolder.Text = "";
				Folder = null;
				m_File = null;
			}
			else
			{
				if ( e.Node.Parent == null )
				{
					// Top Node
					Tree.LabelEdit = false;

					bDownload.Enabled = false;
					bUpload.Enabled = true;
					bDelete.Enabled = false;
					bEdit.Enabled = false;

					txFolder.Text = GetFolder( e.Node );
					m_File = null;
				}
				else
				{
					Tree.LabelEdit = true;

					if ( e.Node.ImageIndex == 1 )
					{
						// Folder node
						txFolder.Text = GetFolder( e.Node );

						bDownload.Enabled = false;
						bUpload.Enabled = true;
						bDelete.Enabled = true;
						bEdit.Enabled = false;

						m_File = null;
					}
					else
					{
						// File node
						txFolder.Text = GetFolder( e.Node.Parent );

						bDownload.Enabled = true;
						bUpload.Enabled = true;
						bDelete.Enabled = true;
						bEdit.Enabled = true;

						m_File = e.Node.Text;
					}
				}
			}
		}

		/// <summary>
		/// Gets the appropriate filter for a filename
		/// </summary>
		/// <param name="filename">The file to find the filter for</param>
		/// <returns>The string that can be used as filter in an open or save dialog</returns>
		private string GetFilter( string filename )
		{
			filename = filename.ToLower();

			if ( filename.EndsWith( ".cs" ) )
			{
				return "C# Files (*.cs)|*.cs";
			}
			else if ( filename.EndsWith( ".vb" ) )
			{
				return "Visaul Basic Files (*.vb)|*.cs";
			}
			else if ( filename.EndsWith( ".txt" ) )
			{
				return "Text Files (*.txt)|*.txt";
			}
			else if ( filename.EndsWith( ".xml" ) )
			{
				return "Xml Files (*.xml)|*.xml";
			}

			return null;
		}

		/// <summary>
		/// Gets a TreeNode corresponding to a filename
		/// </summary>
		/// <param name="filename">The filename being added</param>
		/// <returns>The corresponding TreeNode</returns>
		private TreeNode GetTreeNode( string file )
		{
			TreeNode fileNode = new TreeNode( file );

			file = file.ToLower();

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

			return fileNode;
		}

		/// <summary>
		/// Downloads the selected file from the server
		/// </summary>
		private void Download()
		{	
			if ( Folder != null && m_File != null )
			{
				string filename = System.IO.Path.Combine( Folder, m_File );

				SaveFile.Filter = GetFilter( filename );
				SaveFile.FileName = Path.GetFileNameWithoutExtension( filename );

				if ( SaveFile.ShowDialog() == DialogResult.OK )
				{
					TheBox.BoxServer.DownloadRequest msg = new DownloadRequest();

					Pandora.Profile.Server.FillBoxMessage( msg );

					msg.Filename = filename;

					BoxServerForm form = new BoxServerForm( msg );

					if ( form.ShowDialog() == DialogResult.OK )
					{
						TheBox.BoxServer.FileTransport response = form.Response as FileTransport;

						if ( response != null )
						{
							// Download succesful
							try
							{
								StreamWriter writer = new StreamWriter( SaveFile.FileName, false );
								writer.Write( response.Text );
								writer.Close();

								sBar.Text = string.Format( Pandora.TextProvider[ "Script.DownOk" ], m_File, SaveFile.FileName );
							}
							catch ( Exception err )
							{
								Pandora.Log.WriteError( err, "Can't write file {0} to {1}", filename, SaveFile.FileName );
								MessageBox.Show( Pandora.TextProvider[ "Server.CantWriteFile" ] );

								sBar.Text = string.Format( Pandora.TextProvider[ "Script.GenericErr" ] );
							}
						}
						else
						{
							sBar.Text = Pandora.TextProvider[ "Script.UnexpectedErr" ];
						}
					}
				}
			}
		}

		/// <summary>
		/// Uploads a file to the server
		/// </summary>
		private void Upload()
		{
			if ( Folder == null )
				return;

			if ( OpenFile.ShowDialog() != DialogResult.OK )
				return;

			string filename = Path.Combine( Folder, Path.GetFileName( OpenFile.FileName ) );

			TheBox.BoxServer.FileTransport msg = new FileTransport();
			msg.Filename = filename;

			Pandora.Profile.Server.FillBoxMessage( msg );
			
			// Read text from the file
			try
			{
				StreamReader reader = new StreamReader( OpenFile.FileName );
				msg.Text = reader.ReadToEnd();
				reader.Close();
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, "Error when reading text file {0}", OpenFile.FileName );
				sBar.Text = Pandora.TextProvider[ "Script.ReadErr" ];
				return;
			}

			GenericOK response =  BoxConnection.ProcessMessage( msg, true ) as GenericOK;

			if ( response != null )
			{
				// Success
				sBar.Text = Pandora.TextProvider[ "Script.UploadOk" ];

				// Add item
				TreeNode node = GetTreeNode( Path.GetFileName( OpenFile.FileName ) );
				TreeNode parent = null;

				if ( m_File == null )
				{
					parent = Tree.SelectedNode;
				}
				else
				{
					parent = Tree.SelectedNode.Parent;
				}

				bool exists = false;
				foreach( TreeNode exist in parent.Nodes )
				{
					if ( exist.Text.ToLower() == node.Text.ToLower() )
					{
						exists = true;
						break;
					}
				}

				if ( ! exists )
					parent.Nodes.Add( node );

			}
			else
			{
				sBar.Text = Pandora.TextProvider[ "Script.UploadFail" ];
			}
		}

		/// <summary>
		/// Deletes a file or folder from the server
		/// </summary>
		private void DeleteFromServer()
		{
			if ( Folder == null )
				return;

			if ( MessageBox.Show( this, Pandora.TextProvider[ "Script.ConfirmDel" ], "", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.No )
				return;

			TheBox.BoxServer.DeleteRequest msg = new DeleteRequest();
			Pandora.Profile.Server.FillBoxMessage( msg );

			string target = null;

			if ( m_File == null )
				target = Folder;
			else
				target = Path.Combine( Folder, m_File );

			msg.Path = target;

			GenericOK response = BoxConnection.ProcessMessage( msg, true ) as GenericOK;

			if ( response != null )
			{
				sBar.Text = Pandora.TextProvider[ "Script.DelOk" ];

				Tree.Nodes.Remove( Tree.SelectedNode );
			}
			else
			{
				sBar.Text = Pandora.TextProvider[ "Script.DelErr" ];
			}
		}

		/// <summary>
		/// Renames an item
		/// </summary>
		private void Tree_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			if ( m_CreateFolder )
			{
				m_CreateFolder = false;

				// Create new folder
				if ( e.Label == null || e.Label.Length == 0 )
				{
					e.CancelEdit = true;
					Tree.Nodes.Remove( e.Node );
					return;
				}

				TheBox.BoxServer.CreateFolder msg = new CreateFolder();
				Pandora.Profile.Server.FillBoxMessage( msg );

				string[] path = Folder.Split( Path.DirectorySeparatorChar );
				string parentpath = "";

				int end = path.Length - 1;

				if ( Folder.EndsWith( Path.DirectorySeparatorChar.ToString() ) )
					end--;

				for ( int i = 0; i < end; i++ )
				{
					parentpath += path[i] + Path.DirectorySeparatorChar;
				}

				msg.Folder = Path.Combine( parentpath, e.Label );

				GenericOK response = BoxConnection.ProcessMessage( msg, true ) as GenericOK;

				if ( response != null )
				{
					// Success
					sBar.Text = Pandora.TextProvider[ "Script.CrateOk" ];

					e.Node.Text = e.Label;
					txFolder.Text = GetFolder( e.Node );
				}
				else
				{
					sBar.Text = Pandora.TextProvider[ "Script.CreateFail" ];

					e.CancelEdit = true;
					Tree.Nodes.Remove( e.Node );
				}
			}
			else
			{
				// Rename item
				if ( Folder == null )
					return;

				if ( e.Label == null || e.Label.Length == 0 )
				{
					e.CancelEdit = true;
					return;
				}

				string old = null;

				if ( m_File == null )
					old = Folder;
				else
					old = Path.Combine( Folder, m_File );

				TheBox.BoxServer.MoveRequest msg = new MoveRequest();

				Pandora.Profile.Server.FillBoxMessage( msg );

				msg.OldPath = old;
				msg.NewPath = Path.Combine( Folder, e.Label );

				GenericOK response = BoxConnection.ProcessMessage( msg, true ) as GenericOK;

				if ( response != null )
				{
					// Success
					sBar.Text = Pandora.TextProvider[ "Script.RenOk" ];
					e.Node.Text = e.Label;
					txFolder.Text = GetFolder( e.Node );
				}
				else
				{
					sBar.Text = Pandora.TextProvider[ "Script.RenFail" ];
					e.CancelEdit = true;
				}
			}
		}

		private void tBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if ( e.Button == bRefresh )
			{
				RefreshInfo();
			}
			else if ( e.Button == bCreateFolder )
			{
				TreeNode n = new TreeNode( "NewFolder" );
				n.ImageIndex = 1;
				n.SelectedImageIndex = 1;

				if ( m_File == null )
					Tree.SelectedNode.Nodes.Add( n );
				else
					Tree.SelectedNode.Parent.Nodes.Add( n );

				Tree.SelectedNode = n;

				m_CreateFolder = true;

				n.BeginEdit();
			}
			else if ( e.Button == bDownload )
			{
				Download();
			}
			else if ( e.Button == bUpload )
			{
				Upload();
			}
			else if ( e.Button == bDelete )
			{
				DeleteFromServer();
			}
			else if ( e.Button == bExit )
			{
				Close();
			}
			else if ( e.Button == bEdit )
			{
				RemoteEdit();
			}
		}
		private ArrayList m_Editors;
		private bool m_SelfClosing = false;

		private void RemoteEdit()
		{
			if ( Folder != null && m_File != null )
			{
				string filename = System.IO.Path.Combine( Folder, m_File );
				TheBox.BoxServer.DownloadRequest msg = new DownloadRequest();
				Pandora.Profile.Server.FillBoxMessage( msg );
				msg.Filename = filename;

				BoxServerForm form = new BoxServerForm( msg );

				if ( form.ShowDialog() == DialogResult.OK )
				{
					TheBox.BoxServer.FileTransport response = form.Response as FileTransport;

					if ( response != null )
					{
						// Download succesful
						RemoteEditor f = new RemoteEditor( filename, response.Text );
						Pandora.LocalizeControl( f );
						f.Show();
						f.Closed += new EventHandler(form_Closed);
						m_Editors.Add( f );
					}
					else
					{
						sBar.Text = Pandora.TextProvider[ "Script.UnexpectedErr" ];
					}
				}
			}
		}

		private void form_Closed(object sender, EventArgs e)
		{
			if ( m_SelfClosing )
				return;

			m_Editors.Remove( sender );
		}

		private void ScriptViewer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			m_SelfClosing = true;

			foreach( Form form in m_Editors )
			{
				form.Close();
			}
		}

		private void Tree_DoubleClick(object sender, System.EventArgs e)
		{
			if ( bEdit.Enabled )
				RemoteEdit();
		}
	}
}