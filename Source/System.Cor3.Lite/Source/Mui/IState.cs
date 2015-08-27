using System;

namespace Mui
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
