using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using TheBox.UltimaImport;
using TheBox.Common;

namespace TheBox.ArtViewer
{
	/// <summary>
	/// Art types that the ArtViewer control can display
	/// </summary>
	public enum Art
	{
		/// <summary>
		/// Static items
		/// </summary>
		Items = 0,
		/// <summary>
		/// Characters ( animated or not )
		/// </summary>
		NPCs = 1,
		/// <summary>
		/// Gumps
		/// </summary>
		Gumps = 2,
		/// <summary>
		/// Images
		/// </summary>
		Images = 3
	}

	/// <summary>
	/// Viewer for the Ultima Online art
	/// </summary>
	public class ArtViewer : System.Windows.Forms.Control
	{
		#region events

		/// <summary>
		/// Occurs when the index of the displayed art is changed
		/// </summary>
		public event EventHandler ArtIndexChanged;

		#endregion

		#region Variables

		/// <summary>
		/// The art type currently displayed
		/// </summary>
		private Art m_Art = Art.Items;

		/// <summary>
		/// The index of the art to display
		/// </summary>
		private int m_ArtIndex = 0;

		/// <summary>
		/// Animate characters or not
		/// </summary>
		private bool m_Animate;

		/// <summary>
		/// The hue for the current item
		/// </summary>
		private int m_Hue;

		/// <summary>
		/// Resize images that are larger than the control area
		/// </summary>
		private bool m_ResizeTallItems = false;

		/// <summary>
		/// Display the axes when showing items
		/// </summary>
		private bool m_RoomView = true;

		/// <summary>
		/// The image currently displayed
		/// </summary>
		private Bitmap m_Image;

		/// <summary>
		/// Image is invalidated and needs to be redrawn
		/// </summary>
		private bool m_ImageInvalidated = true;

		/// <summary>
		/// The bitmaps used for the animation
		/// </summary>
		private Bitmap[] m_Animation;

		/// <summary>
		/// The timer for the animation
		/// </summary>
		private Timer m_Timer;

		/// <summary>
		/// Current animation index
		/// </summary>
		private int m_FrameIndex;

		/// <summary>
		/// Shows the ID in the lower right corner
		/// </summary>
		private bool m_ShowID = true;

		/// <summary>
		/// Shows the ID in hex format in the lower left corner
		/// </summary>
		private bool m_ShowHexID = true;

		#endregion

		#region Properties

		/// <summary>
		/// Sets the image that should be displayed by the viewer
		/// </summary>
		public Image DisplayedImage
		{
			set
			{
				if ( value != null )
				{
					StopAnimation();

					if ( m_Image != null )
						m_Image.Dispose();

					m_Image = new Bitmap( value );

					m_Art = Art.Images;

					m_ImageInvalidated = false;
					Refresh();
				}
			}
		}

		/// <summary>
		/// Gets or sets the art type displayed by the control
		/// </summary>
		public Art Art
		{
			get { return m_Art; }
			set
			{
				if ( m_Art != value )
				{
					if ( value == Art.Images )
						return;

					m_Art = value;

					StopAnimation();
					m_ImageInvalidated = true;
					Refresh();
				}
			}
		}

		/// <summary>
		/// Gets or sets the index of the currently displayed art
		/// </summary>
		public int ArtIndex
		{
			get { return m_ArtIndex; }
			set
			{
				if ( value != m_ArtIndex )
				{
					m_ArtIndex = value;

					StopAnimation();
					m_ImageInvalidated = true;
					Refresh();

					if ( ArtIndexChanged != null )
					{
						ArtIndexChanged( this, new EventArgs() );
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets a value stating whether the character display is going to be animated
		/// </summary>
		public bool Animate
		{
			get
			{
				return m_Animate;
			}
			set
			{
				if ( m_Animate != value )
				{
					m_Animate = value;

					if ( m_Art == Art.NPCs )
					{
						StopAnimation();
						m_ImageInvalidated = true;
						Refresh();
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the hue for the current image
		/// </summary>
		public int Hue
		{
			get
			{
				return m_Hue;
			}
			set
			{
				if ( m_Hue == value )
					return;

				m_Hue = value;

				if ( m_Hue < 0 )
					m_Hue = 0;

				if ( m_Hue > 2999 )
					m_Hue = 2999;

				StopAnimation();
				m_ImageInvalidated = true;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets a value stating whether items larger than the display area should be resized
		/// </summary>
		public bool ResizeTallItems
		{
			get
			{
				return m_ResizeTallItems;
			}
			set
			{
				if ( m_ResizeTallItems != value )
				{
					m_ResizeTallItems = value;

					Refresh();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value stating whether the display of static items will show the 
		/// </summary>
		public bool RoomView
		{
			get { return m_RoomView; }
			set
			{
				if ( m_RoomView != value )
				{
					m_RoomView = value;

					Refresh();
				}
			}
		}

		public bool ShowID
		{
			get { return m_ShowID; }
			set
			{
				if ( m_ShowID != value )
				{
					m_ShowID = value;

					Refresh();
				}
			}
		}

		public bool ShowHexID
		{
			get { return m_ShowHexID; }
			set
			{
				if ( m_ShowHexID != value )
				{
					m_ShowHexID = value;
					Refresh();
				}
			}
		}

		#endregion

		#region Mul Manager

		private static MulManager m_MulManager;

		/// <summary>
		/// Gets or sets the mul manager
		/// </summary>
		public static MulManager MulManager
		{
			get
			{
				if ( m_MulManager == null )
				{
					m_MulManager = new MulManager();
				}
				
				return m_MulManager;
			}
			set
			{
				if ( value != null )
				{
					m_MulManager = value;
				}
				else
				{
					m_MulManager = new MulManager();
				}
			}
		}

		/// <summary>
		/// Sets the file manager to use for this art viewer
		/// </summary>
		public MulManager MulFileManager
		{
			set
			{
				m_MulManager = value;
			}
		}

		#endregion

		/// <summary>
		/// Viewer for the Ultima Online art
		/// </summary>
		public ArtViewer()
		{
			// Flickering fix
			SetStyle( ControlStyles.DoubleBuffer, true );
			SetStyle( ControlStyles.UserPaint, true );
			SetStyle( ControlStyles.AllPaintingInWmPaint, true );
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );

			if ( m_Image != null )
				m_Image.Dispose();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);

			if ( m_ImageInvalidated )
				CreateDisplay();

			if ( m_Image != null )
			{
				// Position image
				Point location = Point.Empty;
				Size size = Size.Empty;

				if ( m_Image.Width <= Width && m_Image.Height <= Height )
				{
					// Image fits
					size = m_Image.Size;
					location.X = ( Width - m_Image.Width ) / 2;
					location.Y = ( Height - m_Image.Height ) / 2;
				}
				else
				{
					if ( m_ResizeTallItems )
					{
						size = m_Image.Size;

						double rate = (double) m_Image.Width / (double) m_Image.Height;

						// Resize
						if ( size.Width > Width )
						{
							size.Width = Width;
							size.Height = (int) ( size.Width / rate );
						}

						if ( size.Height > Height )
						{
							size.Height = Height;
							size.Width = (int) ( size.Height * rate );
						}

						location.X = ( Width - size.Width ) / 2;
						location.Y = ( Height - size.Height ) / 2;
					}
					else
					{
						location.X = ( Width - m_Image.Width ) / 2;
						location.Y = ( Height - m_Image.Height ) / 2;
						size = m_Image.Size;
					}
				}

				Rectangle destRect = new Rectangle( location, size );

				if ( m_Art == Art.Items && m_RoomView )
				{
					DrawRoomView( e.Graphics, destRect );
				}

				e.Graphics.DrawImage( m_Image, destRect, 0, 0, m_Image.Width, m_Image.Height, System.Drawing.GraphicsUnit.Pixel );
			}
			else
			{
				if ( m_Art == Art.Items && m_RoomView )
					DrawRoomView( e.Graphics, new Rectangle( ( Width - 44 ) / 2, ( Height - 44 ) / 2, 44, 44 ) );
			}

			// ID
			Pen black = new Pen( SystemColors.ControlDark );

			if ( ( m_ShowID || m_ShowHexID ) && m_Art != Art.Images )
			{
				Brush back = new SolidBrush( SystemColors.Window );
				Brush text = new SolidBrush( SystemColors.WindowText );
			
				Font font = new Font( "Verdana", 8.0f );
				string id = m_ArtIndex.ToString();
				string hex = m_ArtIndex.ToString( "X" );

				if ( m_ShowID )
				{
					SizeF siz = e.Graphics.MeasureString( id, font );

					Size s = siz.ToSize();
					s.Width += 4;
					siz.Height += 4;

					int x = Width - s.Width - 4;
					int y = Height - s.Height - 4;

					RectangleF r = new RectangleF( x + 2, y, siz.Width, siz.Height );

					e.Graphics.FillRectangle( back, x, y, s.Width, s.Height );
					e.Graphics.DrawRectangle( black, x, y, s.Width, s.Height );
					e.Graphics.DrawString( id, font, text, r );
				}

				if ( m_ShowHexID )
				{
					SizeF siz = e.Graphics.MeasureString( hex, font );

					Size s = siz.ToSize();
					s.Width += 4;
					siz.Height += 4;

					int y = Height - s.Height - 4;

					RectangleF r = new RectangleF( 6, y, siz.Width, siz.Height );

					e.Graphics.FillRectangle( back, 4, y, s.Width, s.Height );
					e.Graphics.DrawRectangle( black, 4, y, s.Width, s.Height );
					e.Graphics.DrawString( hex, font, text, r );
				}

				text.Dispose();
				back.Dispose();
				font.Dispose();
			}

			// Border
			
			e.Graphics.DrawRectangle( black, 0,0,Width-1,Height-1 );
			black.Dispose();
		}

		#region Room View

		/// <summary>
		/// Draws the room view
		/// </summary>
		/// <param name="g">The graphics used to draw the room view</param>
		/// <param name="imgRect">The rectangle the image will occupy on the display</param>
		private void DrawRoomView( Graphics g, Rectangle imgRect )
		{
			Point bottom = Point.Empty;
			Point top = Point.Empty;
			Point left = Point.Empty;
			Point right = Point.Empty;

			bottom.X = imgRect.X + ( imgRect.Width / 2 );
			bottom.Y = imgRect.Bottom;

			top.X = bottom.X;
			top.Y = bottom.Y - imgRect.Width;

			left.X = imgRect.X;
			left.Y = imgRect.Bottom - ( imgRect.Width / 2 );

			right.X = imgRect.Right;
			right.Y = left.Y;

			// top points
			Point top1 = Point.Empty;
			Point top2 = Point.Empty;
			Point top3 = Point.Empty;

			top1.Y = top2.Y = top3.Y = 0;
			top1.X = left.X;
			top2.X = top.X;
			top3.X = right.X;

			Pen blackPen = new Pen( Color.Black );
			Brush whiteBrush = new System.Drawing.Drawing2D.LinearGradientBrush( top, right, Color.DarkSlateBlue, Color.SkyBlue );

			// Draw base block
			Point[] baseBlock = new Point[] { top, right, bottom, left, top };
			g.FillPolygon( whiteBrush, baseBlock );

			// Fill
			Point[] leftArea = new Point[] { left, top1, top2, top, left };
			Point[] rightArea = new Point[] { top, top2, top3, right, top };

			Brush darkBrush = new System.Drawing.Drawing2D.LinearGradientBrush( left, top1, Color.MidnightBlue, Color.LightGray);
			Brush lightBrush = new System.Drawing.Drawing2D.LinearGradientBrush( right, top3, Color.SteelBlue, Color.White );

			g.FillPolygon( darkBrush, leftArea );
			g.FillPolygon( lightBrush, rightArea );

			// Draw base
			g.DrawLines( blackPen, baseBlock );

			// Draw vertical lines
			g.DrawLine( blackPen, left, top1 );
			g.DrawLine( blackPen, top, top2 );
			g.DrawLine( blackPen, right, top3 );

			// Draw top line
			g.DrawLine( blackPen, top1, top3 );

			// Draw axes
			g.DrawLine( blackPen, right, GetIntersection( bottom, right ) );
			g.DrawLine( blackPen, right, GetIntersection( top, right ) );
			g.DrawLine( blackPen, left, GetIntersection( bottom, left ) );
			g.DrawLine( blackPen, left, GetIntersection( top, left ) );

			blackPen.Dispose();
			whiteBrush.Dispose();
			darkBrush.Dispose();
			lightBrush.Dispose();
		}

		/// <summary>
		/// Gets the intersection point with the control bounds following a line from p2
		/// </summary>
		/// <param name="p1">the starting point of the line</param>
		/// <param name="p2">the middle point of the line, used to define it</param>
		/// <returns>A point on the bounds of the control</returns>
		private Point GetIntersection( Point p1, Point p2 )
		{
			double a = ( (double) ( p1.Y - p2.Y ) / (double) ( p1.X - p2.X ) );
			double b = (double) p1.Y - ( a * (double) p1.X );

			if ( p1.X < p2.X )
			{
				// Right side
				Point pRight = Point.Empty;

				pRight.X = Width;
				pRight.Y = (int) ( (double) Width * a + b );

				if ( pRight.Y < 0 )
				{
					pRight.Y = 0;
					pRight.X = (int) -( b / a );
				}

				return pRight;
			}
			else
			{
				Point pLeft = Point.Empty;

				if ( b < 0 )
				{
					pLeft.Y = 0;
					pLeft.X = (int) ( - b / a );
				}
				else
				{
					pLeft.Y = (int) b;
					pLeft.X = 0;
				}

				return pLeft;
			}
		}

		#endregion

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);

			Refresh();
		}

		/// <summary>
		/// Calculates the image displayed by the control
		/// </summary>
		private void CreateDisplay()
		{
			if ( m_Image != null )
			{
				m_Image.Dispose();
			}

			switch ( m_Art)
			{
				case Art.Items:

					try { m_Image = new Bitmap( TheBox.UltimaImport.Art.GetStatic( m_ArtIndex ) ); }
					catch { m_Image = null; }

					break;

				case Art.Gumps:

					try { m_Image = new Bitmap( Gumps.GetGump( m_ArtIndex ) ); }
					catch { m_Image = null; }

					break;

				case Art.NPCs:

					if ( m_Animate )
					{
						m_Image = DoAnimation();
					}
					else
					{
						Frame[] frames = Animations.GetAnimation( m_ArtIndex, 0, 1, 0, true );

						if ( frames != null )
							m_Image = new Bitmap( frames[ 0 ].Bitmap );
						else
							m_Image = null;
					}

					break;
			}

			// Hue

			if ( m_Hue != 0 && m_Image != null && m_Art != Art.Images )
			{
				if ( m_Art != Art.NPCs || m_Art == Art.NPCs && ! m_Animate )
				{
					// Use m_Hue - 1 because hues are zero based, while the game considers them 1-based
					TheBox.UltimaImport.Hue.ApplyHueTo( m_Hue - 1, m_Image, false );
				}
			}
		}

		/// <summary>
		/// Sets up the animation 
		/// </summary>
		/// <returns>The current image</returns>
		private Bitmap DoAnimation()
		{
			if ( m_Timer == null )
			{
				// Create the animation
				Frame[] frames = Animations.GetAnimation( m_ArtIndex, 0, 1, m_Hue, false );

				if ( frames == null )
					return null;

				int count = frames.Length;
				m_Animation = new Bitmap[ count ];

				for ( int i = 0; i < count; i++ )
				{
					m_Animation[ i ] = frames[ i ].Bitmap;
				}

				m_Timer = new Timer();
				m_Timer.Interval = 1000 / count;
				m_Timer.Tick += new EventHandler( AnimTick );
				m_Timer.Start();

				m_FrameIndex = 0;
				return new Bitmap( m_Animation[ 0 ] );
			}
			else
			{
				return new Bitmap( m_Animation[ m_FrameIndex ] );
			}
		}

		private void AnimTick(object sender, EventArgs e)
		{
			m_FrameIndex++;
			
			if ( m_FrameIndex == m_Animation.Length )
				m_FrameIndex = 0;

			m_ImageInvalidated = true;
			Refresh();
		}

		/// <summary>
		///  Stops the current animation
		/// </summary>
		private void StopAnimation()
		{
			if ( m_Timer != null )
			{
				if ( m_Timer.Enabled )
					m_Timer.Stop();

				m_Timer.Dispose();
				m_Timer = null;
			}

			if ( m_Animation != null )
			{
				for ( int i = 0; i < m_Animation.Length; i++ )
					if ( m_Animation[i] != null )
						m_Animation[i].Dispose();
			}

			m_Animation = null;
			m_FrameIndex = 0;
		}
	}
}