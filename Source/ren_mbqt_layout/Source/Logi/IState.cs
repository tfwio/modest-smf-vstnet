/*
 * Created by SharpDevelop.
 * User: tfwxo
 * Date: 8/14/2015
 * Time: 10:53 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ren_mbqt_layout.Logi
{
  /// <summary>
  /// </summary>
  public interface IState
  {
    string Name { get; set; }
    T StateUndo<T>();
    T StateApply<T>();
  }
}
