/* User: oIo * Date: 8/18/2010 * Time: 4:27 AM */

namespace System
{
	static public class AssemblyFileInfoExtension
	{
	  static public System.IO.DirectoryInfo GetAppDirectory(this System.Reflection.Assembly assembly)
	  {
      var finf = new System.IO.FileInfo(assembly.Location);
      return finf.Directory;
	  }
	  static public System.IO.FileInfo GetAppFile(this System.Reflection.Assembly assembly, string filePath)
	  {
	    var dir = assembly.GetAppDirectory();
      return new System.IO.FileInfo(System.IO.Path.Combine(dir.FullName,filePath));
	  }
	}
}


