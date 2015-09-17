/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 11/12/2005
 * Time: 4:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Windows.Forms;

using gen.snd;
using gen.snd.Forms;
using gen.snd.Midi;

namespace modest100.Forms
{
  public class MidiNoteView : MidiControlBase
  {
    // Used to calculate MBT to sample-based TimeSpan calculations.
    SampleClock timing = new SampleClock();
    const int samplerate = 44100;
    SampleClock SampleInfo;
    
    #region Registry
    
    const string regpath = @"Software\tfoxo\midi";
    const string reg_list_columns 	= "NoteView.Cols[]";
    const string reg_list_fontsize 	= "EventList2.FontSize";
    
    public string ListColumns {
      get { return Reg.GetKeyValueString(regpath,reg_list_columns); }
      set { Reg.SetKeyValueString(regpath,reg_list_columns,value); }
    }
    public string ListFontSize {
      get { return Reg.GetKeyValueString(regpath,reg_list_fontsize); }
      set { Reg.SetKeyValueString(regpath,reg_list_fontsize,value); }
    }
    
    /// <summary>
    /// Get: Returns integer values from ListView.
    /// <para>set: Sets values to the list view.</para>
    /// </summary>
    int[] ListColSize
    {
      get
      {
        var cols = new int[listNotes.Columns.Count];
        for (int i =0;i < listNotes.Columns.Count; i++) cols[i] = listNotes.Columns[i].Width;
        return cols;
      }
      set { for (int i =0;i < listNotes.Columns.Count; i++) listNotes.Columns[i].Width = value[i]; }
    }
    
    internal void FontLarger(object sender, EventArgs e)
    {
      Reg.SetControlFontSize(listNotes,12);
      ListFontSize = "12";
    }
    internal void FontSmaller(object sender, EventArgs e)
    {
      Reg.SetControlFontSize(listNotes,9);
      ListFontSize = "9";
    }
    
    public override void ApplyRegistrySettings()
    {
      Debug.Print("Loading Settings for NoteView");
      try
      {
        if (!string.IsNullOrEmpty(ListFontSize)) {
          Reg.SetControlFontSize(listNotes,float.Parse(ListFontSize));
        }
      }
      catch {
        Debug.Print("Error updating font size (this.NoteView)");
      }
      if (!string.IsNullOrEmpty(ListColumns))
      {
        Debug.Print("Notes Columns: {0}",ListColumns);
        int[] vals = Reg.TranslateString(ListColumns);
        if (vals!=null) ListColSize = vals;
        vals = null;
      }
      Debug.Print("Adding Col-Resized Event");
      listNotes.ColumnWidthChanged -= LoadSettings ;
      listNotes.ColumnWidthChanged += LoadSettings ;
    }
    #endregion
    
    void LoadSettings(object o, EventArgs e)
    {
      BeforeUnload();
    }
    

    public MidiNoteView() : base()
    {
      this.InitializeComponent();
      this.Text = "Notess";
      WindowsInterop.WindowsTheme.HandleTheme(this.listNotes);
      ApplyRegistrySettings();
    }
    public MidiNoteView(IMidiParserUI parserUI) : this()
    {
      SetUI(parserUI);
    }
    public override void ClearTrack(object sender, EventArgs e)
    {
      base.ClearTrack(sender, e);
      listNotes.Items.Clear();
    }

    /// When a file is loaded, we add our message-filter if one hasn't been attached.
    public override void FileLoaded(object sender, EventArgs e)
    {
      base.FileLoaded(sender, e);

      if (!UserInterface.MidiParser.MessageHandlers.Contains(GotMidiEventD))
        UserInterface.MidiParser.MessageHandlers.Add(GotMidiEventD);
    }

    // Our app is about to load a midi track.
    // 
    // - Hide the note-list.
    public override void BeforeTrackLoaded(object sender, EventArgs e)
    {
      base.BeforeTrackLoaded(sender, e);
      this.listNotes.Visible = false;
    }
    // This is our response to when a track had been loaded
    // 
    // 
    public override void AfterTrackLoaded(object sender, EventArgs e)
    {
      base.AfterTrackLoaded(sender, e);

      // clear note-list
      this.listNotes.Items.Clear();
      // notes have been collected by the midi parser,
      // filter them.
      foreach (MidiNote n in UserInterface.MidiParser.Notes)
      {
        timing.SolveSamples(
          Convert.ToDouble(n.Start),
          samplerate,
          this.UserInterface.MidiParser.MidiTimeInfo.Tempo,
          MidiReader.FileDivision,
          true);
        if (!(n is MidiNote)) continue;
        // s, n, o, l, off
        listNotes.AddItem(
          // Paint
          ListView.DefaultBackColor,
          // MBQT
          n.GetMBT(MidiReader.FileDivision),
          // SAMPLE TIME
          timing.TimeString,
          // MIDI CHANNEL
          n.Ch.HasValue ? n.Ch.ToString() : "?",
          // NOTE KEY
          n.KeyStr,
          // NOTE ON VELOCITY
          n.V1.ToString(),
          // NOTE LENGTH
          n.GetMbtLen2(MidiReader.FileDivision),
          // NOTE OFF VELOCITY
          n.V2.ToString()
         );
      }
      this.listNotes.Visible = true;
    }

    public void BeforeUnload()
    {
      Debug.Print("Saving NoteList Settings");
      string v = Reg.TranslateString(ListColSize);
      if (!string.IsNullOrEmpty(v)) ListColumns = v;
      ListFontSize = listNotes.Font.SizeInPoints.ToString();
    }
//
//		#region Overrides
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
    }
//	#endregion
    void GotMidiEventD(MidiMsgType t, int track, int offset, int imsg, byte bmsg, ulong ppq, int rse, bool isrse)
    {
      if (t== MidiMsgType.NoteOn||t== MidiMsgType.NoteOff)
      {
        UserInterface.MidiParser.CheckNote(t,ppq,Convert.ToByte((rse) & 0x0F),offset,bmsg,isrse);
      }
    }
    
    #region Design
    private System.ComponentModel.IContainer components;
    
    protected override void Dispose(bool disposing)
    {
      if (disposing) {
        if (components != null) {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    
    void InitializeComponent()
    {
      this.listNotes = new System.Windows.Forms.ListView();
      this.colStart = new System.Windows.Forms.ColumnHeader();
      this.colChannel = new System.Windows.Forms.ColumnHeader();
      this.colNote = new System.Windows.Forms.ColumnHeader();
      this.colVOn = new System.Windows.Forms.ColumnHeader();
      this.colLen = new System.Windows.Forms.ColumnHeader();
      this.colVOff = new System.Windows.Forms.ColumnHeader();
      this.colRealtime = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // listNotes
      // 
      this.listNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.listNotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                        this.colStart,
                                        this.colRealtime,
                                        this.colChannel,
                                        this.colNote,
                                        this.colVOn,
                                        this.colLen,
                                        this.colVOff});
      this.listNotes.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listNotes.Font = new System.Drawing.Font("Consolas", 8.25F);
      this.listNotes.FullRowSelect = true;
      this.listNotes.Location = new System.Drawing.Point(0, 0);
      this.listNotes.Name = "listNotes";
      this.listNotes.Size = new System.Drawing.Size(575, 285);
      this.listNotes.TabIndex = 1;
      this.listNotes.UseCompatibleStateImageBehavior = false;
      this.listNotes.View = System.Windows.Forms.View.Details;
      // 
      // colStart
      // 
      this.colStart.Text = "Start";
      this.colStart.Width = 68;
      // 
      // colChannel
      // 
      this.colChannel.Text = "CH";
      this.colChannel.Width = 47;
      // 
      // colNote
      // 
      this.colNote.Text = "Note";
      this.colNote.Width = 79;
      // 
      // colVOn
      // 
      this.colVOn.Text = "on";
      this.colVOn.Width = 86;
      // 
      // colLen
      // 
      this.colLen.Text = "Len";
      this.colLen.Width = 73;
      // 
      // colVOff
      // 
      this.colVOff.Text = "off";
      this.colVOff.Width = 92;
      // 
      // colRealtime
      // 
      this.colRealtime.Text = "HH:MM:SS";
      // 
      // MidiNoteView
      // 
      this.Controls.Add(this.listNotes);
      this.Name = "MidiNoteView";
      this.Size = new System.Drawing.Size(575, 285);
      this.ResumeLayout(false);
    }
    private System.Windows.Forms.ColumnHeader colRealtime;
    private System.Windows.Forms.ColumnHeader colVOff;
    private System.Windows.Forms.ColumnHeader colLen;
    private System.Windows.Forms.ColumnHeader colVOn;
    private System.Windows.Forms.ColumnHeader colNote;
    private System.Windows.Forms.ColumnHeader colChannel;
    private System.Windows.Forms.ColumnHeader colStart;
    private System.Windows.Forms.ListView listNotes;
    #endregion
    
  }
}
