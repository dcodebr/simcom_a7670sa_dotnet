using System.ComponentModel;

namespace Simcom.A7670SA;

public enum SimcomA7670SASmsReadEnum {
    [Description("ALL")] All = 1,
    [Description("REC UNREAD")] RecUnread = 2,
    [Description("REC READ")] RecRead = 3,
    [Description("STO UNSENT")] StoUnsent = 4,
    [Description("STO SENT")] StoSent = 5
}
