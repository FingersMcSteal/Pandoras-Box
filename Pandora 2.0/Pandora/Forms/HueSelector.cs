using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using TheBox.Mul;
using TheBox.Data;

namespace TheBox.Forms
{
	/// <summary>
	/// Summary description for HueChart.
	/// </summary>
	public class HueSelector : System.Windows.Forms.Form
	{
		[DllImport( "User32" )] private static extern short GetKeyState( int nVirtKey );

		private static int VK_SHIFT = 0x10;
		private static int VK_CONTROL = 0x11;
		private static int VK_MENU = 0x12;

		private HueGroups m_Groups;

		private ArrayList m_SelectedHues;

		/// <summary>
		/// Gets or sets the hues selected on the chart
		/// </summary>
		public ArrayList SelectedHues
		{
			get
			{
				return m_SelectedHues;
			}
			set
			{
				m_SelectedHues = value;
			}
		}

		private HuesCollection m_SelectedGroup;

		private HuesCollection SelectedGroup
		{
			set
			{
				m_SelectedGroup = value;

				bUpdate.Enabled = m_SelectedGroup != null;
				bDelete.Enabled = m_SelectedGroup != null;

				m_SelectedHues.Clear();
				m_SelectedHues.AddRange( m_SelectedGroup.Hues );

				this.DrawSelectionChart();
			}
		}

		private Bitmap SelectionChart;
		private Bitmap Chart;
		private Bitmap TempBmp;

		private byte GCSteps = 0;
		private int PreviousSelectedColor;
		private int SelectedColor;
		private Hues m_Hues;
		private System.Windows.Forms.PictureBox TheImage;
		private System.Windows.Forms.ComboBox cmbGroups;
		private System.Windows.Forms.Button bUpdate;
		private System.Windows.Forms.TextBox txNew;
		private System.Windows.Forms.Button bNew;
		private System.Windows.Forms.Button bDelete;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.PictureBox imgHue;
		private System.Windows.Forms.Label labName;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HueSelector()
		{
			InitializeComponent();

			SelectedHues = new ArrayList();

			TempBmp = new Bitmap( 450, 300 );

			m_Hues = Hues.Load( Pandora.Profile.MulManager[ "hues.mul" ] );

			DrawHues();

			SelectedHues.Add( 1 );
			DrawSelectionChart();
			SelectedHues.Clear();
			TheImage.Image = Chart;
			SelectionChart = (Bitmap) Chart.Clone();

			Pandora.LocalizeControl( this );
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HueSelector));
			this.TheImage = new System.Windows.Forms.PictureBox();
			this.cmbGroups = new System.Windows.Forms.ComboBox();
			this.bUpdate = new System.Windows.Forms.Button();
			this.txNew = new System.Windows.Forms.TextBox();
			this.bNew = new System.Windows.Forms.Button();
			this.bDelete = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labName = new System.Windows.Forms.Label();
			this.imgHue = new System.Windows.Forms.PictureBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TheImage
			// 
			this.TheImage.Location = new System.Drawing.Point(16, 88);
			this.TheImage.Name = "TheImage";
			this.TheImage.Size = new System.Drawing.Size(450, 300);
			this.TheImage.TabIndex = 0;
			this.TheImage.TabStop = false;
			this.TheImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TheImage_MouseMove);
			this.TheImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TheImage_MouseDown);
			// 
			// cmbGroups
			// 
			this.cmbGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGroups.Location = new System.Drawing.Point(8, 8);
			this.cmbGroups.Name = "cmbGroups";
			this.cmbGroups.Size = new System.Drawing.Size(128, 21);
			this.cmbGroups.TabIndex = 1;
			this.cmbGroups.SelectedIndexChanged += new System.EventHandler(this.cmbGroups_SelectedIndexChanged);
			// 
			// bUpdate
			// 
			this.bUpdate.Enabled = false;
			this.bUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bUpdate.Location = new System.Drawing.Point(144, 8);
			this.bUpdate.Name = "bUpdate";
			this.bUpdate.Size = new System.Drawing.Size(64, 23);
			this.bUpdate.TabIndex = 2;
			this.bUpdate.Text = "Common.Update";
			this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
			// 
			// txNew
			// 
			this.txNew.Location = new System.Drawing.Point(296, 8);
			this.txNew.Name = "txNew";
			this.txNew.Size = new System.Drawing.Size(104, 20);
			this.txNew.TabIndex = 3;
			this.txNew.Text = "";
			this.txNew.TextChanged += new System.EventHandler(this.txNew_TextChanged);
			// 
			// bNew
			// 
			this.bNew.Enabled = false;
			this.bNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bNew.Location = new System.Drawing.Point(408, 8);
			this.bNew.Name = "bNew";
			this.bNew.Size = new System.Drawing.Size(64, 23);
			this.bNew.TabIndex = 4;
			this.bNew.Text = "Common.New";
			this.bNew.Click += new System.EventHandler(this.bNew_Click);
			// 
			// bDelete
			// 
			this.bDelete.Enabled = false;
			this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bDelete.Location = new System.Drawing.Point(216, 8);
			this.bDelete.Name = "bDelete";
			this.bDelete.Size = new System.Drawing.Size(64, 23);
			this.bDelete.TabIndex = 5;
			this.bDelete.Text = "Common.Delete";
			this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labName);
			this.groupBox1.Controls.Add(this.imgHue);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(464, 48);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			// 
			// labName
			// 
			this.labName.Location = new System.Drawing.Point(8, 16);
			this.labName.Name = "labName";
			this.labName.Size = new System.Drawing.Size(152, 23);
			this.labName.TabIndex = 1;
			this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// imgHue
			// 
			this.imgHue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.imgHue.Location = new System.Drawing.Point(168, 16);
			this.imgHue.Name = "imgHue";
			this.imgHue.Size = new System.Drawing.Size(288, 24);
			this.imgHue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.imgHue.TabIndex = 0;
			this.imgHue.TabStop = false;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Window;
			this.textBox1.Location = new System.Drawing.Point(8, 400);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(464, 72);
			this.textBox1.TabIndex = 7;
			this.textBox1.Text = "HuePicker.Instructions";
			// 
			// HueSelector
			// 
			this.AllowDrop = true;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(482, 480);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.bDelete);
			this.Controls.Add(this.bNew);
			this.Controls.Add(this.txNew);
			this.Controls.Add(this.bUpdate);
			this.Controls.Add(this.cmbGroups);
			this.Controls.Add(this.TheImage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "HueSelector";
			this.Text = "HuePicker.Groups";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.HueSelector_Closing);
			this.Load += new System.EventHandler(this.HueSelector_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void DrawHues()
		{
			Chart = new Bitmap( 450, 300 );

			int Brightness = 28;
			int Index = 0;

			foreach ( TheBox.Mul.HueGroup group in m_Hues.Groups )
			{
				foreach ( Hue entry in group.HueList )
				{
					// Draw the box for the hue
					DrawBox( Chart, entry.ColorTable[ Brightness ], Index );
					Index++;
				}
			}

			// Display the chart
			TheImage.Image = Chart;
		}

		private void DrawBox( Bitmap bmp, short Color16, int Index )
		{
			// Calculate the row and column (zero based)
			int column = (int) ( Index / 60 );
			int row = Index % 60;

			// Get the color
			Color color = Hue.ToColor( Color16 );

			// Find the top left corner of the box
			int x = ( column * 9 );
			int y = ( row * 5 );

			// Color
			for ( int iX = 0; iX < 9; iX++ )
				for ( int iY = 0; iY < 5; iY ++ )
					bmp.SetPixel( x + iX, y + iY, color );
		}

		private int GetHueIndex( int x, int y )
		{
			int column = (int) ( x / 9 );
			int row = (int) ( y / 5 );
			return ( column * 60 + row + 1 );
		}

		private void DrawSelectionChart()
		{
			if ( SelectedHues.Count == 0 )
			{
				TheImage.Image = Chart;
				return;
			}

			SelectedHues.Sort();
			SelectionChart = DrawSelection( Chart, SelectedHues, Color.White );
			TheImage.Image = SelectionChart;
		}

		private void TheImage_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Get the hue index ( zero based )
			SelectedColor = GetHueIndex( e.X, e.Y );

			// Handle selection of hues

			if ( ! ( ShiftPressed() || ControlPressed() ) )
			{
				if ( ( SelectedHues.Count == 1 ) && ( SelectedHues.Contains( SelectedColor ) ) )
				{
					SelectedHues.Clear();
					DrawSelectionChart();
					return;
				}
				// No modifier keys pressed. Clear selection and add current item
				SelectedHues.Clear();
				SelectedHues.Add( SelectedColor );
				PreviousSelectedColor = SelectedColor;
				DrawSelectionChart();
				return;
			}

			if ( SelectedHues.Contains( SelectedColor ) )
			{
				// Clicking the same just deselects it
				SelectedHues.Remove( SelectedColor );
				
				if ( ShiftPressed() )
				{
					// Go down
					if ( SelectedHues.Contains( (SelectedColor + 1) ) )
					{
						for ( int i = ( SelectedColor + 1 ); i <= PreviousSelectedColor; i++ )
						{
							if ( SelectedHues.Contains( i ) )
								SelectedHues.Remove( i );
							else
								break;
						}
					}
				}
				PreviousSelectedColor = ( SelectedColor - 1 );
				DrawSelectionChart();
				return;
			}

			if (  ControlPressed() )
			{
				// Add a single item to the list
				SelectedHues.Add( SelectedColor );
				PreviousSelectedColor = SelectedColor;
				DrawSelectionChart();
				return;
			}

			if ( ShiftPressed() )
			{
				// Add a selection of items
				if ( PreviousSelectedColor < SelectedColor )
				{
					// Moving backwards
					for ( int i = SelectedColor; i > PreviousSelectedColor; i-- )
						if ( !SelectedHues.Contains( i ) )
							SelectedHues.Add( i );
				}
				if ( PreviousSelectedColor > SelectedColor )
				{
					// Moving forward
					for ( int i = SelectedColor; i < PreviousSelectedColor; i++ )
						if ( !SelectedHues.Contains( i ) ) // Avoid duplicates
							SelectedHues.Add( i );
				}

				PreviousSelectedColor = SelectedColor;
				DrawSelectionChart();
				return;
			}
		}

		private Bitmap DrawSelection(Bitmap oldImg, ArrayList list, Color color)
		{
			TempBmp = (Bitmap) oldImg.Clone();

			// Performe garbage collection once every 5 steps
			if ( GCSteps == 5 )
			{
				GC.Collect();
				GCSteps = 0;
			}
			else
				GCSteps++;

			// Sort the selected hues
			list.Sort();

			bool Range = false;
			int First = (int) list[0];
			int Last = First;

			foreach ( int s in list )
			{
				if ( First == s )
					continue;

				if ( s == ( Last + 1 ) )
				{
					Range = true;
					Last = s;
					continue;
				}

				if ( Range )
					DrawSelectionBox( TempBmp, ( First - 1 ), ( Last - 1 ) , color );
				else
					DrawSelectionBox( TempBmp, ( First - 1 ) , color );

				Range = false;
				First = s;
				Last = s;				
			}

			if ( Range )
				DrawSelectionBox( TempBmp, ( First - 1 ), (short) (Last - 1 ), color);
			else
				DrawSelectionBox( TempBmp, ( First - 1 ), color );

			return TempBmp;
		}

		private void DrawSelectionBox( Bitmap img, int first, int last, Color color )
		{
			int c1 = (int) ( first / 60 );
			int c2 = (int) ( last / 60 );
			int r1 = first % 60;
			int r2 = last % 60;

			if ( c2 == c1 )
			{
				// Draw a long box
				int x = (c1 * 9 );
				int y1 = ( r1 * 5 );
				int y2 = ( ( r2 * 5 ) + 4 );

				for ( int i = 0; i < 8; i++ )
				{
					img.SetPixel( x + i, y1, color );
					img.SetPixel( x + i, y2, color );
				}

				for ( int i = y1; i <= y2; i++ )
				{
					img.SetPixel( x, i, color );
					img.SetPixel( x + 8, i, color );
				}
			}

			if ( c2 > c1 )
			{
				// Get the two points first
				int x1 = ( c1 * 9 );
				int x2 = ( c2 * 9 );
				int y1 = ( r1 * 5 );
				int y2 = ( r2 * 5 );

				int TopX1 = ( x1 + 9 ); // Can't be invalid
				int TopX2 = ( x2 + 8 );

				// Draw the top segment
				for ( int i = TopX1; i <= TopX2; i++ )
					img.SetPixel( i, 0, color );

				int BottomX1 = x1;
				int BottomX2 = ( x2 - 1 ); // Can't be invalid

				// Draw the bottom segment
				for ( int i = BottomX1; i <= BottomX2; i++ )
					img.SetPixel( i, 299, color );

				// Draw left horizontal segment, height: y1
				for ( int i = BottomX1; i <= TopX1; i++ )
				{
					if ( ( c2 == ( c1+1 ) ) && ( i == TopX1 ) )
						break;
					img.SetPixel( i, y1, color );
				}

				// Draw right horizontal segment, height: y2 + 5;
				y2 += 4;
				for ( int i = BottomX2; i <= TopX2; i++ )
				{
					if ( ( c2 == ( c1+1) ) && ( i == BottomX2 ) )
						continue;
					img.SetPixel( i, y2, color );
				}

				// Draw left segment
				for ( int i = y1; i <= 299; i++ )
					img.SetPixel( BottomX1, i, color );

				int my = 0;

				if ( c2 == ( c1+1 ) )
					my = Math.Min( y1, y2 );
				else
					my = y1;

				// Draw top left segment
				for ( int i = 0; i <= my; i++ )
					img.SetPixel( TopX1, i, color );

				// Draw right segment
				for ( int i = 0; i <= y2; i++ )
					img.SetPixel( TopX2, i, color );

				if ( c2 == ( c1+1 ) )
					my = Math.Max( y1, y2 );
				else
					my = y2;

				// Draw bottom right segment
				for ( int i = my; i <= 299; i++ )
					img.SetPixel( BottomX2, i, color );				
			}			
		}

		private void DrawSelectionBox( Bitmap img, int index, Color color )
		{
			// Draw a single box around a single hue
			// Calculate the row and column (zero based)
			int column = (int) ( index / 60 );
			int row = index % 60;

			// Find the top left corner of the box
			int x = ( column * 9 );
			int y = ( row * 5 );

			// Draw the box
			for ( int iX = 0; iX < 9; iX++ )
			{
				img.SetPixel( x + iX, y, color );
				img.SetPixel( x + iX, y + 5 - 1, color );
			}
			for ( int iY = 0; iY < 5; iY ++ )
			{
				img.SetPixel( x, y + iY, color );
				img.SetPixel( x + 9 - 1, y + iY, color );
			}
		}

		private int GetGroupNumber( int index )
		{
			return (int) ( ( index - 1 ) / 8 );
		}

		private int GetEntryNumber( int index )
		{
			return (int) (  ( index - 1 ) % 8 );
		}

		private Hue GetHue( int index )
		{
			return m_Hues[ index ];
		}

		private bool ShiftPressed()
		{
			short shift = GetKeyState( VK_SHIFT );
			if ( shift < -100 )
				return true;
			else
				return false;			
		}

		private bool ControlPressed()
		{
			short control = GetKeyState( VK_CONTROL );
			if ( control < -100 )
				return true;
			else
				return false;
		}

		private bool AltPressed()
		{
			short alt = GetKeyState( VK_MENU );
			if ( alt < -100 )
				return true;
			else
				return false;
		}

		private Hue m_Hue;

		private void PreviewHue( int x, int y )
		{
			Hue hue = m_Hues[ this.GetHueIndex( x, y ) ];

			if ( hue != m_Hue )
			{
				m_Hue = hue;

				labName.Text = hue.Name;

				imgHue.Image = hue.GetSpectrum( imgHue.Size );

				this.GCSteps++;

				if ( GCSteps == 10 )
				{
					GCSteps = 0;
					GC.Collect();
				}
			}
		}

		private void TheImage_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			PreviewHue( e.X, e.Y );

			if ( e.Button == MouseButtons.Left )
			{
				// If Alt is pressed, don't select
				if ( AltPressed() )
					return;

				// Get the hue index ( zero based )
				int column = (int) ( e.X / 9 );
				int row = (int) ( e.Y / 5 );
				int selHue = (int) ( column * 60 + row + 1 );

				if ( ( column >= 0 ) && ( column <= 49 ) && ( row >= 0 ) && ( row <= 59 ) )
				{
					if ( !SelectedHues.Contains( selHue ) )
					{
						SelectedHues.Add( selHue );
						PreviousSelectedColor = selHue;
						DrawSelectionChart();
					}
				}
			}
		}

		public void ClearSelection()
		{
			this.SelectedHues.Clear();
			SelectionChart = (Bitmap) Chart.Clone();
			this.TheImage.Image = Chart;
		}

		private void UpdateDisplay()
		{
			DrawHues();
			SelectionChart = (Bitmap) Chart.Clone();
			DrawSelectionChart();
		}

		private void txNew_TextChanged(object sender, System.EventArgs e)
		{
			bNew.Enabled = txNew.Text.Length > 0;
		}

		private void bNew_Click(object sender, System.EventArgs e)
		{
			HuesCollection c = new HuesCollection();

			c.Name = txNew.Text;
			txNew.Text = "";

			c.Hues.Add( 1 );

			m_Groups.Groups.Add( c );

			cmbGroups.Items.Add( c );
			cmbGroups.SelectedItem = c;
		}

		private void bUpdate_Click(object sender, System.EventArgs e)
		{
			m_SelectedGroup.Hues.Clear();
			m_SelectedGroup.Hues.AddRange( m_SelectedHues );
		}

		private void cmbGroups_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SelectedGroup = cmbGroups.SelectedItem as HuesCollection;
		}

		private void HueSelector_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			m_Groups.Save();
		}

		private void HueSelector_Load(object sender, System.EventArgs e)
		{
			m_Groups = HueGroups.Load();

			foreach( HuesCollection c in m_Groups.Groups )
			{
				cmbGroups.Items.Add( c );
			}

			if ( cmbGroups.Items.Count > 0 )
				cmbGroups.SelectedIndex = 0;
		}

		private void bDelete_Click(object sender, System.EventArgs e)
		{
			if ( MessageBox.Show( this, "", Pandora.TextProvider[ "HuePicker.DeleteGroup" ],
				MessageBoxButtons.YesNo ) == DialogResult.Yes )
			{
				m_Groups.Groups.Remove( m_SelectedGroup );

				int index = cmbGroups.Items.IndexOf( m_SelectedGroup );

				cmbGroups.Items.Remove( m_SelectedGroup );

				if ( index < cmbGroups.Items.Count )
				{
					cmbGroups.SelectedIndex = index;
				}
				else if ( --index >= 0 && cmbGroups.Items.Count > 0 )
				{
					cmbGroups.SelectedIndex = index;
				}
			}
		}
	}
}