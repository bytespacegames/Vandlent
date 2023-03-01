using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NET;

namespace Vandlent
{
    public class BotRecieveRevisionEvent : Listener
    {
        public bool inconsistency(string old, string newc, string[] bw, string title)
        {
            foreach(string s in bw) {
                if (newc.Split(" ").Contains(s) && !old.Split(" ").Contains(s) && !title.ToLower().Contains(s))
                {
                    return true;
                }
            }
            return false;
        }
        public override async Task OnRevisionMadeAsync(Revision revision)
        {
            int bd = revision.newBytes - revision.oldBytes;
            Console.WriteLine(revision.editor.username + " made a revision to page: " + revision.page.name + $" byte diff = {bd}");
            bool vandalism = false;
            if (revision.newBytes == 0 && revision.oldBytes >= 500) { vandalism = true; }
            
            if (!vandalism)
            {
                string content = await revision.GetContent();
                content = content.ToLower();
                Revision r = await revision.GetPreviousRevision();
                string oldcontent = await r.GetContent();
                oldcontent = oldcontent.ToLower();

                string[] bw = File.ReadAllText(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\bwfile.txt").Split(",");
                if (inconsistency(oldcontent,content,bw,r.page.name))
                {
                    vandalism = true;
                }
            }
            

            if (vandalism)
            {
                //await revision.Revert();
                await revision.Rollback();
                Console.WriteLine("Rolled back revisions by " + revision.editor.username);

                Page usertalk = new Page("User talk:" + revision.editor.username);
                usertalk.Append("\n\n")
            }
        }
    }
}
