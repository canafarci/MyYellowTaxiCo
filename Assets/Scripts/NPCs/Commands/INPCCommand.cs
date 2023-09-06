using System.Collections;

namespace TaxiGame.NPC.Command
{
    public interface INPCCommand
    {
        public IEnumerator Execute();
    }
}
