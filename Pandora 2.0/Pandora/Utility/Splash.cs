using System;
using System.Threading;
using System.Windows.Forms;

using TheBox.Forms;

namespace TheBox.Common
{
	/// <summary>
	/// Provides access to the splash screen
	/// </summary>
	public class Splash
	{
		private static SplashScreen m_Form;
		private static Thread m_Thread;

		private static void ShowThread()
		{
			m_Form = new SplashScreen();
			Application.Run( m_Form	);
		}

		public static void Show()
		{
			if ( m_Thread != null )
			{
				return;
			}

			m_Thread = new Thread( new ThreadStart( ShowThread ) );
			m_Thread.IsBackground = true;
			m_Thread.ApartmentState = ApartmentState.STA;
			m_Thread.Start();
		}

		public static void Close()
		{
			if ( m_Thread == null || m_Form == null )
				return;

			try
			{
				m_Form.Invoke( new MethodInvoker( m_Form.Close ) );
			}
			catch {}

			m_Thread = null;
			m_Form = null;
		}

		public static void SetStatusText( string text )
		{
			if ( m_Form != null )
			{
				m_Form.SetActionText( text );
			}
		}
	}
}
