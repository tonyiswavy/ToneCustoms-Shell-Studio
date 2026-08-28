namespace ToneCustoms.ShellStudio.Settings;
public sealed class RecentProjects { readonly LinkedList<string> items=[];public IEnumerable<string> Items=>items;public void Touch(string path){var old=items.Find(path);if(old!=null)items.Remove(old);items.AddFirst(path);while(items.Count>12)items.RemoveLast();} }
