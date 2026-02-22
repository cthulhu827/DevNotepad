using CronExpressionDescriptor;
using System;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Describe CRON", "14d95d21-875d-420c-bf9f-bdbe00b6fdd6")]
    public class CronDescribeTransformer : LineTransformer
    {
        protected override string TransformLine(string line)
        {
            try
            {
                return $"CRON '{line}' description: {ExpressionDescriptor.GetDescription(line)}";
            }
            catch (Exception e)
            {
                return $"Failed to describe CRON '{line}': {e.Message}";
            }
        }
    }
}