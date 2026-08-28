namespace ToneCustoms.ShellStudio.Core;
public sealed class UndoRedoService { readonly Stack<Action> undo=[];readonly Stack<Action> redo=[];public void Record(Action undoAction){undo.Push(undoAction);redo.Clear();}public bool CanUndo=>undo.Count>0;public void Undo(){if(undo.TryPop(out var a))a();}public void Clear(){undo.Clear();redo.Clear();} }
