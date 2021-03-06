using System;
using System.Windows.Forms;
using System.Collections;

namespace TheBox.Common
{
	/// <summary>
	/// Provides easy access to search results over TreeViews
	/// </summary>
	public class SearchResults
	{
		private ArrayList m_Results;
		private int m_Index = 0;

		/// <summary>
		/// Creates a new SearchResults object
		/// </summary>
		public SearchResults()
		{
			m_Results = new ArrayList();
		}

		/// <summary>
		/// Adds a new item to the results list
		/// </summary>
		/// <param name="result">The Result object being added to the results list</param>
		public void Add( Result result )
		{
			m_Results.Add( result );
		}

		/// <summary>
		/// Gets the number of results found by the search
		/// </summary>
		public int Count
		{
			get { return m_Results.Count; }
		}

		/// <summary>
		/// Gets the next result in the list
		/// </summary>
		/// <returns>The Result object corresponding to the next result in the list. Null if no results are in the list, or if the end of the list has been reached.</returns>
		public Result GetNext()
		{
			if ( m_Index == m_Results.Count )
			{
				return null;
			}

			return m_Results[ m_Index++ ] as Result;
		}

		/// <summary>
		/// Getst the previous result in the list
		/// </summary>
		/// <returns>The Result object corresponding to the previous result in the list. Null if the current is the first item.</returns>
		public Result GetPrevious()
		{
			if ( m_Index == 0 )
			{
				return null;
			}

			return m_Results[ --m_Index ] as Result;
		}

		/// <summary>
		/// Merges the results provided by a second search results
		/// </summary>
		/// <param name="moreResults"></param>
		public void MergeWith( SearchResults moreResults )
		{
			this.m_Results.AddRange( moreResults.m_Results );
		}
	}

	/// <summary>
	/// Defines a single search result
	/// </summary>
	public class Result : IComparable
	{
		/// <summary>
		/// Creates a Result object
		/// </summary>
		/// <param name="node">The TreeNode containing the result</param>
		/// <param name="index">The index of the </param>
		public Result( TreeNode node, int index )
		{
			m_Node = node;
			m_Index = index;
		}

		private TreeNode m_Node;
		private int m_Index;

		/// <summary>
		/// Gets or sets the category node for this item
		/// </summary>
		public TreeNode Node
		{
			get { return m_Node; }
			set { m_Node = value; }
		}

		/// <summary>
		/// Gets or sets the index for the element to be displayed on the second treenode
		/// </summary>
		public int Index
		{
			get { return m_Index; }
			set { m_Index = value; }
		}

		#region IComparable Members

		/// <summary>
		/// Compares this result to another
		/// </summary>
		/// <param name="obj">The Result to compare to</param>
		/// <returns>The comparison result</returns>
		public int CompareTo(object obj)
		{
			if ( obj is Result )
			{
				Result cmp = obj as Result;

				if ( cmp.Node.FullPath.ToLower() == this.Node.FullPath.ToLower() )
				{
					IComparable one = ( (ArrayList) this.Node.Tag )[ this.m_Index ] as IComparable;
					IComparable two = ( (ArrayList) cmp.Node.Tag )[ cmp.m_Index ] as IComparable;

					return one.CompareTo( two );
				}
				else
				{
					return this.Node.FullPath.CompareTo( cmp.Node.FullPath );
				}
			}
			else
			{
				throw new Exception( string.Format( "Cannot compare Result to {0}", obj.GetType().Name ) );
			}
		}

		#endregion
	}
}
