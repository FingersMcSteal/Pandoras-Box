using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using TheBox.Data;

namespace TheBox.Forms
{
	/// <summary>
	/// Summary description for RandomTileTemplate.
	/// </summary>
	public class RandomTileTemplate : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox cmbGroups;
		private System.Windows.Forms.GroupBox grpGroup;
		private System.Windows.Forms.TextBox txNew;
		private System.Windows.Forms.Button bDelete;
		private System.Windows.Forms.Button bNew;
		private System.Windows.Forms.ListBox lst;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label labQuick;
		private System.Windows.Forms.TextBox txName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txItems;
		private System.Windows.Forms.Button bAdd;
		private System.Windows.Forms.Button bDelItem;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RandomTileTemplate()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Pandora.LocalizeControl( this );
		}

		private RandomTilesList m_TileSet;
		private RandomTiles m_List;

		private RandomTilesList TileSet
		{
			get { return m_TileSet; }
			set
			{
				m_TileSet = value;

				grpGroup.Enabled = m_TileSet != null;
				bDelete.Enabled = m_TileSet != null;

				if ( m_TileSet != null )
				{
					UpdateTileset();
				}
				else
				{
					grpGroup.Text = "";
				}
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RandomTileTemplate));
			this.cmbGroups = new System.Windows.Forms.ComboBox();
			this.grpGroup = new System.Windows.Forms.GroupBox();
			this.bDelItem = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.bAdd = new System.Windows.Forms.Button();
			this.txItems = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labQuick = new System.Windows.Forms.Label();
			this.lst = new System.Windows.Forms.ListBox();
			this.txNew = new System.Windows.Forms.TextBox();
			this.bDelete = new System.Windows.Forms.Button();
			this.bNew = new System.Windows.Forms.Button();
			this.grpGroup.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmbGroups
			// 
			this.cmbGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGroups.Location = new System.Drawing.Point(8, 8);
			this.cmbGroups.Name = "cmbGroups";
			this.cmbGroups.Size = new System.Drawing.Size(121, 21);
			this.cmbGroups.TabIndex = 0;
			this.cmbGroups.SelectedIndexChanged += new System.EventHandler(this.cmbGroups_SelectedIndexChanged);
			// 
			// grpGroup
			// 
			this.grpGroup.Controls.Add(this.bDelItem);
			this.grpGroup.Controls.Add(this.groupBox2);
			this.grpGroup.Controls.Add(this.groupBox1);
			this.grpGroup.Controls.Add(this.lst);
			this.grpGroup.Enabled = false;
			this.grpGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpGroup.Location = new System.Drawing.Point(8, 32);
			this.grpGroup.Name = "grpGroup";
			this.grpGroup.Size = new System.Drawing.Size(432, 312);
			this.grpGroup.TabIndex = 1;
			this.grpGroup.TabStop = false;
			// 
			// bDelItem
			// 
			this.bDelItem.Enabled = false;
			this.bDelItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bDelItem.Location = new System.Drawing.Point(8, 280);
			this.bDelItem.Name = "bDelItem";
			this.bDelItem.Size = new System.Drawing.Size(120, 23);
			this.bDelItem.TabIndex = 4;
			this.bDelItem.Text = "Common.Delete";
			this.bDelItem.Click += new System.EventHandler(this.bDelItem_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.bAdd);
			this.groupBox2.Controls.Add(this.txItems);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.txName);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(136, 88);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(288, 216);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Random.Esplicit";
			// 
			// bAdd
			// 
			this.bAdd.Enabled = false;
			this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bAdd.Location = new System.Drawing.Point(8, 184);
			this.bAdd.Name = "bAdd";
			this.bAdd.TabIndex = 4;
			this.bAdd.Text = "Common.Add";
			this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
			// 
			// txItems
			// 
			this.txItems.AllowDrop = true;
			this.txItems.Location = new System.Drawing.Point(88, 80);
			this.txItems.Multiline = true;
			this.txItems.Name = "txItems";
			this.txItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txItems.Size = new System.Drawing.Size(192, 128);
			this.txItems.TabIndex = 3;
			this.txItems.Text = "";
			this.txItems.DragDrop += new System.Windows.Forms.DragEventHandler(this.txItems_DragDrop);
			this.txItems.TextChanged += new System.EventHandler(this.txItems_TextChanged);
			this.txItems.DragEnter += new System.Windows.Forms.DragEventHandler(this.txItems_DragEnter);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(272, 32);
			this.label2.TabIndex = 2;
			this.label2.Text = "Random.AddItems";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Common.Name";
			// 
			// txName
			// 
			this.txName.Location = new System.Drawing.Point(88, 16);
			this.txName.Name = "txName";
			this.txName.Size = new System.Drawing.Size(192, 20);
			this.txName.TabIndex = 0;
			this.txName.Text = "";
			this.txName.TextChanged += new System.EventHandler(this.txName_TextChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labQuick);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(136, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(288, 80);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Random.Quick";
			// 
			// labQuick
			// 
			this.labQuick.AllowDrop = true;
			this.labQuick.Location = new System.Drawing.Point(24, 24);
			this.labQuick.Name = "labQuick";
			this.labQuick.Size = new System.Drawing.Size(248, 48);
			this.labQuick.TabIndex = 0;
			this.labQuick.Text = "Random.Drag";
			this.labQuick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labQuick.DragEnter += new System.Windows.Forms.DragEventHandler(this.labQuick_DragEnter);
			this.labQuick.Paint += new System.Windows.Forms.PaintEventHandler(this.labQuick_Paint);
			this.labQuick.DragDrop += new System.Windows.Forms.DragEventHandler(this.labQuick_DragDrop);
			// 
			// lst
			// 
			this.lst.Location = new System.Drawing.Point(8, 16);
			this.lst.Name = "lst";
			this.lst.Size = new System.Drawing.Size(120, 251);
			this.lst.TabIndex = 0;
			this.lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);
			// 
			// txNew
			// 
			this.txNew.Location = new System.Drawing.Point(248, 8);
			this.txNew.Name = "txNew";
			this.txNew.TabIndex = 2;
			this.txNew.Text = "";
			this.txNew.TextChanged += new System.EventHandler(this.txNew_TextChanged);
			// 
			// bDelete
			// 
			this.bDelete.Enabled = false;
			this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bDelete.Location = new System.Drawing.Point(136, 8);
			this.bDelete.Name = "bDelete";
			this.bDelete.TabIndex = 3;
			this.bDelete.Text = "Common.Delete";
			this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
			// 
			// bNew
			// 
			this.bNew.Enabled = false;
			this.bNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bNew.Location = new System.Drawing.Point(360, 8);
			this.bNew.Name = "bNew";
			this.bNew.TabIndex = 4;
			this.bNew.Text = "Common.New";
			this.bNew.Click += new System.EventHandler(this.bNew_Click);
			// 
			// RandomTileTemplate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 350);
			this.Controls.Add(this.bNew);
			this.Controls.Add(this.bDelete);
			this.Controls.Add(this.txNew);
			this.Controls.Add(this.grpGroup);
			this.Controls.Add(this.cmbGroups);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RandomTileTemplate";
			this.Text = "Random.EditTile";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.RandomTileTemplate_Closing);
			this.Load += new System.EventHandler(this.RandomTileTemplate_Load);
			this.grpGroup.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void labQuick_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Pen pen = new Pen( SystemColors.ControlDark );
			e.Graphics.DrawRectangle( pen, 0, 0, labQuick.Width - 1, labQuick.Height - 1 );
			pen.Dispose();
		}

		private void labQuick_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			BoxDeco deco = e.Data.GetData( typeof( BoxDeco ) ) as BoxDeco;

			if ( deco != null )
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void txNew_TextChanged(object sender, System.EventArgs e)
		{
			bNew.Enabled = txNew.Text.Length > 0;
		}

		private void RandomTileTemplate_Load(object sender, System.EventArgs e)
		{
			m_List = RandomTiles.Load();

			foreach( RandomTilesList list in m_List.List )
			{
				cmbGroups.Items.Add( list );
			}

			if ( cmbGroups.Items.Count > 0 )
			{
				cmbGroups.SelectedIndex = 0;
			}
		}

		private void bNew_Click(object sender, System.EventArgs e)
		{
			RandomTilesList list = new RandomTilesList( txNew.Text );
			txNew.Text = "";

			m_List.List.Add( list );

			cmbGroups.Items.Add( list );
			cmbGroups.SelectedItem = list;
		}

		private void UpdateTileset()
		{
			lst.BeginUpdate();
			lst.Items.Clear();

			foreach( RandomTile tile in m_TileSet.Tiles )
			{
				lst.Items.Add( tile );
			}

			lst.EndUpdate();

			grpGroup.Text = m_TileSet.Name;
			bDelItem.Enabled = false;
		}

		private void cmbGroups_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TileSet = cmbGroups.SelectedItem as RandomTilesList;
		}

		private void labQuick_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			BoxDeco deco = e.Data.GetData( typeof( BoxDeco ) ) as BoxDeco;

			if ( deco != null )
			{
				RandomTile tile = new RandomTile();

				tile.Items.Add( deco.ID );
				tile.Name = deco.Name;

				m_TileSet.Tiles.Add( tile );
				lst.Items.Add( tile );
			}
		}

		private void txName_TextChanged(object sender, System.EventArgs e)
		{
			bAdd.Enabled = txName.Text.Length > 0 && txItems.Text.Length > 0;
		}

		private void txItems_TextChanged(object sender, System.EventArgs e)
		{
			bAdd.Enabled = txName.Text.Length > 0 && txItems.Text.Length > 0;
		}

		private void bAdd_Click(object sender, System.EventArgs e)
		{
			RandomTile tile = new RandomTile();

			tile.Name = txName.Text;

			foreach( string s in txItems.Lines )
			{
				try
				{
					int id = int.Parse( s );
					tile.Items.Add( id );
				}
				catch {}
			}

			if ( tile.Items.Count == 0 )
			{
				MessageBox.Show( Pandora.TextProvider[ "Random.InvalidIDs" ] );
				return;
			}

			txName.Text = "";
			txItems.Text = "";

			m_TileSet.Tiles.Add( tile );
			lst.Items.Add( tile );
		}

		private void txItems_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			BoxDeco deco = e.Data.GetData( typeof( BoxDeco ) ) as BoxDeco;

			if ( deco != null )
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void txItems_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			BoxDeco deco = e.Data.GetData( typeof( BoxDeco ) ) as BoxDeco;

			if ( deco != null )
			{
				txItems.AppendText( "\r\n" );
				txItems.AppendText( deco.ID.ToString() );
			}
		}

		private void lst_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			bDelItem.Enabled = lst.SelectedItem != null;
		}

		private void bDelItem_Click(object sender, System.EventArgs e)
		{
			RandomTile tile = lst.SelectedItem as RandomTile;

			if ( tile != null )
			{
				m_TileSet.Tiles.Remove( tile );
				UpdateTileset();
			}
		}

		private void bDelete_Click(object sender, System.EventArgs e)
		{
			if ( m_TileSet != null )
			{
				m_List.List.Remove( m_TileSet );

				int index = cmbGroups.Items.IndexOf( m_TileSet );
				cmbGroups.Items.Remove( m_TileSet );

				if ( index < cmbGroups.Items.Count )
				{
					cmbGroups.SelectedIndex = index;
				}
				else
				{
					if ( --index >= 0 && cmbGroups.Items.Count > 0 )
					{
						cmbGroups.SelectedIndex = index;
					}
					else
					{
						TileSet = null;
					}
				}
			}
		}

		private void RandomTileTemplate_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ArrayList purge = new ArrayList();

			// Validate
			foreach( RandomTilesList tiles in m_List.List )
			{
				if ( tiles.Tiles.Count == 0 )
				{
					purge.Add( tiles );
				}
			}

			foreach( RandomTilesList tiles in purge )
			{
				m_List.List.Remove( tiles );
			}

			if ( purge.Count > 0 )
				MessageBox.Show( string.Format( Pandora.TextProvider[ "Random.Purge" ], purge.Count ) );

			m_List.Save();
		}
	}
}