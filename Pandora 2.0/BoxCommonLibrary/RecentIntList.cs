using System;
using System.Collections;

namespace TheBox.Common
{
	/// <summary>
	/// Listing of integers that updates accordingly to usage
	/// </summary>
	[ Serializable ]
	public class RecentIntList
	{
		private int m_Capacity;
		private ArrayList m_List;

		/// <summary>
		/// Gets or sets the capacity of the list
		/// </summary>
		[ System.Xml.Serialization.XmlAttribute ]
		public int Capacity
		{
			get { return m_Capacity; }
			set
			{
				m_Capacity = value;

				if ( m_Capacity < 1 )
					m_Capacity = 1;

				while ( m_List.Count > m_Capacity )
					m_List.RemoveAt( m_List.Count - 1 );
			}
		}

		/// <summary>
		/// Gets or sets the list of items
		/// </summary>
		public ArrayList List
		{
			get { return m_List; }
			set { m_List = value; }
		}

		/// <summary>
		/// Creates a new list of integers with recent behaviour
		/// </summary>
		public RecentIntList()
		{
			m_List = new ArrayList();
			m_Capacity = 10;
		}

		/// <summary>
		/// Gets the item at a given location in the list
		/// </summary>
		public int this[int index]
		{
			get
			{
				if ( index < m_List.Count )
					return (int) m_List[ index ];
				else
					return 0;
			}
		}

		/// <summary>
		/// Adds a new value to the list
		/// </summary>
		/// <param name="value">The integer to add</param>
		public void Add( int value )
		{
			if ( m_List.Count < m_Capacity )
			{
				if ( m_List.Contains( value ) )
					m_List.Remove( value );
				
				m_List.Insert( 0, value );
			}
			else
			{
				if ( m_List.Contains( value ) )
				{
					m_List.Remove( value );
				}
				else
				{
					m_List.RemoveAt( m_List.Count - 1 );
				}

				m_List.Insert( 0, value );
			}

			OnListChanged( new EventArgs() );
		}

		/// <summary>
		/// Occurs when the list has been changed
		/// </summary>
		public event EventHandler ListChanged;

		/// <summary>
		/// Fire the ListChanged event
		/// </summary>
		protected virtual void OnListChanged( EventArgs e )
		{
			if ( ListChanged != null )
			{
				ListChanged( this, e );
			}
		}
	}
}
