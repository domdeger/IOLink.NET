
using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.IODD.Resolution;

public record ResolvedCondition(ConditionT Condition, VariableT VariableDef);