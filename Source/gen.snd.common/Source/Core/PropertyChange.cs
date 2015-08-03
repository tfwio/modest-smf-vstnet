/*
 * Date: 11/12/2005 --- Time: 4:19 PM
 */
using System;
using System.ComponentModel;
using System.Linq;

namespace gen.snd.Core
{
	public class PropertyChange : INotifyPropertyChanged
	{
		protected virtual void OnPropertyChanged(string property)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(property));
		}

		#region INotifyPropertyChanged implementation
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		#endregion
	}
}


