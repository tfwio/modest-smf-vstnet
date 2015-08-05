/* tfooo 11/12/2005 4:19 PM */
using modest100.Forms;
using modest100.Internals;

namespace modest100.Views
{
	using Keys=System.Windows.Forms.Keys;

  /// <summary>
  /// </summary>
	public class MidiPianoContainer : MasterViewContainer
	{

    /// <summary></summary>
		public override Keys? ShortcutKeys { get { return Keys.F11; } }

    /// <summary></summary>
		public override string Title { get { return "Piano Layout"; } }

    /// <summary></summary>
		public override MidiControlBase GetView() { return new MidiPianoView(); }

    /// <summary></summary>
		public MidiPianoContainer() {}
	}
}
