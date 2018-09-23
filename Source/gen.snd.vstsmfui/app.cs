/*
 * User: tfooo
 * Date: 11/12/2005
 * Time: 4:19 PM
 */
 #define MODE1
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Internals;
using System.Windows.Forms;

using modest100.Forms;
using modest100.Internals;

namespace modest100
{
	class app
	{
		static List<MasterViewContainer> ViewCollection;
		/// 1. How are we to handle argument usage if we have no arguments?
		/// 2. We could very well provide changes to the timing configuration here.
		/// 3. Provide external database controls possably in an external thread.
		///    a. Initialize SQLite database if none exists.  For this we will
		///       need a initial table script (sql)
		///    b. 
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			ViewMaster<MasterViewContainer,MidiControlBase> Viewer = new ViewMaster<MasterViewContainer,MidiControlBase>(Assembly.GetExecutingAssembly());
			Application.Run( new ModestForm(Viewer.ViewCollection) );
			Application.Exit();
		}
	}
}
