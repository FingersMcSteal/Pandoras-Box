using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TheBox.BoxServer;
using System.IO;

namespace TheBox.Forms
{
	/// <summary>
	/// Summary description for RemoteEditor.
	/// </summary>
	public class RemoteEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.StatusBar sBar;
		private System.Windows.Forms.RichTextBox tb;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string m_File;
		private string m_Text;
		private System.Windows.Forms.MenuItem miRemoteSave;
		private System.Windows.Forms.MenuItem miLocalSave;
		private System.Windows.Forms.MenuItem miExit;
		private System.Windows.Forms.SaveFileDialog SaveFile;
		private bool m_Modified = false;

		public RemoteEditor( string file, string text )
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_File = file;
			m_Text = text;
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
			this.sBar = new System.Windows.Forms.StatusBar();
			this.tb = new System.Windows.Forms.RichTextBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.miRemoteSave = new System.Windows.Forms.MenuItem();
			this.miLocalSave = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.miExit = new System.Windows.Forms.MenuItem();
			this.SaveFile = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// sBar
			// 
			this.sBar.Location = new System.Drawing.Point(0, 371);
			this.sBar.Name = "sBar";
			this.sBar.Size = new System.Drawing.Size(536, 22);
			this.sBar.TabIndex = 0;
			// 
			// tb
			// 
			this.tb.AcceptsTab = true;
			this.tb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tb.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb.HideSelection = false;
			this.tb.Location = new System.Drawing.Point(0, 0);
			this.tb.Name = "tb";
			this.tb.Size = new System.Drawing.Size(536, 371);
			this.tb.TabIndex = 1;
			this.tb.Text = "";
			this.tb.TextChanged += new System.EventHandler(this.tb_TextChanged);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.miRemoteSave,
																					  this.miLocalSave,
																					  this.menuItem4,
																					  this.miExit});
			this.menuItem1.Text = "File";
			// 
			// miRemoteSave
			// 
			this.miRemoteSave.Index = 0;
			this.miRemoteSave.Text = "Remote Save";
			this.miRemoteSave.Click += new System.EventHandler(this.miRemoteSave_Click);
			// 
			// miLocalSave
			// 
			this.miLocalSave.Index = 1;
			this.miLocalSave.Text = "Local Save";
			this.miLocalSave.Click += new System.EventHandler(this.miLocalSave_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "-";
			// 
			// miExit
			// 
			this.miExit.Index = 3;
			this.miExit.Text = "Exit";
			this.miExit.Click += new System.EventHandler(this.miExit_Click);
			// 
			// RemoteEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 393);
			this.Controls.Add(this.tb);
			this.Controls.Add(this.sBar);
			this.Menu = this.mainMenu1;
			this.Name = "RemoteEditor";
			this.Text = "RemoteEditor";
			this.Load += new System.EventHandler(this.RemoteEditor_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void RemoteEditor_Load(object sender, System.EventArgs e)
		{
			tb.Text = m_Text;
		}

		private void tb_TextChanged(object sender, System.EventArgs e)
		{
			m_Modified = true;
			m_Text = tb.Text;
		}

		private void miRemoteSave_Click(object sender, System.EventArgs e)
		{
			TheBox.BoxServer.FileTransport msg = new TheBox.BoxServer.FileTransport();
			msg.Filename = m_File;

			Pandora.Profile.Server.FillBoxMessage( msg );
			msg.Text = m_Text;

			GenericOK response =  BoxConnection.ProcessMessage( msg, true ) as GenericOK;

			if ( response != null )
			{
				// Success
				sBar.Text = "Remote save succesful";
			}
			else
			{
				sBar.Text = "Remote save failed";
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

		private void miLocalSave_Click(object sender, System.EventArgs e)
		{
			SaveFile.Filter = GetFilter( m_File );
			SaveFile.FileName = Path.GetFileNameWithoutExtension( m_File );

			if ( SaveFile.ShowDialog() != DialogResult.OK )
				return;

			try
			{
				StreamWriter writer = new StreamWriter( SaveFile.FileName, false );
				writer.Write( m_Text );
				writer.Close();

				sBar.Text = "File saved";
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, "Can't write file {0} to {1}", m_File, SaveFile.FileName );

				sBar.Text = "Save failed";
			}
		}

		private void miExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}