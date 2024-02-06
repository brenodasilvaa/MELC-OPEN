using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MELC.Core.Commons.Enums
{
    public enum ArquivoExtensao
    {
        [Description(".pdf")]
        Pdf = 0,

        [Description(".jpeg")]
        Jpeg = 1,

        [Description(".jpg")]
        Jpg = 2,

        [Description(".png")]
        Png = 3
    }
}
