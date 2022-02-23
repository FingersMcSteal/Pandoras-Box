using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

using TheBox.Common;

namespace TheBox.Buttons
{
	[ Serializable ]
	/// <summary>
	/// Describes a menu used in custom buttons in Pandora's Box
	/// </summary>
	public class MenuDef : IButtonFunction, IDisposable, ICloneable
	{
		private ArrayList m_Items;
		private ContextMenu m_Menu;
		/// <summary>
		/// Gets or sets the collection defining this menu
		/// </summary>
		public ArrayList Items
		{
			get { return m_Items; }
			set { m_Items = value; }
		}

		/// <summary>
		/// Gets the context meny generated by this MenuDef object
		/// </summary>
		public ContextMenu Menu
		{
			get
			{
				if ( m_Menu == null )
				{
					m_Menu = new ContextMenu();
					m_Menu.MenuItems.AddRange( GetItems( m_Items ) );
				}

				return m_Menu;
			}
		}

		/// <summary>
		/// Creates a structure defining a box custom menu
		/// </summary>
		public MenuDef()
		{
			m_Items = new ArrayList();
		}

		/// <summary>
		/// Resets the context menu to zero so that it gets computed again next time when needed
		/// </summary>
		public void ResetMenu()
		{
			m_Menu.Dispose();
			m_Menu = null;
		}

		/// <summary>
		/// Computes menu items corresponding to a given list of definitions
		/// </summary>
		/// <param name="list">ArrayList containing the elements to be transformed into the menu items</param>
		/// <returns>A list of menu items resulting from the array list</returns>
		private MenuItem[] GetItems( ArrayList list )
		{
			MenuItem[] items = new MenuItem[ list.Count ];

			for ( int i = 0; i < items.Length; i++ )
			{
				object o = list[ i ];

				if ( o is MenuCommand )
				{
					items[ i ] = new BoxMenuItem( o as MenuCommand );
					( items[ i ] as BoxMenuItem ).SendCommand += new SendCommandEventHandler(MenuDef_SendCommand);
				}
				else if ( o is GenericNode )
				{
					GenericNode node = o as GenericNode;

					items[ i ] = new MenuItem( node.Name );
					items[ i ].MenuItems.AddRange( GetItems( node.Elements ) );
				}
			}

			return items;
		}

		/// <summary>
		/// Occurs when a command is being sent to UO
		/// </summary>
		public event SendCommandEventHandler SendCommand;

		protected virtual void OnSendCommand( SendCommandEventArgs e )
		{
			if ( SendCommand != null )
				SendCommand( this, e );
		}

		/// <summary>
		/// Sending a command to UO
		/// </summary>
		private void MenuDef_SendCommand(object sender, SendCommandEventArgs e)
		{
			OnSendCommand( e );
		}

		#region IButtonFunction Members

		public string Name
		{
			get { return "ButtonMenuEditor.Menu"; }
		}

		/// <summary>
		/// States whether this action allows another action to be configured on the other button
		/// </summary>
		public bool AllowsSecondButton
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// States whether this action requires another action configured on the other button
		/// </summary>
		public bool RequiresSecondButton
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Does the action specified by the function
		/// </summary>
		/// <param name="button">The button owner of the action</param>
		/// <param name="clickPoint">The location of the user click on the button</param>
		/// <param name="mouseButton">The mouse button clicked</param>
		public void DoAction(BoxButton button, Point clickPoint, System.Windows.Forms.MouseButtons mouseButton)
		{
			Menu.Show( button, clickPoint );
		}

		/// <summary>
		/// Not used in this class
		/// </summary>
		public event System.EventHandler SendLastCommand;

		protected virtual void OnSendLastCommand( EventArgs e )
		{
			if ( SendLastCommand != null )
			{
				SendLastCommand( this, e );
			}
		}

		/// <summary>
		/// Not used in this class
		/// </summary>
		public event TheBox.CommandChangedEventHandler CommandChanged;

		protected virtual void OnCommandChanged( CommandChangedEventArgs e )
		{
			if ( CommandChanged != null )
			{
				CommandChanged( this, e );
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if ( m_Menu != null )
			{
				m_Menu.Dispose();
			}
		}

		#endregion

		#region ICloneable Members

		public object Clone()
		{
			MenuDef def = new MenuDef();
			def.m_Items = CloneItems( m_Items );

			return def;
		}

		private ArrayList CloneItems( ArrayList items )
		{
			ArrayList list = new ArrayList();

			foreach ( object o in items )
			{
				if ( o is MenuCommand )
				{
					list.Add( ( o as MenuCommand ).Clone() );
				}
				else if ( o is GenericNode )
				{
					GenericNode gn = new GenericNode( ( o as GenericNode ).Name );

					gn.Elements.AddRange( CloneItems( ( o as GenericNode ).Elements ) );

					list.Add( gn );
				}
			}

			return list;
		}

		#endregion
	}
}
