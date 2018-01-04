using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System.SequenceTypes
{
    public interface ISequenceKeyframe<SequenceKeyframeValueType>
    {
        SequenceKeyframeValueType Value
        {
            get;
        }

        float Time
        {
            get;
        }
    }
}
