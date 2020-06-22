using System.Management.Automation;
using TooDues.Client.PowerShell.Client;

namespace TooDues.Client.PowerShell
{
    [Cmdlet(VerbsCommunications.Connect, "TooDues")]
    public class ConnectTooDues : Cmdlet
    {
        protected override void ProcessRecord()
        {
            TooDuesClient.Connect();

            WriteObject("Connected");
        }
    }
}