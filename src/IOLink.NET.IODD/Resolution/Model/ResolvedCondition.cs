
using IOLink.NET.IODD.Structure.DeviceFunction;
using IOLink.NET.IODD.Structure.ProcessData;

namespace IOLink.NET.IODD.Resolution;

public record ResolvedCondition(ConditionT ConditionDef, VariableT VariableDef);
